using System;

namespace fmCommon
{
    public class fmOpInfo : IPacketable, IDisposable, IfmObject
    {
        public eFinance Finance { get; set; }
        public int Amount { get; set; }

        public void Read(ref Packet p)
        {
            Finance = (eFinance)p.ReadInt();
            Amount = p.ReadInt();
        }

        public void Write(ref Packet p)
        {
            p.WriteInt((int)Finance);
            p.WriteInt(Amount);
        }

        public object Clone()
        {
            fmOpInfo copy = new fmOpInfo();
            copy.Finance = Finance;
            copy.Amount = Amount;

            return copy;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmOpInfo()
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
