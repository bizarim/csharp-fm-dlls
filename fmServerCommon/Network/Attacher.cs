using fmLibrary;
using fmCommon;
using System.Net.Sockets;

namespace fmServerCommon
{
    public class Attacher : Connector
    {
        // 알기 쉽게 TO, FROM
        protected appServer m_fromServer;
        protected eServerType m_eToServerType;

        public eServerType GetServerType() { return m_eToServerType; }

        public void SendPacket(fmProtocol fp)
        {
            if (null == m_session) return;
            if (null == fp) return;

            using (Packet sendPacket = new Packet())
            {
                fp.Serialize(sendPacket);
                m_session.Send(sendPacket.GetBuffer(), 0, sendPacket.GetPacketLen());
            }
        }

        public void SendPacket(Packet p)
        {
            if (null == m_session) return;
            if (null == p) return;

            m_session.Send(p.GetBuffer(), 0, p.GetPacketLen());
        }
    }

    public class CenterServerAttacher : Attacher
    {
        int m_nSequence;
        AttacherConfig m_toConfig;
        ListenerConfig m_fromConfig;

        public CenterServerAttacher(appServer fromServer)
        {
            m_fromServer = fromServer;
        }

        public void OnAttach(int seq, AttacherConfig toConfig, ListenerConfig fromConfig)
        {
            if (null == toConfig || null == fromConfig)
            {
                Logger.Error("OnAttach: config == null");
                return;
            }

            m_nSequence = seq;
            m_toConfig = toConfig;
            m_fromConfig = fromConfig;
            Connect(m_toConfig.m_strIP, m_toConfig.m_nPort);
            m_eToServerType = eServerType.Center;

        }

        public override void OnAttachResult(Socket socket, bool isSuccess)
        {
            if (false == isSuccess)
            {
                Logger.Error("Fail. Connect to CenterServer. Close Server");
                System.Threading.Thread.Sleep(1000);
                m_fromServer.ShutDown();
                return;
            }

            RequestRegistServer(m_fromServer.GetServerType());
        }

        public void RequestRegistServer(eServerType eFromServerType)
        {
            ServerSession ss = m_session as ServerSession;

            descOtherServer desc = new descOtherServer();
            desc.m_eServerType = m_eToServerType;
            ss.m_descServer = desc;

            using (PT_Server_RegisterAtCenter_RQ fmp = new PT_Server_RegisterAtCenter_RQ())
            {
                fmp.m_eServerType = eFromServerType;
                fmp.m_nSequence = m_nSequence;
                fmp.m_strIP = m_fromConfig.m_strPublicIP;
                fmp.m_nPort = m_fromConfig.m_nPort;

                SendPacket(fmp);
            }
        }
    }

    public class ChatServerAttacher : Attacher
    {
        int m_nSequence;
        AttacherConfig m_toConfig;
        ListenerConfig m_fromConfig;

        public ChatServerAttacher(appServer fromServer)
        {
            m_fromServer = fromServer;
        }

        public bool IsConnected()
        {
            if (null == m_session)
                return false;

            return m_session.IsConnected();
        }

        public void OnAttach(int seq, AttacherConfig toConfig, ListenerConfig fromConfig)
        {
            if (null == toConfig || null == fromConfig)
            {
                Logger.Error("OnAttach: config == null");
                return;
            }

            m_nSequence = seq;
            m_toConfig = toConfig;
            m_fromConfig = fromConfig;
            Connect(m_toConfig.m_strIP, m_toConfig.m_nPort);
            m_eToServerType = eServerType.Chat;
        }

        public override void OnAttachResult(Socket socket, bool isSuccess)
        {
            if (false == isSuccess)
            {
                Logger.Warn("Fail. Connect to ChatServer. Close Server");
                System.Threading.Thread.Sleep(10);
                Connect(m_toConfig.m_strIP, m_toConfig.m_nPort);
                return;
            }

            if (null == m_fromServer)
            {
                Logger.Error("OnAttach: m_fromServer == null");
                return;
            }

            RequestRegistServer(m_fromServer.GetServerType());
        }

        public void RequestRegistServer(eServerType eFromServerType)
        {
            ServerSession ss = m_session as ServerSession;

            descOtherServer desc = new descOtherServer();
            desc.m_eServerType = m_eToServerType;
            ss.m_descServer = desc;

            using (PT_Server_RegisterAtChat_RQ fmp = new PT_Server_RegisterAtChat_RQ())
            {
                fmp.m_eServerType = eFromServerType;
                fmp.m_nSequence = m_nSequence;
                fmp.m_strIP = m_fromConfig.m_strPublicIP;
                fmp.m_nPort = m_fromConfig.m_nPort;

                SendPacket(fmp);
            }
        }
    }

    //public class GameServerAttacher : Attacher
    //{
    //    int m_nSequence;
    //    AttacherConfig m_config;

    //    public void OnAttach(int seq, AttacherConfig config, appServer fromServer)
    //    {
    //        m_nSequence = seq;
    //        m_config = config;
    //        Connect(config.m_strIP, config.m_nPort);
    //        m_eToServerType = eServerType.Game;
    //        m_fromServer = fromServer;
    //    }

    //    public override void OnAttachResult(Socket socket, bool isSuccess)
    //    {
    //        if (false == isSuccess)
    //        {
    //            Logger.Warn("Fail. Connect to GameServer. Retry Connect");
    //            System.Threading.Thread.Sleep(2000);
    //            Connect(m_config.m_strIP, m_config.m_nPort);
    //            return;
    //        }

    //        appServer appServer = m_fromServer as appServer;
    //        RequestRegistServer(appServer.GetServerType());
    //    }

    //    public void RequestRegistServer(eServerType eFromServerType)
    //    {
    //        ServerSession ss = m_session as ServerSession;

    //        descOtherServer desc = new descOtherServer();
    //        desc.m_eServerType = m_eToServerType;
    //        ss.m_descServer = desc;

    //        //using (PT_Server_RegisterAtChat_RQ fmp = new PT_Server_RegisterAtChat_RQ())
    //        //{
    //        //    fmp.m_eServerType = eFromServerType;
    //        //    fmp.m_nSequence = m_nSequence;
    //        //    fmp.m_strIP = m_fromConfig.m_strPublicIP;
    //        //    fmp.m_nPort = m_fromConfig.m_nPort;

    //        //    SendPacket(fmp);
    //        //}
    //    }
    //}

    //public class AuthServerAttacher : Attacher
    //{
    //    int m_nSequence;
    //    AttacherConfig m_config;

    //    public void OnAttach(int seq, AttacherConfig config, Server fromServer)
    //    {
    //        m_nSequence = seq;
    //        m_config = config;
    //        Connect(config.m_strIP, config.m_nPort);
    //        m_eToServerType = eServerType.Auth;
    //        m_fromServer = fromServer;
    //    }

    //    public override void OnAttachResult(Socket socket, bool isSuccess)
    //    {
    //        if (false == isSuccess)
    //        {
    //            Logger.Warn("Fail. Connect to AuthServer. Retry Connect");
    //            System.Threading.Thread.Sleep(2000);
    //            Connect(m_config.m_strIP, m_config.m_nPort);
    //            return;
    //        }

    //        appServer appServer = m_fromServer as appServer;
    //        RequestRegistServer(appServer.GetServerType());
    //    }

    //    public void RequestRegistServer(eServerType eFromServerType)
    //    {

    //    }
    //}
}
