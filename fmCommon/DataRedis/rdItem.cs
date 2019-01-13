using System.Collections.Generic;
//using System.Web.Script.Serialization;

namespace fmCommon
{
    public interface ISlot
    {
        int Slot { get; set; }
        bool Equip { get; set; }
    }

    public class rdItem : RedisData , ISlot
    {
        public int Slot{ get; set; }
        public int Lv { get; set; }
        public eGrade Grade { get; set; }
        public eParts Parts { get; set; }
        public int Code { get; set; }

        public bool Equip { get; set; }

        public List<rdOption> BaseOpt { get; set; }

        public List<rdOption> AddOpts { get; set; }

        public eBeyond Beyond { get; set; }
        //public bool Ancient { get; set; }

        public override void Write(ref Packet p)
        {
            p.WriteInt(Slot);
            p.WriteInt(Lv);
            p.WriteInt((int)Grade);
            p.WriteInt((int)Parts);
            p.WriteInt(Code);

            p.WriteBool(Equip);

            if (null == BaseOpt)
                BaseOpt = new List<rdOption>();
            BaseOpt.Write(ref p);

            if (null == AddOpts)
                AddOpts = new List<rdOption>();
            AddOpts.Write(ref p);

            //p.WriteBool(Ancient);
            p.WriteInt((int)Beyond);
        }

        public override void Read(ref Packet p)
        {
            Slot = p.ReadInt();
            Lv = p.ReadInt();
            Grade = (eGrade)p.ReadInt();
            Parts = (eParts)p.ReadInt();
            Code = p.ReadInt();

            Equip = p.ReadBool();


            if (null == BaseOpt)
                BaseOpt = new List<rdOption>();
            BaseOpt.Read(ref p);

            if (null == AddOpts)
                AddOpts = new List<rdOption>();
            AddOpts.Read(ref p);

            //Ancient = p.ReadBool();
            Beyond = (eBeyond)p.ReadInt();
        }

        protected override void Release()
        {
            BaseOpt = null;
            AddOpts = null;
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
