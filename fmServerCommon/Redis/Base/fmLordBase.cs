using fmCommon;
using System;
using System.Collections.Generic;

namespace fmServerCommon
{
    public class fmLordBase
    {
        public bool Block { get; set; }

        public string Name { get; set; }
        public int Lv { get; set; }
        public long Exp { get; set; }
        public long Gold { get; set; }

        public int GameRuby { get; set; }
        public int AccRuby { get; set; }

        public int Stone { get; set; }
        //public int Ticket { get; set; }
        public int Floor { get; set; }
        public int PvpPoint { get; set; }
        public int DTombCnt { get; set; }
        public bool Payment { get; set; }
        public int SCKey { get; set; }

        public rdLordInfo ToClient()
        {
            return new rdLordInfo
            {
                Name = Name,
                Lv = Lv,
                Exp = Exp,
                Gold = Gold,
                Ruby = GameRuby + AccRuby,
                Stone = Stone,
                //Ticket = Ticket,
                Floor = Floor,
                PvpPoint = PvpPoint,
                DTombCnt = DTombCnt,
                Payment = Payment,
                SCKey = SCKey,
            };
        }

        public bool TryModifyInfomation(List<fmOpInfo> list)
        {
            if (null == list)
                return false;

            foreach (var node in list)
            {
                if (node.Finance == eFinance.Gold)
                    Gold += (node.Amount);
                else if (node.Finance == eFinance.Ruby)
                    AccRuby += (node.Amount);
                else if (node.Finance == eFinance.Stone)
                    Stone += (node.Amount);
                else if (node.Finance == eFinance.PvpPoint)
                    PvpPoint += (node.Amount);
                //else if (node.Finance == eFinance.DHeart)
                //    DHeartFnc += (node.Amount);
                //else if (node.Finance == eFinance.Seal)
                //    Seal += (node.Amount);
            }

            return true;
        }
    }
}
