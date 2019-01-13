using System;

namespace fmCommon
{
    /// <summary>
    /// 프로토콜 추상 클래스
    /// 목적:
    ///     프로토콜을 packet에 직열화/역직렬화 할 수 있게 한다.
    ///     Serialize(Packet p)
    ///     Deserialize(Packet p)
    /// </summary>
    public abstract class fmProtocol : IDisposable
    {
        protected bool m_disposed;
        protected eProtocolType m_eProtocolType = eProtocolType.PT_Unkwon;
        protected abstract void Reset();

        public eProtocolType GeteProtocolType() { return m_eProtocolType; }

        public virtual void Serialize(Packet p)
        {
            p.SeteProtocolType(m_eProtocolType);
            p.WriteInt((int)m_eProtocolType);
        }

        public virtual void Deserialize(Packet p)
        {
            int size = p.ReadInt();
            m_eProtocolType = (eProtocolType)p.ReadInt();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmProtocol()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                Reset();
            }
            m_disposed = true;
        }
    }
}
