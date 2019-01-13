using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace fmLibrary
{
    // Session 베이스
    public class SessionBase : IAsyncSession, IDisposable
    {
        public delegate void DisconnectHandler(SessionBase session, CloseReason reason);
        public delegate void ReceiveHandler(SessionBase session, byte[] buffer, int offset, int length);

        public DisconnectHandler m_handlerDisconnected;
        public ReceiveHandler m_handlerReceived;

        // Session 관리 번호
        private long m_biManagedNumber;
        private int m_nCreateTick;

        // socket 관련
        private Socket m_socket;
        public SocketAsyncEventArgs m_saeaReciver;
        public SocketAsyncEventArgs m_saeaSender;

        // buffer
        private PooledBufferManager m_pooledBufferManager;

        public DateTime m_dateStartTime;
        public DateTime m_dateLastActiveTime;

        private ConcurrentQueue<ArraySegment<byte>> m_queueSend = new ConcurrentQueue<ArraySegment<byte>>();
        private List<ArraySegment<byte>>            m_listSend  = new List<ArraySegment<byte>>();

        private fmBool m_bClosed = new fmBool();
        private fmBool m_bClosing = new fmBool();
        private fmBool m_bSending = new fmBool();
        private fmBool m_bReceiving = new fmBool();

        public SessionBase(Socket socket, SocketAsyncEventArgs recvSAEA, SocketAsyncEventArgs sendSAEA, PooledBufferManager pooledBufferManager)
        {
            m_socket                = socket;
            m_saeaReciver           = recvSAEA;
            m_saeaSender            = sendSAEA;
            m_saeaReciver.UserToken = this;
            m_saeaSender.UserToken  = this;

            m_pooledBufferManager   = pooledBufferManager;

            DateTime dateTimeNow    = DateTime.Now;
            m_dateStartTime         = dateTimeNow;
            m_dateLastActiveTime    = dateTimeNow;
        }

        public void SetNumber(long number, int tick) { m_biManagedNumber = number; m_nCreateTick = tick; }
        public long GetNumber() { return m_biManagedNumber; }
        public int GetCreatedTick() { return m_nCreateTick; }

        public void StartReceive()
        {
            if (false == IsConnected())
            {
                ForceDisconnect(CloseReason.NotConnected);
                m_bReceiving.ForceFalse();
                return;
            }

            try
            {
                bool willRaiseEvent = m_socket.ReceiveAsync(m_saeaReciver);

                m_bReceiving.ForceTrue();

                // I/O 작업이 동기적으로 완료된 경우 false를 반환합니다.
                // 이 경우에는 e 매개 변수에서 SocketAsyncEventArgs.Completed 이벤트가 발생하지 않으며,
                // 메서드 호출이 반환된 직후 매개 변수로 전달된 e 개체를 검사하여 작업 결과를 검색할 수 있습니다. 
                if (false == willRaiseEvent)
                {
                    ProcessReceive(m_saeaReciver);
                }
            }
            catch (Exception ex)
            {
                OnError(string.Format("StartReceive {0}", ex.ToString()));
                m_bReceiving.ForceFalse();
                ForceDisconnect(CloseReason.SocketError);
                return;
            }
        }

        public virtual void ProcessReceive(SocketAsyncEventArgs e)
        {
            m_bReceiving.SetFalse();

            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                OnReceived(e.Buffer, e.Offset, e.BytesTransferred);
            }
            else
            {
                ForceDisconnect(CloseReason.RemoteClose);
                return;
            }

            m_dateLastActiveTime = DateTime.Now;

            StartReceive();
        }

        public virtual void Send(byte[] _buffer, int _offset, int _length)
        {
            if (false == IsConnected())
                return;

            if (null == _buffer)
                return;

            byte[] buffer = m_pooledBufferManager.Take(_length);
            Array.Copy(_buffer, _offset, buffer, 0, _length);

            m_queueSend.Enqueue(new ArraySegment<byte>(buffer, 0, _length));

            StartSend();
        }

        public void StartSend()
        {
            if (false == IsConnected()) return;
            if (m_queueSend.Count <= 0) return;

            // send 할 때 setTrue를 하지 못하는 경우가 발생 보내지마.
            if (false == m_bSending.SetTrue()) return;

            try
            {
                m_listSend.Clear();

                ArraySegment<byte> arrSegment;
                while (m_queueSend.TryDequeue(out arrSegment))
                {
                    m_listSend.Add(arrSegment);
                }

                m_saeaSender.BufferList = m_listSend;

                bool willRaiseEvent = m_socket.SendAsync(m_saeaSender);

                // I/O 작업이 동기적으로 완료된 경우 false를 반환합니다.
                // 이 경우에는 e 매개 변수에서 SocketAsyncEventArgs.Completed 이벤트가 발생하지 않으며,
                // 메서드 호출이 반환된 직후 매개 변수로 전달된 e 개체를 검사하여 작업 결과를 검색할 수 있습니다. 
                if (false == willRaiseEvent)
                {
                    ProcessSend(m_saeaSender);
                }
            }
            catch (Exception ex)
            {
                OnError(string.Format("StartReceive {0}", ex.ToString()));
                m_bSending.ForceFalse();
                ForceDisconnect(CloseReason.SocketError);
                return;
            }
        }

        public void ProcessSend(SocketAsyncEventArgs e)
        {
            if (null != e.BufferList)
            {
                if (e.BufferList.Count != m_listSend.Count)
                {
                    Logger.Debug("Different Send Count BL:{0}/SL:{1}", e.BufferList.Count, m_listSend.Count);
                }

                // [ COMMNET BY KWJ ] : 20150507
                // 하아.....
                try
                {
                    foreach (var buffer in m_listSend)
                    {
                        m_pooledBufferManager.Return(buffer.Array);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("ProcessSend - m_listSend Count Error");
                    Logger.Error(ex.ToString());
                }

                m_listSend.Clear();
            }

            m_bSending.ForceFalse();

            if (e.SocketError == SocketError.Success)
            {
                // 큐에 남은거 보내기
                StartSend();
            }
            else
            {
                Logger.Debug("ProcessSend SocketError");
                ForceDisconnect(CloseReason.SocketError);
                return;
            }

            //m_dateLastActiveTime = DateTime.Now;
        }

        public bool IsAlive(DateTime limitTime)
        {
            return limitTime < m_dateLastActiveTime;
        }

        public bool IsConnected()
        {
            if (null == m_socket) return false;
            if (true == m_bClosing.IsTrue()) return false;
            if (true == m_bClosed.IsTrue()) return false;

            return true;
        }

        // 서버에서 강제로 제거 용
        public void ForceDisconnect(CloseReason reason = CloseReason.Unknown)
        {
            try
            {
                if (reason == CloseReason.RemoteClose)
                    Logger.Debug("ForceDisconnect: {0}", reason);
                else
                    Logger.Log("ForceDisconnect: {0}", reason);

                if (true == m_bClosing.SetTrue())
                {
                    // [ MUSTBE BY KWJ ] : 20150212
                    // OnDisconnected 메소드 처리 내용 수정하자.
                    // 굳이 세션메니저에서 처리할 내용인가?에 대해서 다시 생각해보자.
                    OnDisconnected(reason);
                    OnClose();
                    Close();
                    m_bClosed.ForceTrue();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        protected virtual void OnDisconnected(CloseReason reason)
        {
            if (null != m_handlerDisconnected)
                m_handlerDisconnected(this, reason);
        }

        protected virtual void OnReceived(byte[] buffer, int offset, int length)
        {
            if (null != m_handlerReceived)
                m_handlerReceived(this, buffer, offset, length);
        }

        protected virtual void OnError(string message)
        {
            Logger.Error(message);
        }

        private void Close()
        {
            if (true == m_bSending.IsTrue())
            {
                Logger.Error("Call Close When SendIng message");
            }
            else
            {
                if (0 != m_listSend.Count)
                {
                    Logger.Error("Call Close When listSend Count {0}", m_listSend.Count);
                }
                m_listSend.Clear();
            }

            ArraySegment<byte> arrSegment;
            while (m_queueSend.TryDequeue(out arrSegment))
            {
                m_pooledBufferManager.Return(arrSegment.Array);
            }

            if (null != m_socket)
            {

                m_socket.CloseEx();
                //Task.Run(() => { m_socket.CloseEx(); });
                m_socket = null;
            }
        }

        protected virtual void OnClose() { }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SessionBase()
        {
            Dispose(false);
        }

        bool m_disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            //if (disposing) { }

            m_handlerDisconnected = null;
            m_handlerReceived = null;
            m_saeaReciver = null;
            m_saeaSender = null;
            m_pooledBufferManager = null;

            m_queueSend = null;
            m_listSend.Clear();
            m_listSend = null;

            m_bClosed = null;
            m_bClosing = null;
            m_bSending = null;
            m_bReceiving = null;

            m_disposed = true;
        }
    }
}
