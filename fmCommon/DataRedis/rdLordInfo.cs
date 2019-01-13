using System;

namespace fmCommon
{
    public class rdLordInfo : IDisposable, IPacketable
    {
        public string Name { get; set; }
        public int Lv { get; set; }
        public long Exp { get; set; }
        public long Gold { get; set; }
        public int Ruby { get; set; }
        public int Stone { get; set; }
        //public int Ticket { get; set; }
        public int Floor { get; set; }
        public int PvpPoint { get; set; }
        public int DTombCnt { get; set; }
        //public int DHeartCnt { get; set; }
        //public int DHeartFnc { get; set; }
        public bool Payment { get; set; }
        //public int Seal { get; set; }
        public int SCKey { get; set; }

        public void Write(ref Packet p)
        {
            p.WriteString(Name);
            p.WriteInt(Lv);
            p.WriteLong(Exp);
            p.WriteLong(Gold);
            p.WriteInt(Ruby);
            p.WriteInt(Stone);
            //p.WriteInt(Ticket);
            p.WriteInt(Floor);
            p.WriteInt(PvpPoint);
            p.WriteInt(DTombCnt);
            //p.WriteInt(DHeartCnt);
            //p.WriteInt(DHeartFnc);
            p.WriteBool(Payment);
            //p.WriteInt(Seal);
            p.WriteInt(SCKey);
        }

        public void Read(ref Packet p)
        {
            Name = p.ReadString();
            Lv = p.ReadInt();
            Exp = p.ReadLong();
            Gold = p.ReadLong();
            Ruby = p.ReadInt();
            Stone = p.ReadInt();
            //Ticket = p.ReadInt();
            Floor = p.ReadInt();
            PvpPoint = p.ReadInt();
            DTombCnt = p.ReadInt();
            //DHeartCnt = p.ReadInt();
            //DHeartFnc = p.ReadInt();
            Payment = p.ReadBool();
            //Seal = p.ReadInt();
            SCKey = p.ReadInt();
        }

        protected bool m_disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~rdLordInfo()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing) { }
            m_disposed = true;
        }
    }
}
