namespace fmCommon
{
    public class rdStat : RedisData
    {
        //public int TotalPoint { get; set; }
        public int Point { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }

        public override void Write(ref Packet p)
        {
            //p.WriteInt(TotalPoint);
            p.WriteInt(Point);
            p.WriteInt(Atk);
            p.WriteInt(Def);
            p.WriteInt(Hp );
        }

        public override void Read(ref Packet p)
        {
            //TotalPoint = p.ReadInt();
            Point = p.ReadInt();
            Atk = p.ReadInt();
            Def = p.ReadInt();
            Hp = p.ReadInt();
        }

        protected override void Release()
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override object Clone()
        {
            return new rdStat
            {
                //TotalPoint = TotalPoint,
                Point = Point,
                Atk = Atk,
                Def = Def,
                Hp = Hp
            };
        }

        //public override string ToJson()
        //{
        //    return new JavaScriptSerializer().Serialize(this);
        //}
    }
}

