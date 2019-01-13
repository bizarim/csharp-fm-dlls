namespace fmCommon
{
    public class rdInDun : RedisData
    {
        public int Code     { get; set; }
        public int CurPlace { get; set; }
        public int Forge    { get; set; }
        public int Shortcut { get; set; }
        public int Killed   { get; set; }

        public override void Write(ref Packet p)
        {
            p.WriteInt(Code);
            p.WriteInt(CurPlace);
            p.WriteInt(Forge);
            p.WriteInt(Shortcut);
            p.WriteInt(Killed);
        }
        public override void Read(ref Packet p)
        {
            Code = p.ReadInt();
            CurPlace = p.ReadInt();
            Forge = p.ReadInt();
            Shortcut = p.ReadInt();
            Killed = p.ReadInt();
        }
        protected override void Release() { }
        protected override void Dispose(bool disposing) { base.Dispose(disposing); }
    }
}
