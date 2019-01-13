using System;

namespace fmCommon
{
    public class fmRanker : IPacketable, IDisposable
    {
        public long Rank { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }

        public void Read(ref Packet p)
        {
            Rank = p.ReadLong();
            Name = p.ReadString();
            Floor = p.ReadInt();
        }

        public void Write(ref Packet p)
        {
            p.WriteLong(Rank);
            p.WriteString(Name);
            p.WriteInt(Floor);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmRanker()
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
    }
}
