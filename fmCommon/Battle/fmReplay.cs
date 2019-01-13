using System;
using System.Collections.Generic;

namespace fmCommon.Battle
{
    public class fmActNode : IPacketable, IDisposable
    {
        public eActType ActType { get; set; }
        public bool     IsCri   { get; set; }
        public long     Value   { get; set; }

        public long WD { get; set; }
        public long ED { get; set; }

        public void Write(ref Packet p)
        {
            p.WriteInt((int)ActType);
            p.WriteBool(IsCri);
            p.WriteLong(Value);
        }

        public void Read(ref Packet p)
        {
            ActType = (eActType)p.ReadInt();
            IsCri = p.ReadBool();
            Value = p.ReadLong();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmActNode()
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

    public class fmReplay : IPacketable, IDisposable
    {
        public eBattleResult Result { get; set; }

        public long     MyStartHp       { get; set; }
        public long     OtherStartHp    { get; set; }
        public eElement MyElement       { get; set; }
        public eElement OtherElement    { get; set; }

        public List<fmReplayNode> Nodes { get; set; }

        public fmReplay()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (null == Nodes) Nodes = new List<fmReplayNode>();
        }

        public void Write(ref Packet p)
        {
            p.WriteInt((int)Result);
            p.WriteLong(MyStartHp);
            p.WriteLong(OtherStartHp);
            p.WriteInt((int)MyElement);
            p.WriteInt((int)OtherElement);

            //if (null == Nodes) Nodes = new List<fmReplayNode>();

            Nodes.Write(ref p);
        }

        public void Read(ref Packet p)
        {
            Result = (eBattleResult)p.ReadInt();
            MyStartHp = p.ReadLong();
            OtherStartHp = p.ReadLong();
            MyElement = (eElement)p.ReadInt();
            OtherElement = (eElement)p.ReadInt();

            if (null == Nodes) Nodes = new List<fmReplayNode>();

            Nodes.Read(ref p);
        }

        public bool TryAdd(fmReplayNode node)
        {
            if (null == Nodes)
                Nodes = new List<fmReplayNode>();

            Nodes.Add(node);

            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmReplay()
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

    public class fmReplayNode : IPacketable, IDisposable
    {
        public int              Turn            { get; set; }
        public long             MyRemainHp      { get; set; }
        public long             OtherRemainHp   { get; set; }

        public List<fmActNode>  MyPreNodes      { get; set; }
        public List<fmActNode>  MyInNodes       { get; set; }
        public List<fmActNode>  MyPostNodes     { get; set; }

        public List<fmActNode>  OtherPreNodes   { get; set; }
        public List<fmActNode>  OtherInNodes    { get; set; }
        public List<fmActNode>  OtherPostNodes  { get; set; }

        public fmReplayNode()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (null == MyPreNodes) MyPreNodes = new List<fmActNode>();
            if (null == MyInNodes) MyInNodes = new List<fmActNode>();
            if (null == MyPostNodes) MyPostNodes = new List<fmActNode>();

            if (null == OtherPreNodes) OtherPreNodes = new List<fmActNode>();
            if (null == OtherInNodes) OtherInNodes = new List<fmActNode>();
            if (null == OtherPostNodes) OtherPostNodes = new List<fmActNode>();
        }

        public void Write(ref Packet p)
        {
            p.WriteInt(Turn);
            p.WriteLong(MyRemainHp);
            p.WriteLong(OtherRemainHp);

            //if (null == MyPreNodes) MyPreNodes = new List<fmActNode>();
            //if (null == MyInNodes) MyInNodes = new List<fmActNode>();
            //if (null == MyPostNodes) MyPostNodes = new List<fmActNode>();

            //if (null == OtherPreNodes) OtherPreNodes = new List<fmActNode>();
            //if (null == OtherInNodes) OtherInNodes = new List<fmActNode>();
            //if (null == OtherPostNodes) OtherPostNodes = new List<fmActNode>();


            MyPreNodes.Write(ref p);
            MyInNodes.Write(ref p);
            MyPostNodes.Write(ref p);

            OtherPreNodes.Write(ref p);
            OtherInNodes.Write(ref p);
            OtherPostNodes.Write(ref p);
        }

        public void Read(ref Packet p)
        {
            Turn = p.ReadInt();
            MyRemainHp = p.ReadLong();
            OtherRemainHp = p.ReadLong();

            if (null == MyPreNodes) MyPreNodes = new List<fmActNode>();
            if (null == MyInNodes) MyInNodes = new List<fmActNode>();
            if (null == MyPostNodes) MyPostNodes = new List<fmActNode>();

            if (null == OtherPreNodes) OtherPreNodes = new List<fmActNode>();
            if (null == OtherInNodes) OtherInNodes = new List<fmActNode>();
            if (null == OtherPostNodes) OtherPostNodes = new List<fmActNode>();

            MyPreNodes.Read(ref p);
            MyInNodes.Read(ref p);
            MyPostNodes.Read(ref p);

            OtherPreNodes.Read(ref p);
            OtherInNodes.Read(ref p);
            OtherPostNodes.Read(ref p);
        }

        public bool TryMyPreAdd(fmActNode node)
        {
            if (null == MyPreNodes)
                MyPreNodes = new List<fmActNode>();

            MyPreNodes.Add(node);

            return true;
        }
        public bool TryMyInAdd(fmActNode node)
        {
            if (null == MyInNodes)
                MyInNodes = new List<fmActNode>();

            MyInNodes.Add(node);

            return true;
        }
        public bool TryMyPostAdd(fmActNode node)
        {
            if (null == MyPostNodes)
                MyPostNodes = new List<fmActNode>();

            MyPostNodes.Add(node);

            return true;
        }

        public bool TryOtherPreAdd(fmActNode node)
        {
            if (null == OtherPreNodes)
                OtherPreNodes = new List<fmActNode>();

            OtherPreNodes.Add(node);

            return true;
        }
        public bool TryOtherInAdd(fmActNode node)
        {
            if (null == OtherInNodes)
                OtherInNodes = new List<fmActNode>();

            OtherInNodes.Add(node);

            return true;
        }
        public bool TryOtherPostAdd(fmActNode node)
        {
            if (null == OtherPostNodes)
                OtherPostNodes = new List<fmActNode>();

            OtherPostNodes.Add(node);

            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmReplayNode()
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
