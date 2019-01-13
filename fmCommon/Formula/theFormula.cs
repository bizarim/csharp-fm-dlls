using System;

namespace fmCommon.Formula
{
    public static partial class theFormula
    {
        public static int CrushingBlowRate { get { return 10; } }
        public static int SturnRate { get { return 8; } }
    }

    public static partial class theFormula
    {
        public static int NeedRubyDoubleRound { get { return 1; } }
        public static int NeedStoneToRemoveRemelt { get { return 50; } }

        public static int RateCombineEpic { get { return 100; } }
        public static int RateCombineRare { get { return 90; } }
        public static int RateCombineMagic { get { return 80; } }
        public static int RateCombineNormal { get { return 75; } }

        public static int SellPrice { get { return 8; } }

        public static int NeedGoldToCombine { get { return 500; } }
        public static int NeedStoneToCombine { get { return 1; } }

        public static int NeedGoldUpToAncient { get { return 2000; } }
        public static int NeedStoneUpToAncient { get { return 4; } }

        public static int NeedGoldToCreateMythic { get { return 10000; } }
        public static int NeedStoneToCreateMythic { get { return 20; } }

        public static int NeedGoldAddOptToMythic { get { return 10000; } }
        public static int NeedStoneAddOptToMythic { get { return 20; } }
        

        //---- Refresh
        public static int RefreshMissionRuby { get { return 3; } }
        public static int RefreshShorcutRuby { get { return 1; } }

        public static int RefreshDTombRuby { get { return 3; } }
        public static int RefreshDTombGold { get { return 6000; } }

        public static int RefreshInDunRuby(int code)
        {
            int remain = code % 1000;
            int cost = 0;
            switch (remain)
            {
                case 1: cost = 0; break;
                case 2: cost = 0; break;
                case 3: cost = 1; break;
                case 4: cost = 2; break;
                case 5: cost = 3; break;
                default:
                    cost = 9;
                    break;
            }

            return cost;
        }
        public static int RefreshInDunGold(int code)
        {
            int remain = code % 1000;
            int cost = 0;
            switch (remain)
            {
                case 1: cost = 50; break;
                case 2: cost = 500; break;
                case 3: cost = 2000; break;
                case 4: cost = 4000; break;
                case 5: cost = 6000; break;
                default:
                    cost = 9999;
                    break;
            }

            return cost;
        }

        public static int RemeltRuby(int itemLv, eBeyond beyond)
        {
            int cost = 1;

            switch (beyond)
            {
                case eBeyond.None:      cost = 1; break;
                case eBeyond.Ancient:   cost = 2; break;
                case eBeyond.Mythic:    cost = 4; break;
                default:
                    cost = 9999;
                    break;
            }

            return cost;
        }
    }

    // 서버 클라 공통으로 사용 하는 공식
    public static partial class theFormula
    {

        public static int GetdropLimitValue(float itemDropRate, eRareLv rareLv)
        {
            int hit = (int)(itemDropRate * 0.25) + (((int)rareLv - 1) * 50);
            if (400 < hit)
                hit = 400;

            return hit;
        }

        public static int GetMagicChanceValue(float findMagicItemRate, eRareLv rareLv)
        {
            return (int)(findMagicItemRate) + (((int)rareLv - 1) * 50);
        }

        public static int GetExp(eRareLv rareLv, int lv, int extraLv, float rate)
        {
            int nRareLv = (int)rareLv;
            int inc = ((nRareLv - 1) * lv);
            int extra = extraLv * 100;
            int exp = (int)Math.Round((33 + lv + inc + extra) * (1 + rate * 0.01) * 1.8);
            return exp;
        }

        public static int GetGold(float dropRate, eRareLv rareLv, int lv)
        {
            return (int)(((1 + dropRate * 0.01) * (((int)rareLv) * 0.5) * lv) + 45);
        }

        public static int ModifyMissionExp(int lv, int exp)
        {
            int modify = lv / 5;
            int modifyLv = 5 * modify;
            if (modifyLv <= 0) modifyLv = 1;

            int modifyExp = (int)Math.Round(exp * 0.01 * (2 * modifyLv));

            return modifyExp <= 0 ? 1 : modifyExp;
        }
        
    }
}
