using System.Net.Sockets;

// 2015.02.03 리펙토링
// 다시 한번 생각해 보자.
// 왜 NetService를 만들려고 했지?
// NetService를 만든 이유는 Server의 네트워크 기능과 쓰레드 기능 등 
// 서버의 기능을.. 역할을 분리 하기 위해서 이다.
// 그러면 내가 생각하는 NetService의 기능은 뭘까?
// 아니 젤 중요하다고 생각 하는 건 뭘까?
// Socket으로 Session생성과 파괴가 젤 중요하다.
// 그리고 억셉, 어테치 인지. 이렇게 된건 NetService가 여러개 이기 때문에
// 하아... 복잡도만 높아졌다.
// 최대한 심플하게 하고 싶은데...
// 2중으로 연결하는건 정말 맘에 안드는데.
// 이번에는 여기까지. 담에 좀더 깊이 있게 심플하게 만들어보자.

namespace fmLibrary
{
    public class NetService
    {
        public delegate void MessageHandler(SessionBase session, byte[] buffer, int offset, int length);
        protected MessageHandler m_handlerReceiveMessage;   // 통합관리 목적: 서버에서 Session이 받은 Message를 통합 처리 하기 위한 함수포인터

        private Server m_server;
        private NetworkRole m_netRole;
        protected ISessionHandler m_sessionHandler;

        public NetService(Server server, ISessionHandler sessionHandler, NetworkRole role)
        {
            m_server = server;
            m_netRole = role;
            m_sessionHandler = sessionHandler;
        }

        public bool Initialize(MessageHandler messageHandler)
        {
            if (null == messageHandler)
                return false;

            m_handlerReceiveMessage += messageHandler;
            return m_netRole.Initialize(this);
        }

        public bool Start() { return m_netRole.Start(); }
        public void Stop() { m_netRole.Stop(); }

        // 리스너가 socket을 accept 할 때 처리
        public void OnAcceptSocket(Socket socket)
        {
            if (true == m_server.m_bStopped.IsTrue())
                return;

            SessionBase session = CreateSession(socket);
            if (null == session)
            {
                Logger.Error("Failed Create Session");
                return;
            }

            // ver 2.0 비동기로 바꿈
            session.StartReceive();
            //session.AsyncRun(() => session.StartReceive());
            //OnRegisterSession(session);

            Logger.Debug("OnAcceptSocket: {0}", session.GetNumber());
        }

        // Connector가 다른 서버에 붙었을 때 처리
        public void OnAttachResult(Connector connector, Socket socket, bool isSuccess)
        {
            if (false == isSuccess) return;

            SessionBase session = CreateSession(socket);
            if (null == session)
            {
                Logger.Error("Failed Create Session");
                return;
            }
            connector.SetSession(session);

            // ver 2.0 비동기로 바꿈
            session.StartReceive();
            //session.AsyncRun(() => session.StartReceive());

            //AttachingResult(connector);

            Logger.Debug("OnAttachResult: {0}", session.GetNumber());
        }

        // 붙어 있던 Session이 해제 됬을 때 처리
        protected void OnDisconnectSession(SessionBase session, CloseReason reason)
        {
            Logger.Debug("OnDisconnectSession: {0}", session.GetNumber());
            RemoveSession(session);
        }

        // Session이 Message를 받았을 때 처리
        protected void OnReceiveMessage(SessionBase session, byte[] buffer, int offset, int length)
        {
            if (null != m_handlerReceiveMessage)
                m_handlerReceiveMessage(session, buffer, offset, length);
        }

        // Session 생성
        protected SessionBase CreateSession(Socket socket)
        {
            SessionBase session = m_sessionHandler.CreateSession(socket);
            if (null == session) return null;

            session.m_handlerReceived += OnReceiveMessage;
            session.m_handlerDisconnected += OnDisconnectSession;

            return session;
        }

        // Session 파괴
        protected void RemoveSession(SessionBase session)
        {
            m_sessionHandler.DestroySession(session);
        }

        // Session 생성 후 관리 하기 위한 목적
        //protected void OnRegisterSession(SessionBase session) { }
        //protected void OnRemoveSession(SessionBase session) { }

        // Connetor가 Attach에 성공 했을 때 처리
        //protected void AttachingResult(Connector connector) { }
    }
}
