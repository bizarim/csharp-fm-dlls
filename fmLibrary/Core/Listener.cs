using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace fmLibrary
{
    // 리스너 클래스
    public class Listener : IDisposable
    {
        public delegate void AcceptHandler(Socket socket);

        public AcceptHandler m_handlerAccepted;

        private Socket m_socketListener;

        private ConcurrentStack<SocketAsyncEventArgs> m_poolAcceptSAEA;

        private IPEndPoint m_ipEndPoint;

        private ListenerConfig m_config;

        public Listener(ListenerConfig config) { m_config = config; }

        public void Stop()
        {
            if (m_socketListener == null)
                return;

            lock (this)
            {
                if (m_socketListener == null)
                    return;

                try
                {
                    m_socketListener.Close();
                }
                finally
                {
                    m_socketListener = null;
                }
            }
        }

        public bool Start()
        {
            IPAddress addr = null;

            IPAddress.TryParse(m_config.m_strPrivateIP, out addr);

            if (addr == null)
                addr = IPAddress.Any;

            m_ipEndPoint = new IPEndPoint(addr, m_config.m_nPort);

            m_socketListener = new Socket(m_ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                SocketAsyncEventArgs socketEventArg;

                // 3개가 있어야 한다고 보는건지.
                var socketArgsList = new List<SocketAsyncEventArgs>(m_config.m_nMaxAcceptOps);

                for (int i = 0; i < m_config.m_nMaxAcceptOps; i++)
                {
                    // SocketAsyncEventArgs는 버퍼가 없어야 한다
                    socketEventArg = new SocketAsyncEventArgs();
                    socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(CompletedAccept);
                    socketArgsList.Add(socketEventArg);
                }

                m_poolAcceptSAEA = new ConcurrentStack<SocketAsyncEventArgs>(socketArgsList);

                m_socketListener.Bind(m_ipEndPoint);
                m_socketListener.Listen(m_config.m_nBackLog);
                m_socketListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

                bool isReadyForAccept = StartAccept();

                if (isReadyForAccept)
                    Logger.Info("Listener Start [{0}:{1} *{2}]", addr, m_config.m_nPort, m_config.m_nMaxAcceptOps);
                else
                    Logger.Info("Listener is not ready");

                return isReadyForAccept;
            }
            catch (Exception e)
            {
                OnError(e);
                return false;
            }
        }

        bool StartAccept()
        {
            //if (m_server == null || m_server.Stopped.IsTrue())
            //    return false;

            SocketAsyncEventArgs acceptSAEA;

            if (!m_poolAcceptSAEA.TryPop(out acceptSAEA))
            {
                Logger.Info("Not Enough Accept SAEA", m_config.m_nMaxAcceptOps);
                return false;
            }

            try
            {
                bool willRaiseEvent = m_socketListener.AcceptAsync(acceptSAEA);

                if (false == willRaiseEvent)
                {
                    ProcessAccept(acceptSAEA);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("StartAccept");
                Logger.Error(ex.ToString());
                // TODO 풀로 돌려보내고 다시 받아라? 하면되나
                m_poolAcceptSAEA.Push(acceptSAEA);
                StartAccept();
            }

            return true;
        }

        void CompletedAccept(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        void ProcessAccept(SocketAsyncEventArgs e)
        {
            //if (m_server == null || m_server.Stopped.IsTrue())
            //    return;

            Socket socket = null;

            if (e.SocketError != SocketError.Success)
            {
                if (false == SocketEx.IsIgnorableError(e.SocketError))
                    Logger.Info("ProcessAccept:{0}", e.SocketError);

                Task.Run(() => { e.AcceptSocket.CloseEx(); });

                e.AcceptSocket = null;
                m_poolAcceptSAEA.Push(e);
                StartAccept();
                return;
            }

            socket = e.AcceptSocket;

            StartAccept();

            e.AcceptSocket = null;
            m_poolAcceptSAEA.Push(e);

            if (socket != null)
                OnAccepted(socket);
        }

        void OnAccepted(Socket socket)
        {
            if (m_handlerAccepted != null)
                m_handlerAccepted(socket);
        }

        void OnError(Exception ex)
        {

        }

        public void Dispose()
        {
            SocketAsyncEventArgs eventArgs = null;
            while (m_poolAcceptSAEA.Count > 0)
            {
                if (m_poolAcceptSAEA.TryPop(out eventArgs))
                    eventArgs.Dispose();
            }

            m_poolAcceptSAEA = null;
        }
    }
}
