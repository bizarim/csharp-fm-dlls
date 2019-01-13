using System;
using System.Collections.Generic;

namespace fmCommon
{
    public class fmDiscoveryRs : IPacketable
    {
        public bool IsLevelUp { get; set; }
        public int Gold { get; set; }
        public int Stone { get; set; }
        //public int Ticket { get; set; }
        public int Ruby { get; set; }
        public int Exp { get; set; }
        //public int DHeart { get; set; }
        //public int Seal { get; set; }
        public int SCKey { get; set; }

        public void Read(ref Packet p)
        {
            IsLevelUp = p.ReadBool();
            Gold = p.ReadInt();
            Stone = p.ReadInt();
            //Ticket = p.ReadInt();
            Ruby = p.ReadInt();
            Exp = p.ReadInt();
            //DHeart = p.ReadInt();
            //Seal = p.ReadInt();
            SCKey = p.ReadInt();
        }

        public void Write(ref Packet p)
        {
            p.WriteBool(IsLevelUp);
            p.WriteInt(Gold);
            p.WriteInt(Stone);
            //p.WriteInt(Ticket);
            p.WriteInt(Ruby);
            p.WriteInt(Exp);
            //p.WriteInt(DHeart);
            //p.WriteInt(Seal);
            p.WriteInt(SCKey);
        }
    }

    public class fmDropNode : IPacketable
    {
        public eReward Kind { get; set; }
        public string Contents { get; set; }

        public void Read(ref Packet p)
        {
            Kind = (eReward)p.ReadInt();
        }

        public void Write(ref Packet p)
        {
            p.WriteInt((int)Kind);
        }
    }

    public class fmDiscovery : IPacketable, IDisposable
    {
        // 순서
        public int  Index   { get; set; }
        // 몬스터 
        public bool Pvp     { get; set; }
        public int  MonCode { get; set; }

        public int  Exp     { get; set; }
        // 드랍 아이템
        //public eDropKind Kind { get; set; }
        //public string Contents { get; set; }

        public List<fmDropNode> DropList { get; set; }

        public fmDiscovery(int index, bool pvp, int monCode, int exp, List<fmDropNode> list)
        {
            Index = index;
            Pvp = pvp;
            MonCode = monCode;
            Exp = exp;
            DropList = list;
        }

        public fmDiscovery()
        {
            Index = 0;
            Pvp = false;
            MonCode = 0;
            Exp = 0;
            DropList = new List<fmDropNode>();
        }

        public void Write(ref Packet p)
        {
            p.WriteInt(Index);
            p.WriteBool(Pvp);
            p.WriteInt(MonCode);
            //p.WriteInt((int)Kind);
            //p.WriteString(Contents);
            //p.WriteInt(Exp);
            if (null == DropList)
                DropList = new List<fmDropNode>();

            DropList.Write(ref p);
        }

        public void Read(ref Packet p)
        {
            Index = p.ReadInt();
            Pvp = p.ReadBool();
            MonCode = p.ReadInt();
            //Kind = (eDropKind)p.ReadInt();
            //Contents = p.ReadString();
            //Exp = p.ReadInt();
            if (null == DropList)
                DropList = new List<fmDropNode>();

            DropList.Read(ref p);
        }

        public void SetPvp()
        {
            MonCode = 0;
            Pvp = true;

            if (null != DropList)
            {
                DropList.Clear();
            }
                
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmDiscovery()
        {
            Dispose(false);
        }

        bool m_disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                if (null != DropList)
                {
                    DropList.Clear();
                    DropList = null;
                }
            }
            m_disposed = true;
        }
    }
}
