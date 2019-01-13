using System;

namespace fmCommon.Formula
{
    public static partial class theFormula
    {
        //public static int ExtraAtkChanceRate { get { return 1; } }
        public static int CrushingBlowRate { get { return 10; } }
        public static int SturnRate { get { return 8; } }
        //public static int ExtraDMGToRareMon { get { return 15; } }
        //public static int DMGReduceRate { get { return 10; } }
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
            //int hit = (int)Math.Round(2.5f * lv);
            //int inc = (int)Math.Round(lv * 0.15f * (int)rareLv * 10);
            //int extra = extraLv * 28;
            ////return (33 + hit + inc + extra);
            //return (33 + hit + inc + extra) * 2;

            int nRareLv = (int)rareLv;

            //int hit = (int)Math.Round(1.7 * lv);
            //int inc = ((nRareLv - 1) * (nRareLv - 1) * nRareLv * lv);
            //int extra = extraLv * 300;
            int inc = ((nRareLv - 1) * lv);
            int extra = extraLv * 100;

            //return (33 + hit + inc + extra) * 2 * 10000;
            int exp = (int)Math.Round((33 + lv + inc + extra) * (1 + rate * 0.01) * 1.8);

            return exp;
        }

        public static int GetGold(float dropRate, eRareLv rareLv, int lv)
        {
            return (int)(((1 + dropRate * 0.01) * (((int)rareLv) * 0.5) * lv) + 45);
            //return (int)(((1 + dropRate * 0.01) * (((int)rareLv) * 0.4) * 4 * lv) + 45);
        }

        public static int ModifyMissionExp(int lv, int exp)
        {
            int modify = lv / 5;
            int modifyLv = 5 * modify;
            if (modifyLv <= 0) modifyLv = 1;

            int modifyExp = (int)Math.Round(exp * 0.01 * (2 * modifyLv));

            return modifyExp <= 0 ? 1 : modifyExp;
        }


        //public static int NeedRemeltStone(int itemLv, eBeyond beyond)
        //{
        //    if (beyond == eBeyond.None)
        //    {
        //        return 5;
        //    }
        //    else
        //    {
        //        if (itemLv <= 20)
        //            return 1;
        //        else if (20 < itemLv && itemLv <= 40)
        //            return 2;
        //        else
        //            return 3;
        //    }
        //}

        //public static int NeedRemeltGold(int itemLv, eBeyond beyond)
        //{
        //    if (beyond == eBeyond.None)
        //    {
        //        return 2000;
        //    }
        //    else
        //    {
        //        int gold = 100;
        //        int cnt = 1;

        //        if (itemLv <= 20)
        //            cnt = 1;
        //        else if (20 < itemLv && itemLv <= 40)
        //            cnt = 2;
        //        else
        //            cnt = 3;

        //        switch (cnt)
        //        {
        //            case 0: break;
        //            case 1: gold = gold * (cnt + 1); break;
        //            case 2: gold = gold * (cnt + 3); break;
        //            case 3: gold = gold * (cnt + 5); break;
        //            default:
        //                break;
        //        }

        //        return gold;
        //    }
        //}

        //public static int NeedRerollStone()
        //{
        //    return 1;
        //    //if (lv <= 20) return 1;
        //    //else if (20 < lv && lv <= 40) return 2;
        //    //else return 3;
        //}

        //public static int NeedRerollGold(int optCnt)
        //{
        //    int gold = 200;
        //    //return 200;
        //    switch (optCnt)
        //    {
        //        case 0: break;
        //        case 1: gold = gold * (optCnt + 3); break;
        //        case 2: gold = gold * (optCnt + 7); break;
        //        case 3: gold = gold * (optCnt + 13); break;
        //        case 4: gold = gold * (optCnt + 21); break;
        //        default:
        //            break;
        //    }

        //    return gold;
        //}

        //public static int NeedRerollRuby(eGrade grade)
        //{
        //    int ruby = 0;
        //    //return 200;
        //    switch (grade)
        //    {
        //        case eGrade.None: break;
        //        case eGrade.Normal: ruby = 1; break;
        //        case eGrade.Magic: ruby = 2; break;
        //        case eGrade.Rare: ruby = 3; break;
        //        case eGrade.Epic: ruby = 4; break;
        //        default:
        //            break;
        //    }

        //    return ruby;
        //}

        //public static int RefreshSeal { get { return 2; } }
        //public static int NeedEnchantStone()
        //{
        //    return 2;
        //}

        //public static int NeedEnchantGold(int optCnt)
        //{
        //    int gold = 300;
        //    switch (optCnt)
        //    {
        //        case 0: break;
        //        case 1: gold = gold * (optCnt + 3); break;
        //        case 2: gold = gold * (optCnt + 7); break;
        //        case 3: gold = gold * (optCnt + 13); break;
        //        case 4: gold = gold * (optCnt + 21); break;
        //        default:
        //            break;
        //    }

        //    return gold;
        //}

        //public static int NeedEnchantRuby(eGrade grade)
        //{
        //    int ruby = 0;
        //    switch (grade)
        //    {
        //        case eGrade.None: break;
        //        case eGrade.Normal: ruby = 2; break;
        //        case eGrade.Magic: ruby = 3; break;
        //        case eGrade.Rare: ruby = 5; break;
        //        case eGrade.Epic: ruby = 9; break;
        //        default:
        //            break;
        //    }

        //    return ruby;
        //}

        //public static int NeedSeal(eParts parts, int lv)
        //{
        //    int seal = 0;

        //    if (lv <= 30)
        //        seal = 2;
        //    else if (30 < lv && lv <= 40)
        //        seal = 4;
        //    else if (40 < lv && lv <= 50)
        //        seal = 6;
        //    else if (50 < lv && lv <= 60)
        //        seal = 8;
        //    else if (60 < lv && lv <= 70)
        //        seal = 10;


        //    switch (parts)
        //    {
        //        case eParts.None: seal = seal / 2; break;
        //        default:
        //            break;
        //    }

        //    return seal;
        //}

    }
}
