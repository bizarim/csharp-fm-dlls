using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;

namespace fmLibrary
{
    public class ManagerSAEA
    {
        private readonly int m_nReceiveBufferSize = 4096;

        private ServerConfig m_config;
        private BufferManager m_bufferManager;
        public PooledBufferManager m_pooledBufferManager;

        private ConcurrentStack<SocketAsyncEventArgs> m_poolRecvSAEA;
        private ConcurrentStack<SocketAsyncEventArgs> m_poolSendSAEA;

        public bool Initialize(ServerConfig config)
        {
            m_config = config;

            int[] poolSizes = new int[] { 4096, 16, 128, 256, 1024, };       // size를 재설정 해보자.
            m_pooledBufferManager = new PooledBufferManager(poolSizes);

            int bufferSize = m_nReceiveBufferSize;

            m_bufferManager = new BufferManager(bufferSize * m_config.m_nMaxConnection, bufferSize);
            m_bufferManager.InitBuffer();

            try
            {
                {
                    SocketAsyncEventArgs socketEventArg;
                    var socketArgsList = new List<SocketAsyncEventArgs>(m_config.m_nMaxConnection);

                    for (int i = 0; i < m_config.m_nMaxConnection; i++)
                    {
                        socketEventArg = new SocketAsyncEventArgs();
                        socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(CompletedReceive);
                        m_bufferManager.SetBuffer(socketEventArg);
                        socketArgsList.Add(socketEventArg);
                    }
                    m_poolRecvSAEA = new ConcurrentStack<SocketAsyncEventArgs>(socketArgsList);
                }

                {
                    SocketAsyncEventArgs socketEventArg;
                    var socketArgsList = new List<SocketAsyncEventArgs>(m_config.m_nMaxConnection);
                    for (int i = 0; i < m_config.m_nMaxConnection; i++)
                    {
                        socketEventArg = new SocketAsyncEventArgs();
                        socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(CompletedSend);
                        socketEventArg.SetBuffer(null, 0, 0);   // Send 할때 pooledBufferManager를 이용한다.
                        socketArgsList.Add(socketEventArg);
                    }
                    m_poolSendSAEA = new ConcurrentStack<SocketAsyncEventArgs>(socketArgsList);
                }
            }
            catch (Exception ex)
            {
                Logger.Info("Failed Server Init");
                Logger.Error("Out Of Memory! Reset MaxConnectCount!");
                Logger.Error(ex.ToString());
                return false;
            }


            return true;
        }

        public void Uninitialize()
        {
            m_config = null;
            m_bufferManager = null;
            m_pooledBufferManager = null;
            m_poolRecvSAEA = null;
            m_poolSendSAEA = null;
        }

        void CompletedReceive(object sender, SocketAsyncEventArgs e)
        {
            var session = e.UserToken as SessionBase;
            if (null == session)
            {
                Logger.Debug("CompletedReceive() session == null");
                return;
            }

            if (e.LastOperation != SocketAsyncOperation.Receive)
                throw new ArgumentException(string.Format("Invalid LastOperation:{0}", e.LastOperation));

            // ver 2.0 비동기로 바꿈
            session.ProcessReceive(e);
            // session.AsyncRun(()=> session.ProcessReceive(e));
        }

        void CompletedSend(object sender, SocketAsyncEventArgs e)
        {
            var session = e.UserToken as SessionBase;
            if (null == session)
            {
                Logger.Warn("CompletedSend() session == null");
                return;
            }

            if (e.LastOperation != SocketAsyncOperation.Send)
                throw new ArgumentException(string.Format("Invalid LastOperation:{0}", e.LastOperation));

            // ver 2.0 보내는건 task를 이용하지 않는다.
            session.ProcessSend(e);
        }

        public int PopRecvSAEACount { get { return m_poolRecvSAEA.Count; } }

        public bool TryPopRecvSAEA(out SocketAsyncEventArgs recvSAEA)
        {
            recvSAEA = null;
            return m_poolRecvSAEA.TryPop(out recvSAEA);
        }

        public void PushRecvSAEA(SocketAsyncEventArgs recvSAEA)
        {
            m_poolRecvSAEA.Push(recvSAEA);


            Logger.Debug("PushRecvSAEA");
        }

        public bool TryPopSendSAEA(out SocketAsyncEventArgs sendSAEA)
        {
            sendSAEA = null;
            return m_poolSendSAEA.TryPop(out sendSAEA);
        }

        public void PushSendSAEA(SocketAsyncEventArgs sendSAEA)
        {
            m_poolSendSAEA.Push(sendSAEA);

            Logger.Debug("PushSendSAEA");
        }
    }
}
