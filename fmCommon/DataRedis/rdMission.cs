//using System.Web.Script.Serialization;

namespace fmCommon
{
    public class rdMission : RedisData
    {
        public eMission Type { get; set; }
        public int Code { get; set; }
        public int Condition { get; set; }
        public bool Complete { get; set; }

        public override void Write(ref Packet p)
        {
            p.WriteInt((int)Type);
            p.WriteInt(Code);
            p.WriteInt(Condition);
            p.WriteBool(Complete);
        }
        public override void Read(ref Packet p)
        {
            Type = (eMission)p.ReadInt();
            Code = p.ReadInt();
            Condition = p.ReadInt();
            Complete = p.ReadBool();
        }
        protected override void Release() { }
        protected override void Dispose(bool disposing) { base.Dispose(disposing); }

        //public override string ToJson()
        //{
        //    return new JavaScriptSerializer().Serialize(this);
        //}
    }
}
