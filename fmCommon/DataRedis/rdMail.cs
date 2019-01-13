namespace fmCommon
{
    public class rdMail : RedisData
    {
        public string Name { get; set; }
        public eBattleResult Result { get; set; }
        public int Point { get; set; }

        public override void Write(ref Packet p)
        {
            p.WriteString(Name);
            p.WriteInt((int)Result);
            p.WriteInt(Point);
            
        }
        public override void Read(ref Packet p)
        {
            Name = p.ReadString();
            Result = (eBattleResult)p.ReadInt();
            Point = p.ReadInt();
            
        }
        protected override void Release() { }
        protected override void Dispose(bool disposing) { base.Dispose(disposing); }
    }
}
