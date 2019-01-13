using System;
using fmLibrary;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace fmServerCommon
{
    // SessionHandler는 Session의 생성과 파괴를 담당한다.
    // Sever용과 Client용으로 기능 분할.

    public class SessionHandler : ISessionHandler
    {
        //private ConcurrentDictionary<long, Session> m_dicSessions = new ConcurrentDictionary<long, Session>();
        private IKeyGenerator m_keyGenerator;
        private ManagerSAEA m_managerSAEA;
        //private SessionManager m_managerSession;

        public delegate void fnSessionHandler(SessionBase session);
        protected fnSessionHandler m_fnAddSession;   // 통합관리 목적: 서버에서 Session이 받은 Message를 통합 처리 하기 위한 함수포인터
        protected fnSessionHandler m_fnRemoveSession;   // 통합관리 목적: 서버에서 Session이 받은 Message를 통합 처리 하기 위한 함수포인터

        public SessionHandler(ManagerSAEA saea, fnSessionHandler OnAdd, fnSessionHandler OnRemove)
        {
            m_keyGenerator = new LongKeyForClient();
            m_managerSAEA = saea;
            //m_managerSession = sessionManager;
            m_fnAddSession += OnAdd;
            m_fnRemoveSession += OnRemove;
        }

        public SessionBase CreateSession(Socket socket)
        {
            //Logger.Debug("CreateSession TryPopRecvSAEA Count {0}", m_managerSAEA.PopRecvSAEACount);

            SocketAsyncEventArgs recvSAEA;
            if (false == m_managerSAEA.TryPopRecvSAEA(out recvSAEA))
            {
                Task.Run(() => { socket.CloseEx(); });
                // 로그
                //Logger.Error("Failed CreateSession TryPopRecvSAEA");
                return null;
            }

            SocketAsyncEventArgs sendSAEA;
            if (false == m_managerSAEA.TryPopSendSAEA(out sendSAEA))
            {
                m_managerSAEA.PushRecvSAEA(recvSAEA);
                Task.Run(() => { socket.CloseEx(); });
                // 로그
                //Logger.Error("Failed CreateSession TryPopSendSAEA");
                return null;
            }

            SessionBase session = new Session(socket, recvSAEA, sendSAEA, m_managerSAEA.m_pooledBufferManager);
            if (null == session)
            {
                recvSAEA.UserToken = null;
                sendSAEA.UserToken = null;
                m_managerSAEA.PushRecvSAEA(recvSAEA);
                m_managerSAEA.PushSendSAEA(sendSAEA);
                Task.Run(() => { socket.CloseEx(); });
                //Logger.Error("Failed new Session");
                return null;
            }

            long managedNumber = m_keyGenerator.Alloc();
            session.SetNumber(managedNumber, Environment.TickCount);

            //if (false == m_managerSession.TryAdd(session))
            //    return null;
            if (null != m_fnAddSession)
                m_fnAddSession(session);

            return session;
        }

        public void DestroySession(SessionBase session)
        {
            if (null == session)
            {
                Logger.Error("DestroySession session == null");
                return;
            }

            if (null != m_managerSAEA)
            {
                session.m_saeaReciver.UserToken = null;
                session.m_saeaSender.UserToken = null;
                // 버퍼 셋팅도 다시 해줘야 하나? 최초 BufferManager에서 할당받은 위치겠지만 
                m_managerSAEA.PushRecvSAEA(session.m_saeaReciver);
                m_managerSAEA.PushSendSAEA(session.m_saeaSender);
            }

            long id = session.GetNumber();
            m_keyGenerator.Free(id);
            //m_managerSession.Remove(session);
            if (null != m_fnAddSession)
                m_fnRemoveSession(session);

            //session = null;
            Logger.Debug(string.Format("DestroySession {0}", id));
        }
    }

    public class ServerSessionHandler : ISessionHandler
    {
        public delegate void ConnectingHandler(SessionBase session);
        protected ConnectingHandler m_handlerCheckingServer;  // 통합관리 목적, 서버장애 대책: 서버에서 Attach 처리 하기 위한 함수 포인터

        //private ConcurrentDictionary<long, Session> m_dicSessions = new ConcurrentDictionary<long, Session>();
        private IKeyGenerator m_keyGenerator;
        private ManagerSAEA m_managerSAEA;
        //private SessionManager m_managerSession;

        public delegate void fnSessionHandler(SessionBase session);
        protected fnSessionHandler m_fnAddSession;   // 통합관리 목적: 서버에서 Session이 받은 Message를 통합 처리 하기 위한 함수포인터
        protected fnSessionHandler m_fnRemoveSession;   // 통합관리 목적: 서버에서 Session이 받은 Message를 통합 처리 하기 위한 함수포인터

        public ServerSessionHandler(ManagerSAEA saea, fnSessionHandler OnAdd, fnSessionHandler OnRemove, ConnectingHandler fnCheckingServer)
        {
            m_keyGenerator = new LongKeyForServer();
            m_managerSAEA = saea;
            //m_managerSession = sessionManager;
            m_fnAddSession += OnAdd;
            m_fnRemoveSession += OnRemove;
            m_handlerCheckingServer += fnCheckingServer;
        }

        public SessionBase CreateSession(Socket socket)
        {
            SocketAsyncEventArgs recvSAEA;
            if (!m_managerSAEA.TryPopRecvSAEA(out recvSAEA))
            {
                Task.Run(() => { socket.CloseEx(); });
                // 로그
                return null;
            }

            SocketAsyncEventArgs sendSAEA;
            if (!m_managerSAEA.TryPopSendSAEA(out sendSAEA))
            {
                m_managerSAEA.PushRecvSAEA(recvSAEA);
                Task.Run(() => { socket.CloseEx(); });
                // 로그
                return null;
            }

            ServerSession session = new ServerSession(socket, recvSAEA, sendSAEA, m_managerSAEA.m_pooledBufferManager);
            if (null == session)
            {
                recvSAEA.UserToken = null;
                sendSAEA.UserToken = null;
                m_managerSAEA.PushRecvSAEA(recvSAEA);
                m_managerSAEA.PushSendSAEA(sendSAEA);
                Task.Run(() => { socket.CloseEx(); });

                return null;
            }

            long managedNumber = m_keyGenerator.Alloc();
            session.SetNumber(managedNumber, Environment.TickCount);

            if (null != m_fnAddSession)
                m_fnAddSession(session);

            //if (false == m_managerSession.TryAdd(session))
            //    return null;

            return session;
        }

        public void DestroySession(SessionBase session)
        {
            if (null == session)
            {
                Logger.Error("DestroySession session == null");
                return;
            }

            if (null != m_managerSAEA)
            {
                session.m_saeaReciver.UserToken = null;
                session.m_saeaSender.UserToken = null;
                // 버퍼 셋팅도 다시 해줘야 하나? 최초 BufferManager에서 할당받은 위치겠지만 
                m_managerSAEA.PushRecvSAEA(session.m_saeaReciver);
                m_managerSAEA.PushSendSAEA(session.m_saeaSender);
            }

            long id = session.GetNumber();
            m_keyGenerator.Free(id);

            if (null != m_fnAddSession)
                m_fnRemoveSession(session);

            CheckingServer(session);

            //m_managerSession.Remove(session);
            //session.Dispose();
            //session = null;
            Logger.Debug(string.Format("DestroySession {0}", id));
        }

        private void CheckingServer(SessionBase session)
        {
            if (null != m_handlerCheckingServer)
                m_handlerCheckingServer(session);
        }
    }
}
