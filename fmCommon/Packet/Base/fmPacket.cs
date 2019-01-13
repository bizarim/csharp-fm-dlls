using System;

namespace fmCommon
{
    public abstract class fmPacket : IDisposable
    {
        protected bool m_disposed;
        protected PacketType m_packetType = PacketType.PT_Unkwon;
        protected abstract void Reset();

        public PacketType GetPacketType() { return m_packetType; }

        public virtual void Serialize(Packet p)
        {
            p.SetPacketType(m_packetType);
            p.WriteInt((int)m_packetType);
        }

        public virtual void Deserialize(Packet p)
        {
            int size = p.ReadInt();
            m_packetType = (PacketType)p.ReadInt();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmPacket()
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
