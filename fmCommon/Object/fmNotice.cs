using System;

namespace fmCommon
{
    public class fmNotice : IPacketable, IDisposable
    {
        public string Contents { get; set; }

        public fmNotice()
        {
            Contents = string.Empty;
        }

        public void Read(ref Packet p)
        {
            Contents = p.ReadString();
        }

        public void Write(ref Packet p)
        {
            p.WriteString(Contents);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmNotice()
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
