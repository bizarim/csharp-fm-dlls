namespace fmCommon
{
    public class rdMap : RedisData
    {
        public int Code { get; set; }
        public bool Open { get; set; }

        public override void Write(ref Packet p)
        {
            p.WriteInt(Code);
            p.WriteBool(Open);
        }
        public override void Read(ref Packet p)
        {
            Code = p.ReadInt();
            Open = p.ReadBool();
        }
        protected override void Release() { }
        protected override void Dispose(bool disposing) { base.Dispose(disposing); }
    }
}
