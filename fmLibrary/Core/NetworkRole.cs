using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace fmLibrary
{
    // NetworkRole은 받아 드리느냐, 붙을 거냐 [listener or connector(attacher)]
    // 역할을 구분하기 위해서.
    public class NetworkRole
    {
        protected NetService m_netService;

        public virtual bool Initialize(NetService netService)
        {
            m_netService = netService;
            return true;
        }
        public virtual bool Start() { return true; }
        public virtual void Stop() { }
    }

    // 리스터 기능
    public class NetListenable : NetworkRole
    {
        Listener m_listener;

        public NetListenable(ListenerConfig config)
        {
            m_listener = new Listener(config);
        }

        public override bool Initialize(NetService netService)
        {
            if (false == base.Initialize(netService))
                return false;

            m_listener.m_handlerAccepted = m_netService.OnAcceptSocket;

            return true;
        }

        public override bool Start() { return m_listener.Start(); }
        public override void Stop() { m_listener.Stop(); }

    }

    // connector 기능
    public class NetAttachable : NetworkRole
    {
        //public delegate void ResultAttachHandler(Connector connector, bool isSuccess);
        //protected ResultAttachHandler m_handlerAttachedResult;  // 통합관리 목적, 서버장애 대책: 서버에서 Attach 처리 하기 위한 함수 포인터

        private List<Connector> m_listConnector;

        public NetAttachable(List<Connector> list)
        {
            m_listConnector = list;
            //m_handlerAttachedResult += attachedHandler;
        }

        public override bool Initialize(NetService netService)
        {
            if (false == base.Initialize(netService))
                return false;

            foreach (var node in m_listConnector)
                node.m_handlerAttachResult += OnAttachResult;

            return true;
        }

        public void OnAttachResult(Connector connector, Socket socket, bool isSuccess)
        {
            m_netService.OnAttachResult(connector, socket, isSuccess);

            //if (null != m_handlerAttachedResult)
            //    m_handlerAttachedResult(connector, isSuccess);
        }
    }

    public class NetBothRole : NetworkRole
    {
        //public delegate void ResultAttachHandler(Connector connector, bool isSuccess);
        //protected ResultAttachHandler m_handlerAttachedResult;  // 통합관리 목적, 서버장애 대책: 서버에서 Attach 처리 하기 위한 함수 포인터

        Listener m_listener;
        private List<Connector> m_listConnector;

        public NetBothRole(ListenerConfig config, List<Connector> list)
        {
            m_listener = new Listener(config);
            m_listConnector = list;
            //m_handlerAttachedResult += attachedHandler;
        }

        public override bool Initialize(NetService netService)
        {
            if (false == base.Initialize(netService))
                return false;

            m_listener.m_handlerAccepted = m_netService.OnAcceptSocket;

            foreach (var node in m_listConnector)
                node.m_handlerAttachResult += OnAttachResult;

            return true;
        }

        public void OnAttachResult(Connector connector, Socket socket, bool isSuccess)
        {
            m_netService.OnAttachResult(connector, socket, isSuccess);

            //if (null != m_handlerAttachedResult)
            //    m_handlerAttachedResult(connector, isSuccess);
        }

        public override bool Start() { return m_listener.Start(); }
        public override void Stop() { m_listener.Stop(); }
    }
}
