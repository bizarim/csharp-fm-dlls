using System;

namespace fmCommon
{
    public enum eWorldState
    {
        Normal,
        Busy,
        Full,
        Check,
    }

    public class fmWorld : IfmObject, IPacketable
    {
        public int m_nSequence = 0;
        public string m_strIP = string.Empty;
        public int m_nPort = 0;
        public int m_nPlayer = 0;

        public void Read(ref Packet p)
        {
            m_nSequence = p.ReadInt();
            m_strIP = p.ReadString();
            m_nPort = p.ReadInt();
            m_nPlayer = p.ReadInt();
        }

        public void Write(ref Packet p)
        {
            p.WriteInt(m_nSequence);
            p.WriteString(m_strIP);
            p.WriteInt(m_nPort);
            p.WriteInt(m_nPlayer);
        }

        public object Clone()
        {
            fmWorld copy = new fmWorld();
            copy.m_nSequence = m_nSequence;
            copy.m_strIP = m_strIP;
            copy.m_nPort = m_nPort;
            copy.m_nPlayer = m_nPlayer;

            return copy;

            //return new fmWorld
            //{
            //    m_nIndex = m_nIndex,
            //    m_strIP = m_strIP,
            //    m_nPort = m_nPort,
            //    m_eState = m_eState
            //};
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmWorld()
        {
            Dispose(false);
        }

        bool m_disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            //if (disposing)
            //{
                
            //}
            m_disposed = true;
        }
    }

}
