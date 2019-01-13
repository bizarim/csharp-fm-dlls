using System;
using fmCommon;

namespace fmServerCommon
{
    // 다른 서버의 정보 클래스
    // 기본적으로 descOtherServer 내 서버에 붙어 있는 서버의 정보를 의미 한다.
    public class descOtherServer : IPacketable, IDisposable
    {
        public eState m_eState = eState.eState_Stop;
        public eServerType m_eServerType = eServerType.None;
        public int m_nSequence = 0;
        public string m_strIP = string.Empty;
        public int m_nPort = 0;

        //public eWorldState m_eWorldState { get; set; }
        public int m_nPlayerCount { get; set; }

        public void Read(ref Packet p)
        {
            m_eServerType = (eServerType)p.ReadInt();
            m_eState = (eState)p.ReadInt();
            m_nSequence = p.ReadInt();
            m_strIP = p.ReadString();
            m_nPort = p.ReadInt();
            //m_eWorldState = (eWorldState)p.ReadInt();
            m_nPlayerCount = p.ReadInt();
        }

        public void Write(ref Packet p)
        {
            p.WriteInt((int)m_eServerType);
            p.WriteInt((int)m_eState);
            p.WriteInt(m_nSequence);
            p.WriteString(m_strIP);
            p.WriteInt(m_nPort);
            //p.WriteInt((int)m_eWorldState);
            p.WriteInt(m_nPlayerCount);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~descOtherServer()
        {
            Dispose(false);
        }

        bool m_disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            //if (disposing) { }
            m_disposed = true;
        }

        public fmWorld ToFmWorld()
        {
            return new fmWorld
            {
                m_nPlayer = m_nPlayerCount,
                m_nSequence = m_nSequence,
                m_strIP = m_strIP,
                m_nPort = m_nPort
            };
        }
    }
}
