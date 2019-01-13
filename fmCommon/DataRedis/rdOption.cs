
//using System.Web.Script.Serialization;

namespace fmCommon
{
    public class rdOption : RedisData
    {
        public int Index { get; set; }
        public bool Remelt { get; set; }
        public eOptGrade Grade { get; set; }
        public eOption Kind { get; set; }
        public float Value { get; set; }


        public rdOption() { }

        public rdOption(int index, bool remelt, eOptGrade grade, eOption option, float value)
        {
            Index = index;
            Remelt = remelt;
            Grade = grade;
            Kind = option;
            Value = value;
        }

        public override void Write(ref Packet p)
        {
            p.WriteInt(Index);
            p.WriteBool(Remelt);
            p.WriteInt((int)Grade);
            p.WriteInt((int)Kind);
            p.WriteFloat(Value);
        }

        public override void Read(ref Packet p)
        {
            Index = p.ReadInt();
            Remelt = p.ReadBool();
            Grade = (eOptGrade)p.ReadInt();
            Kind = (eOption)p.ReadInt();
            Value = p.ReadFloat();
        }

        protected override void Release()
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        //public override string ToJson()
        //{
        //    return new JavaScriptSerializer().Serialize(this);
        //}
    }
}
