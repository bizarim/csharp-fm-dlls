//using System.Collections.Generic;
//using System.Linq;

//namespace fmCommon
//{
//    public class rdEnchantList : RedisData
//    {
//        public bool Exist { get; set; }
//        public int ItemSlot { get; set; }
//        //public int OptIdx { get; set; }

//        public List<eOption> List = new List<eOption>();

//        public override void Write(ref Packet p)
//        {
//            p.WriteBool(Exist);
//            p.WriteInt(ItemSlot);
//            //p.WriteInt(OptIdx);
//            List.Write(ref p);
//        }

//        public override void Read(ref Packet p)
//        {
//            Exist = p.ReadBool();
//            ItemSlot = p.ReadInt();
//            //OptIdx = p.ReadInt();
//            List.Read(ref p);
//        }
//        protected override void Release() { }
//        protected override void Dispose(bool disposing) { base.Dispose(disposing); }


//        public override object Clone()
//        {
//            return new rdEnchantList
//            {
//                Exist = Exist,
//                ItemSlot = ItemSlot,
//                //OptIdx = OptIdx,
//                List = List.ToList(),
//            };
//        }
//        //public override string ToJson()
//        //{
//        //    return new JavaScriptSerializer().Serialize(this);
//        //}
//    }
//}
