

using System;

namespace fmCommon
{
    public class fmGameConst : IPacketable, IDisposable
    {
        public int MaxLv            { get; set; }
        public int MaxHaveItemCnt   { get; set; }
        //public int RefreshRuby      { get; set; }
        public int MazeEnterLv      { get; set; }
        public int DTombEnterLv     { get; set; }

        public string ChatSvrIp { get; set; }
        public int ChatSvrPort { get; set; }


        public fmGameConst Clone()
        {
            return new fmGameConst
            {
                MaxLv = this.MaxLv,
                MaxHaveItemCnt = this.MaxHaveItemCnt,
                //RefreshRuby = this.RefreshRuby,
                MazeEnterLv = this.MazeEnterLv,
                DTombEnterLv = this.DTombEnterLv,
                ChatSvrIp = ChatSvrIp,
                ChatSvrPort = ChatSvrPort,
            };
        }

        public void Write(ref Packet p)
        {
            p.WriteInt(MaxLv);
            p.WriteInt(MaxHaveItemCnt);
            //p.WriteInt(RefreshRuby);
            p.WriteInt(MazeEnterLv);
            p.WriteInt(DTombEnterLv);
            p.WriteString(ChatSvrIp);
            p.WriteInt(ChatSvrPort);
        }

        public void Read(ref Packet p)
        {
            MaxLv = p.ReadInt();
            MaxHaveItemCnt = p.ReadInt();
            //RefreshRuby = p.ReadInt();
            MazeEnterLv = p.ReadInt();
            DTombEnterLv = p.ReadInt();
            ChatSvrIp = p.ReadString();
            ChatSvrPort = p.ReadInt();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~fmGameConst()
        {
            Dispose(false);
        }
        protected bool m_disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            m_disposed = true;
        }
    }
}
