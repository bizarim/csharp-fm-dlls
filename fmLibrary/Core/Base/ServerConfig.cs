using System;
using System.Xml.Serialization;

namespace fmLibrary
{
    // [COMMENT BY KWJ] : 2016-03-23
    // 가독성이 좋게 수정

    [Serializable]
    public class ServerConfig
    {
        [XmlElement("Sequence")]
        public int m_nSequence;
        [XmlElement("MaxConnection")]
        public int m_nMaxConnection;
        [XmlElement("TimeOut")]
        public int m_nSessionTimeOut;

        [XmlElement("Thread")]
        public ThreadConfig m_thread;
        [XmlElement("DB")]
        public dbConfig m_db;

        [XmlElement("ClientListner")]
        public ListenerConfig m_listnerClient;
        [XmlElement("ServerListner")]
        public ListenerConfig m_listnerServer;

        [XmlElement("Center")]
        public AttacherConfig m_center;
        [XmlElement("Auth")]
        public AttacherConfig m_auth;
        [XmlElement("Game")]
        public AttacherConfig m_game;
        [XmlElement("PrivateChat")]
        public AttacherConfig m_pirvateChat;
        [XmlElement("PublicChat")]
        public AttacherConfig m_publicChat;

        [XmlElement("IABGoogle")]
        public IABGoogle m_IABGoogle;

        //[XmlElement("APNS_SendBox")]
        //public bool m_isAPNS_SendBox;

        [XmlElement("Cver")]
        public int m_nCver;
    }

    [Serializable]
    public class dbConfig
    {
        [XmlElement("Account")]
        public string m_myAcc;
        [XmlElement("Log")]
        public string m_myLog;
        [XmlElement("RedisToken")]
        public string m_rdToken;
        [XmlElement("RedisGame")]
        public string m_rdGame;
    }

    [Serializable]
    public class ListenerConfig
    {
        [XmlElement("MaxAccept")]
        public int m_nMaxAcceptOps;
        [XmlElement("PublicIP")]
        public string m_strPublicIP;
        [XmlElement("PrivateIP")]
        public string m_strPrivateIP;
        [XmlElement("Port")]
        public int m_nPort;
        [XmlElement("BackLog")]
        public int m_nBackLog;
    }

    [Serializable]
    public class ThreadConfig
    {
        [XmlElement("MinSync")]
        public int m_nMinSync;
        [XmlElement("MaxSync")]
        public int m_nMaxSync;
        [XmlElement("MinAsync")]
        public int m_nMinAsync;
        [XmlElement("MaxAsync")]
        public int m_nMaxAsync;
    }

    [Serializable]
    public class AttacherConfig
    {
        [XmlElement("IP")]
        public string m_strIP;
        [XmlElement("Port")]
        public int m_nPort;
    }

    [Serializable]
    public class IABGoogle
    {
        [XmlElement("PubKey")]
        public string m_strPubKey;
        [XmlElement("Exponent")]
        public string m_strExponent;
        [XmlElement("Modulus")]
        public string m_strModulus;
    }

    //[Serializable]
    //public class APNS
    //{
    //    [XmlElement("APNS_SendBox")]
    //    public bool m_isAPNS_SendBox;
    //}
}
