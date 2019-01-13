namespace fmCommon
{
    public class rdMissionBase : RedisData
    {
        public int RefreshCnt { get; set; }
        public int RemainSec { get; set; }

        public override void Write(ref Packet p)
        {
            p.WriteInt(RefreshCnt);
            p.WriteInt(RemainSec);
        }
        public override void Read(ref Packet p)
        {
            RefreshCnt = p.ReadInt();
            RemainSec = p.ReadInt();
        }
        protected override void Release() { }
        protected override void Dispose(bool disposing) { base.Dispose(disposing); }

        //public override string ToJson()
        //{
        //    return new JavaScriptSerializer().Serialize(this);
        //}
    }
}
