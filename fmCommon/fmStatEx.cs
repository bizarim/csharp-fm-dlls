using fmCommon.Battle;
using System;
using System.Collections.Generic;

namespace fmCommon
{
    public static partial class fmStatEx
    {
        public static int BaseHp { get { return 310; } }
        public static int BaseDef { get { return 5; } }

        public static float CriRate { get { return 5; } }
        public static float CriDamageRate { get { return 50; } }

        public static int IncWDPerPoint { get { return 15; } }   //10
        public static int IncDefPerPoint { get { return 10; } } //3
        public static int IncHpPerPoint { get { return 50; } }  //40

        //public static int IncWDPerPoint { get { return 8; } }   //8
        //public static int IncDefPerPoint { get { return 10; } } //12
        //public static int IncHpPerPoint { get { return 40; } }  //15

        private static bool m_bInit = false;
        private static Dictionary<eOption, List<fmSetEffect>> m_dic = new Dictionary<eOption, List<fmSetEffect>>();

        private static void Load()
        {
            m_bInit = true;
            m_dic.Clear();
            
            m_dic.Add(eOption.SETAttack, new List<fmSetEffect>());
            m_dic[eOption.SETAttack].Clear();
            m_dic[eOption.SETAttack].Add(new fmSetEffect { SetOpt = eOption.SETAttack, SetCnt = 2, AddOpt = eOption.CriRate, Value = 25f });
            m_dic[eOption.SETAttack].Add(new fmSetEffect { SetOpt = eOption.SETAttack, SetCnt = 4, AddOpt = eOption.ASRate, Value = 50f });
            m_dic[eOption.SETAttack].Add(new fmSetEffect { SetOpt = eOption.SETAttack, SetCnt = 6, AddOpt = eOption.WDRate, Value = 75f });

            //m_dic[eOption.SETLuck].Add(new fmSetEffect { SetOpt = eOption.SETLuck, SetCnt = 2, AddOpt = eOption.HP, Value = 300 });
            m_dic.Add(eOption.SETLuck, new List<fmSetEffect>());
            m_dic[eOption.SETLuck].Clear();
            m_dic[eOption.SETLuck].Add(new fmSetEffect { SetOpt = eOption.SETLuck, SetCnt = 2, AddOpt = eOption.GoldDropRate, Value = 100f });
            m_dic[eOption.SETLuck].Add(new fmSetEffect { SetOpt = eOption.SETLuck, SetCnt = 4, AddOpt = eOption.ItemDropRate, Value = 150f });

            //SETFindMagicItemRate
            m_dic.Add(eOption.SETFindMagicItemRate, new List<fmSetEffect>());
            m_dic[eOption.SETFindMagicItemRate].Clear();
            m_dic[eOption.SETFindMagicItemRate].Add(new fmSetEffect { SetOpt = eOption.SETFindMagicItemRate, SetCnt = 2, AddOpt = eOption.ItemDropRate, Value = 50f });
            m_dic[eOption.SETFindMagicItemRate].Add(new fmSetEffect { SetOpt = eOption.SETFindMagicItemRate, SetCnt = 4, AddOpt = eOption.FindMagicItemRate, Value = 200f });

            //SETEXPRate
            m_dic.Add(eOption.SETExpRate, new List<fmSetEffect>());
            m_dic[eOption.SETExpRate].Clear();
            m_dic[eOption.SETExpRate].Add(new fmSetEffect { SetOpt = eOption.SETExpRate, SetCnt = 2, AddOpt = eOption.EXPRate, Value = 100f });

            //SETResistFireRate
            //SETResistIceRate
            //SETResistNatureRate
            //SETResistNoneRate
            m_dic.Add(eOption.SETFire, new List<fmSetEffect>());
            m_dic[eOption.SETFire].Clear();
            m_dic[eOption.SETFire].Add(new fmSetEffect { SetOpt = eOption.SETFire, SetCnt = 2, AddOpt = eOption.EDRateFire, Value = 100f });
            m_dic[eOption.SETFire].Add(new fmSetEffect { SetOpt = eOption.SETFire, SetCnt = 4, AddOpt = eOption.AcAllResistRate, Value = 150f });

            m_dic.Add(eOption.SETIce, new List<fmSetEffect>());
            m_dic[eOption.SETIce].Clear();
            m_dic[eOption.SETIce].Add(new fmSetEffect { SetOpt = eOption.SETIce, SetCnt = 2, AddOpt = eOption.EDRateIce, Value = 100f });
            m_dic[eOption.SETIce].Add(new fmSetEffect { SetOpt = eOption.SETIce, SetCnt = 4, AddOpt = eOption.AcAllResistRate, Value = 150f });

            m_dic.Add(eOption.SETNature, new List<fmSetEffect>());
            m_dic[eOption.SETNature].Clear();
            m_dic[eOption.SETNature].Add(new fmSetEffect { SetOpt = eOption.SETNature, SetCnt = 2, AddOpt = eOption.EDRateNature, Value = 100f });
            m_dic[eOption.SETNature].Add(new fmSetEffect { SetOpt = eOption.SETNature, SetCnt = 4, AddOpt = eOption.AcAllResistRate, Value = 150f });

            m_dic.Add(eOption.SETNone, new List<fmSetEffect>());
            m_dic[eOption.SETNone].Clear();
            m_dic[eOption.SETNone].Add(new fmSetEffect { SetOpt = eOption.SETNone, SetCnt = 2, AddOpt = eOption.EDRateNone, Value = 100f });
            m_dic[eOption.SETNone].Add(new fmSetEffect { SetOpt = eOption.SETNone, SetCnt = 4, AddOpt = eOption.AcAllResistRate, Value = 150f });


            ////SETThorn
            //m_dic.Add(eOption.SETThorn, new List<fmSetEffect>());
            //m_dic[eOption.SETThorn].Clear();
            //m_dic[eOption.SETThorn].Add(new fmSetEffect { SetOpt = eOption.SETThorn, SetCnt = 2, AddOpt = eOption.ThornRate,    Value = 100 });
            //m_dic[eOption.SETThorn].Add(new fmSetEffect { SetOpt = eOption.SETThorn, SetCnt = 4, AddOpt = eOption.ThornRate,    Value = 300 });
            //m_dic[eOption.SETThorn].Add(new fmSetEffect { SetOpt = eOption.SETThorn, SetCnt = 6, AddOpt = eOption.ThornRate,    Value = 500 });
            ////SETPoison
            //m_dic.Add(eOption.SETPoison, new List<fmSetEffect>());
            //m_dic[eOption.SETPoison].Clear();
            //m_dic[eOption.SETPoison].Add(new fmSetEffect { SetOpt = eOption.SETPoison, SetCnt = 2, AddOpt = eOption.PoisonRate,    Value = 200 });
            //m_dic[eOption.SETPoison].Add(new fmSetEffect { SetOpt = eOption.SETPoison, SetCnt = 4, AddOpt = eOption.PoisonRate,    Value = 400 });
            //m_dic[eOption.SETPoison].Add(new fmSetEffect { SetOpt = eOption.SETPoison, SetCnt = 6, AddOpt = eOption.PoisonRate,    Value = 800 });

            ////SETBurn
            //m_dic.Add(eOption.SETBurn, new List<fmSetEffect>());
            //m_dic[eOption.SETBurn].Clear();
            //m_dic[eOption.SETBurn].Add(new fmSetEffect { SetOpt = eOption.SETBurn, SetCnt = 2, AddOpt = eOption.BurnRate, Value = 100 });
            //m_dic[eOption.SETBurn].Add(new fmSetEffect { SetOpt = eOption.SETBurn, SetCnt = 4, AddOpt = eOption.BurnRate, Value = 300 });
            //m_dic[eOption.SETBurn].Add(new fmSetEffect { SetOpt = eOption.SETBurn, SetCnt = 6, AddOpt = eOption.BurnRate, Value = 500 });
            ////SETFreeze
            //m_dic.Add(eOption.SETFreeze, new List<fmSetEffect>());
            //m_dic[eOption.SETFreeze].Clear();
            //m_dic[eOption.SETFreeze].Add(new fmSetEffect { SetOpt = eOption.SETFreeze, SetCnt = 2, AddOpt = eOption.FreezeRate, Value = 6 });
            //m_dic[eOption.SETFreeze].Add(new fmSetEffect { SetOpt = eOption.SETFreeze, SetCnt = 4, AddOpt = eOption.FreezeRate, Value = 12 });
            //m_dic[eOption.SETFreeze].Add(new fmSetEffect { SetOpt = eOption.SETFreeze, SetCnt = 6, AddOpt = eOption.FreezeRate, Value = 18 });

            //SETThorn
            m_dic.Add(eOption.SETThorn, new List<fmSetEffect>());
            m_dic[eOption.SETThorn].Clear();
            m_dic[eOption.SETThorn].Add(new fmSetEffect { SetOpt = eOption.SETThorn, SetCnt = 2, AddOpt = eOption.ThornRate, Value = 100 });
            m_dic[eOption.SETThorn].Add(new fmSetEffect { SetOpt = eOption.SETThorn, SetCnt = 4, AddOpt = eOption.ThornRate, Value = 200 });
            m_dic[eOption.SETThorn].Add(new fmSetEffect { SetOpt = eOption.SETThorn, SetCnt = 6, AddOpt = eOption.ThornRate, Value = 400 });
            //SETPoison
            m_dic.Add(eOption.SETPoison, new List<fmSetEffect>());
            m_dic[eOption.SETPoison].Clear();
            m_dic[eOption.SETPoison].Add(new fmSetEffect { SetOpt = eOption.SETPoison, SetCnt = 2, AddOpt = eOption.PoisonRate, Value = 100 });
            m_dic[eOption.SETPoison].Add(new fmSetEffect { SetOpt = eOption.SETPoison, SetCnt = 4, AddOpt = eOption.PoisonRate, Value = 200 });
            m_dic[eOption.SETPoison].Add(new fmSetEffect { SetOpt = eOption.SETPoison, SetCnt = 6, AddOpt = eOption.PoisonRate, Value = 400 });

            //SETBurn
            m_dic.Add(eOption.SETBurn, new List<fmSetEffect>());
            m_dic[eOption.SETBurn].Clear();
            m_dic[eOption.SETBurn].Add(new fmSetEffect { SetOpt = eOption.SETBurn, SetCnt = 2, AddOpt = eOption.BurnRate, Value = 100 });
            m_dic[eOption.SETBurn].Add(new fmSetEffect { SetOpt = eOption.SETBurn, SetCnt = 4, AddOpt = eOption.BurnRate, Value = 200 });
            m_dic[eOption.SETBurn].Add(new fmSetEffect { SetOpt = eOption.SETBurn, SetCnt = 6, AddOpt = eOption.BurnRate, Value = 400 });

            //SETFreeze
            m_dic.Add(eOption.SETFreeze, new List<fmSetEffect>());
            m_dic[eOption.SETFreeze].Clear();
            m_dic[eOption.SETFreeze].Add(new fmSetEffect { SetOpt = eOption.SETFreeze, SetCnt = 2, AddOpt = eOption.FreezeRate, Value = 6 });
            m_dic[eOption.SETFreeze].Add(new fmSetEffect { SetOpt = eOption.SETFreeze, SetCnt = 4, AddOpt = eOption.FreezeRate, Value = 12 });
            m_dic[eOption.SETFreeze].Add(new fmSetEffect { SetOpt = eOption.SETFreeze, SetCnt = 6, AddOpt = eOption.FreezeRate, Value = 18 });

            //SETExtraStone
            m_dic.Add(eOption.SETExtraStone, new List<fmSetEffect>());
            m_dic[eOption.SETExtraStone].Clear();
            m_dic[eOption.SETExtraStone].Add(new fmSetEffect { SetOpt = eOption.SETExtraStone, SetCnt = 3, AddOpt = eOption.ExtraStone, Value = 1 });

            //SETSturn
            m_dic.Add(eOption.SETRecovery, new List<fmSetEffect>());
            m_dic[eOption.SETRecovery].Clear();
            m_dic[eOption.SETRecovery].Add(new fmSetEffect { SetOpt = eOption.SETRecovery, SetCnt = 2, AddOpt = eOption.Sturn, Value = 1 });
            m_dic[eOption.SETRecovery].Add(new fmSetEffect { SetOpt = eOption.SETRecovery, SetCnt = 3, AddOpt = eOption.Recovery, Value = 50 });
            m_dic[eOption.SETRecovery].Add(new fmSetEffect { SetOpt = eOption.SETRecovery, SetCnt = 4, AddOpt = eOption.Recovery, Value = 100 });
            m_dic[eOption.SETRecovery].Add(new fmSetEffect { SetOpt = eOption.SETRecovery, SetCnt = 5, AddOpt = eOption.Recovery, Value = 150 });
            m_dic[eOption.SETRecovery].Add(new fmSetEffect { SetOpt = eOption.SETRecovery, SetCnt = 6, AddOpt = eOption.Recovery, Value = 200 });
            //m_dic[eOption.SETRecovery].Add(new fmSetEffect { SetOpt = eOption.SETRecovery, SetCnt = 6, AddOpt = eOption.RecoveryRate, Value = 30 });

            m_dic.Add(eOption.SETHP, new List<fmSetEffect>());
            m_dic[eOption.SETHP].Clear();
            m_dic[eOption.SETHP].Add(new fmSetEffect { SetOpt = eOption.SETHP, SetCnt = 2, AddOpt = eOption.Sturn, Value = 1 });
            m_dic[eOption.SETHP].Add(new fmSetEffect { SetOpt = eOption.SETHP, SetCnt = 4, AddOpt = eOption.HP, Value = 1500 });
            m_dic[eOption.SETHP].Add(new fmSetEffect { SetOpt = eOption.SETHP, SetCnt = 6, AddOpt = eOption.HP, Value = 2500 });


            // T3
            m_dic.Add(eOption.SETFireT3, new List<fmSetEffect>());
            m_dic[eOption.SETFireT3].Clear();
            m_dic[eOption.SETFireT3].Add(new fmSetEffect { SetOpt = eOption.SETFireT3, SetCnt = 2, AddOpt = eOption.EDRateFire, Value = 200f });
            m_dic[eOption.SETFireT3].Add(new fmSetEffect { SetOpt = eOption.SETFireT3, SetCnt = 4, AddOpt = eOption.LegnendDMGReduceRate, Value = 20f });
            m_dic[eOption.SETFireT3].Add(new fmSetEffect { SetOpt = eOption.SETFireT3, SetCnt = 6, AddOpt = eOption.BurnRate, Value = 300f });

            m_dic.Add(eOption.SETIceT3, new List<fmSetEffect>());
            m_dic[eOption.SETIceT3].Clear();
            m_dic[eOption.SETIceT3].Add(new fmSetEffect { SetOpt = eOption.SETIceT3, SetCnt = 2, AddOpt = eOption.EDRateIce, Value = 200f });
            m_dic[eOption.SETIceT3].Add(new fmSetEffect { SetOpt = eOption.SETIceT3, SetCnt = 4, AddOpt = eOption.LegnendDMGReduceRate, Value = 20f });
            m_dic[eOption.SETIceT3].Add(new fmSetEffect { SetOpt = eOption.SETIceT3, SetCnt = 6, AddOpt = eOption.FreezeRate, Value = 10 });

            m_dic.Add(eOption.SETNatureT3, new List<fmSetEffect>());
            m_dic[eOption.SETNatureT3].Clear();
            m_dic[eOption.SETNatureT3].Add(new fmSetEffect { SetOpt = eOption.SETNatureT3, SetCnt = 2, AddOpt = eOption.EDRateNature, Value = 200f });
            m_dic[eOption.SETNatureT3].Add(new fmSetEffect { SetOpt = eOption.SETNatureT3, SetCnt = 4, AddOpt = eOption.LegnendDMGReduceRate, Value = 20f });
            m_dic[eOption.SETNatureT3].Add(new fmSetEffect { SetOpt = eOption.SETNatureT3, SetCnt = 6, AddOpt = eOption.PoisonRate, Value = 300f });

            m_dic.Add(eOption.SETNoneT3, new List<fmSetEffect>());
            m_dic[eOption.SETNoneT3].Clear();
            m_dic[eOption.SETNoneT3].Add(new fmSetEffect { SetOpt = eOption.SETNoneT3, SetCnt = 2, AddOpt = eOption.EDRateNone, Value = 200f });
            m_dic[eOption.SETNoneT3].Add(new fmSetEffect { SetOpt = eOption.SETNoneT3, SetCnt = 4, AddOpt = eOption.LegnendDMGReduceRate, Value = 20f });
            m_dic[eOption.SETNoneT3].Add(new fmSetEffect { SetOpt = eOption.SETNoneT3, SetCnt = 6, AddOpt = eOption.ThornRate, Value = 300f });
        }

        public static bool LiftSetEffect(this fmStats stats)
        {
            if (false == m_bInit) Load();

            Dictionary<eOption, fmOption> dic = null;
            if (false == stats.TryGetRefDic(out dic))
                return false;

            foreach (var opt in m_dic.Keys)
            {
                if (true == dic.ContainsKey(opt))
                {
                    if (dic[opt].Value <= 0)
                        continue;

                    int cnt = (int)dic[opt].Value + (int)stats.PlusSetEffect;

                    foreach (var data in m_dic[opt])
                    {
                        if (data.SetCnt <= cnt)
                        {
                            stats.DecSetEffect(data);
                        }
                    }
                }
            }

            return true;
        }

        public static bool ApplySetEffect(this fmStats stats)
        {
            if (false == m_bInit) Load();

            Dictionary<eOption, fmOption> dic = null;
            if (false == stats.TryGetRefDic(out dic))
                return false;

            foreach (var opt in m_dic.Keys)
            {
                if (true == dic.ContainsKey(opt))
                {
                    if (dic[opt].Value <= 0)
                        continue;

                    int cnt = (int)dic[opt].Value + (int)stats.PlusSetEffect;

                    foreach (var data in m_dic[opt])
                    {
                        if (data.SetCnt <= cnt)
                        {
                            stats.IncSetEffect(data);
                        }
                    }
                }
            }
            
            return true;
        }

        public static bool TryGetRefList(eOption opt, out List<fmSetEffect> list)
        {
            if (false == m_bInit) Load();

            list = null;

            if (false == m_dic.ContainsKey(opt))
                return false;

            list = m_dic[opt];

            return true;
        }
    }

    public static partial class fmStatEx
    {
        private static fmStats CreateMonsterStat(eUnit unit, eTrait trait, eRareLv eRare, int lv, int extarLv, eElement element)
        {
            return new fmBaseMonster().SetupStats(unit, trait, eRare, lv, extarLv, element);
        }
    }

    class fmBaseMonster
    {
        private readonly int BaseBWDMin = 12;
        private readonly int BaseBWDMax = 15;
        private readonly int BaseAS = 1;
        private readonly float BaseCriRate = 18f;
        private readonly float BaseCriDamageRate = 50f;
        private readonly int BaseHp = 60;
        private readonly int BaseDEF = 18;
        private readonly int BaseResistAll = 15;
        private readonly int BaseED = 15;
        private readonly int BaseResist = 23;
        private readonly float BaseMulipy = 0.025f;

        public fmStats SetupStats(eUnit unit, eTrait trait, eRareLv eRare, int lv, int extraLv, eElement element)
        {
            fmStats stats = new fmStats();
            SetBaseStats(stats, lv, element);
            SetUnitSetRareLv(stats, unit, eRare);
            SetTrait(stats, trait);
            Multiply(stats, extraLv);
            return stats;
        }

        private void SetBaseStats(fmStats stats, int lv, eElement element)
        {
            stats.Lv = lv;
            stats.Element = element;

            stats.AS = BaseAS;
            stats.CriRate = BaseCriRate;
            stats.CriDamageRate = BaseCriDamageRate;

            stats.BWDMin = (int)Math.Round(0.2 * BaseBWDMin * 1.2 * (1.15 + 0.07 * (lv + 55) + lv * lv * (lv - 1) * 0.00075));
            stats.BWDMax = (int)Math.Round(0.3 * BaseBWDMax * 1.2 * (1.15 + 0.07 * (lv + 55) + lv * lv * (lv - 1) * 0.00075));
            stats.HP = (int)Math.Round(BaseHp * (1.3 + 0.037 * (lv + 70) + lv * (lv - 1) * 0.075));
            stats.DEF = (int)Math.Round((BaseDEF * 1.69 * (lv - 1)) + 1);
            stats.ResistAll = (int)Math.Round((BaseResistAll * 1.69 * (lv - 1)) + 1);

            int tempED = (int)Math.Round( BaseED * 0.95 * (1.15 + 0.07 * (lv + 55) + lv * lv * (lv - 1) * 0.00082));
            int tempResist = (int)Math.Round(BaseResist * 1.69 * (lv - 1) + 1);

            if (element == eElement.None)
            {
                stats.EDNone = tempED;
                stats.ResistNone = tempResist;
            }
            else if (element == eElement.Fire)
            {
                stats.EDFire = tempED;
                stats.ResistFire = tempResist;
            }
            else if (element == eElement.Ice)
            {
                stats.EDIce = tempED;
                stats.ResistIce = tempResist;
            }
            else if (element == eElement.Nature)
            {
                stats.EDNature = tempED;
                stats.ResistNature = tempResist;
            }
        }

        private void SetTrait(fmStats stats, eTrait trait)
        {
            if (trait == eTrait.HP)
            {
                if (stats.Lv <= 25)
                    stats.HP = (int)Math.Round(1.8f * stats.HP);
                else
                    stats.HP = (int)Math.Round(2.1f * stats.HP);
            }
            else if (trait == eTrait.Power)
            {
                if (stats.Lv <= 20)
                {
                    stats.BWDMin = (int)Math.Round(1.5f * stats.BWDMin);
                    stats.BWDMax = (int)Math.Round(1.5f * stats.BWDMax);
                }
                else
                {
                    stats.BWDMin = (int)Math.Round(2.1f * stats.BWDMin);
                    stats.BWDMax = (int)Math.Round(2.1f * stats.BWDMax);
                }

            }
            else if (trait == eTrait.CriCriD)
            {
                stats.CriRate = 60f;
                stats.CriDamageRate = 190f;
            }
            else if (trait == eTrait.Boss)
            {
                stats.HP = (int)Math.Round(1.75f * stats.HP);
                //stats.BWDMin = (int)Math.Round(1.1f * stats.BWDMin);
                //stats.BWDMax = (int)Math.Round(1.6f * stats.BWDMax);

                stats.BWDMax = (int)Math.Round(1.25f * stats.BWDMax);

                stats.EDNone    = (int)Math.Round(1.35f * stats.EDNone);
                stats.EDFire    = (int)Math.Round(1.35f * stats.EDFire);
                stats.EDIce     = (int)Math.Round(1.35f * stats.EDIce);
                stats.EDNature  = (int)Math.Round(1.35f * stats.EDNature);

                stats.DEF       = (int)Math.Round(1.25f * stats.DEF);
                stats.ResistAll = (int)Math.Round(1.25f * stats.ResistAll);

                stats.CriRate = 30f;
                stats.CriDamageRate = 90f;
            }
            else if (trait == eTrait.Recovery)
            {
                if (stats.Lv <= 34)
                    stats.Recovery = (int)(5 * (stats.Lv - 1) * 2.1f * 1);
                else if (34 < stats.Lv && stats.Lv <= 69)
                    stats.Recovery = (int)(5 * (stats.Lv - 1) * 2.1f * 2);
                else
                    stats.Recovery = (int)(5 * (stats.Lv - 1) * 2.1f * 6);
            }
            else if (trait == eTrait.ExtraAtk)
            {
                stats.ExtraAtkChance = 2;
            }
            else if (trait == eTrait.CrushBlow)
            {
                stats.CrushingBlow = 3;
            }
            else if (trait == eTrait.Sturn)
            {
                stats.Sturn = 3;
            }
            else if (trait == eTrait.Poison)
            {
                int moReAll = 150;
                stats.ResistAll = (int)Math.Round((moReAll * 1.69 * (stats.Lv - 1)) + 1);
                stats.PoisonRate = 1000;
            }
            else if (trait == eTrait.Thron)
            {
                int moDEF = 72;
                stats.DEF = (int)Math.Round((moDEF * 1.69 * (stats.Lv - 1)) + 1);
                stats.ThornRate = 500;
            }
            else if (trait == eTrait.Burn)
            {
                //int moFire = 100;
                //stats.EDFire = (int)Math.Round(moFire * (1 + (stats.Lv * stats.Lv * 0.007f)));
                stats.CriRate = 45;
                stats.CriDamageRate = 80f;
                stats.BurnRate = 400;
            }
            else if (trait == eTrait.Freeze)
            {
                stats.FreezeRate = 40;
            }

        }

        private void SetUnitSetRareLv(fmStats stats, eUnit unit, eRareLv eRare)
        {
            int rareLv = (int)eRare;

            // 유닛
            if (eUnit.Goblin == unit)
            {
                stats.BWDMin = (int)Math.Round(stats.BWDMin * 0.01);
                stats.BWDMax = (int)Math.Round(stats.BWDMax * 0.01);
                stats.HP = (int)Math.Round(stats.HP * 1.1);
            }
            else if (eUnit.Dragon == unit)
            {
                stats.BWDMin    = (int)Math.Round(stats.BWDMin * 0.07);
                stats.BWDMax    = (int)Math.Round(stats.BWDMax * 0.18);
                //stats.HP        = (int)Math.Round(stats.HP * (rareLv + 2.7 + (rareLv - 1) * (rareLv - 1)));
                stats.HP = (int)Math.Round(stats.HP * (rareLv + 2.1 + (rareLv - 1) * (rareLv - 1)));
                stats.EDNone    = (int)Math.Round(stats.EDNone      * (1 + (rareLv - 1) * 0.1f));
                stats.EDFire    = (int)Math.Round(stats.EDFire      * (1 + (rareLv - 1) * 0.1f));
                stats.EDIce     = (int)Math.Round(stats.EDIce       * (1 + (rareLv - 1) * 0.1f));
                stats.EDNature  = (int)Math.Round(stats.EDNature    * (1 + (rareLv - 1) * 0.1f));
                stats.ResistAll = (int)Math.Round(stats.ResistAll   * (1 + (rareLv) * 0.5f));
            }
            if (eUnit.Monster == unit)
            {
                //stats.BWDMin    = (int)Math.Round(stats.BWDMin      * (1 + ((rareLv - 1) * (rareLv - 1) * 0.065f)));
                //stats.BWDMax    = (int)Math.Round(stats.BWDMax      * (1 + ((rareLv - 1) * (rareLv - 1) * 0.065f)));
                //stats.HP        = (int)Math.Round(stats.HP          * (1 + (rareLv - 1) * (rareLv - 1) * 0.15f));
                //stats.DEF       = (int)Math.Round(stats.DEF         * (1 + (rareLv - 1) * 0.25f));
                //stats.ResistAll = (int)Math.Round(stats.ResistAll   * (1 + (rareLv - 1) * 1.2f));

                //if (stats.Lv < 10)
                //{
                //    stats.BWDMin    = (int)Math.Round(stats.BWDMin      * (1 + ((rareLv - 1) * 0.08f)));
                //    stats.BWDMax    = (int)Math.Round(stats.BWDMax      * (1 + ((rareLv - 1) * 0.08f)));
                //    stats.HP        = (int)Math.Round(stats.HP          * (1 + (rareLv - 1) * (rareLv - 1) * 0.15f));
                //    stats.DEF       = (int)Math.Round(stats.DEF         * (1 + (rareLv - 1) * 0.25f));
                //    stats.ResistAll = (int)Math.Round(stats.ResistAll   * (1 + (rareLv - 1) * 1.2f));
                //}
                //else
                {
                    // 레어도 계산
                    stats.BWDMin    = (int)Math.Round(stats.BWDMin      * (1 + (rareLv - 1) * 0.075f));
                    stats.BWDMax    = (int)Math.Round(stats.BWDMax      * (1 + (rareLv - 1) * 0.075f));

                    stats.HP        = (int)Math.Round(stats.HP          * (1 + (rareLv - 1) * 0.55f));
                    stats.DEF       = (int)Math.Round(stats.DEF         * (1 + (rareLv - 1) * 0.15));
                    stats.ResistAll = (int)Math.Round(stats.ResistAll   * (1 + (rareLv - 1) * 0.25));

                    stats.EDNone    = (int)Math.Round(stats.EDNone      * (1 + (rareLv - 1) * 0.085f));
                    stats.EDFire    = (int)Math.Round(stats.EDFire      * (1 + (rareLv - 1) * 0.085f));
                    stats.EDIce     = (int)Math.Round(stats.EDIce       * (1 + (rareLv - 1) * 0.085f));
                    stats.EDNature  = (int)Math.Round(stats.EDNature    * (1 + (rareLv - 1) * 0.085f));
                }
            }
            if (eUnit.Dummy == unit)
            {
                //if (stats.Lv <= 30)
                //{
                    // 레어도 계산
                    //stats.BWDMin    = (int)Math.Round(stats.BWDMin      * (1 + ((rareLv - 1) * (rareLv - 1) * 0.065f)));
                    //stats.BWDMax    = (int)Math.Round(stats.BWDMax      * (1 + ((rareLv - 1) * (rareLv - 1) * 0.065f)));
                    //stats.HP        = (int)Math.Round(stats.HP          * (1 + (rareLv - 1) * (rareLv - 1) * 0.15f));
                    //stats.DEF       = (int)Math.Round(stats.DEF         * (1 + (rareLv - 1) * 0.25f));
                    //stats.ResistAll = (int)Math.Round(stats.ResistAll   * (1 + (rareLv - 1) * 1.2f));

                stats.BWDMin    = (int)Math.Round(stats.BWDMin      * (1 + (rareLv - 1) * 0.065f));
                stats.BWDMax    = (int)Math.Round(stats.BWDMax      * (1 + (rareLv - 1) * 0.065f));
                //stats.HP        = (int)Math.Round(stats.HP          * (1 + (rareLv - 1) * 0.15f));
                stats.DEF       = (int)Math.Round(stats.DEF         * (1 + (rareLv - 1) * 0.25f));
                stats.ResistAll = (int)Math.Round(stats.ResistAll   * (1 + (rareLv - 1) * 1.2f));
                //stats.EDNone    = (int)Math.Round(stats.EDNone      * ((rareLv - 1) * 0.5f));
                //stats.EDFire    = (int)Math.Round(stats.EDFire      * ((rareLv - 1) * 0.5f));
                //stats.EDIce     = (int)Math.Round(stats.EDIce       * ((rareLv - 1) * 0.5f));
                //stats.EDNature  = (int)Math.Round(stats.EDNature    * ((rareLv - 1) * 0.5f));
                //}
                //else
                //{
                //    // 레어도 계산
                //    stats.BWDMin    = (int)Math.Round(stats.BWDMin      * (1 + (rareLv - 1) * 0.22f));
                //    stats.BWDMax    = (int)Math.Round(stats.BWDMax      * (1 + (rareLv - 1) * 0.22f));
                //    stats.HP        = (int)Math.Round(stats.HP          * (1 + (rareLv - 1) * 0.05f));
                //    stats.DEF       = (int)Math.Round(stats.DEF         * (1 + (rareLv - 1) * 0.25f));
                //    stats.ResistAll = (int)Math.Round(stats.ResistAll   * (1 + (rareLv - 1) * 1.2f));
                //    stats.EDNone    = (int)Math.Round(stats.EDNone      * (1 + (rareLv - 1) * 0.05f));
                //    stats.EDFire    = (int)Math.Round(stats.EDFire      * (1 + (rareLv - 1) * 0.05f));
                //    stats.EDIce     = (int)Math.Round(stats.EDIce       * (1 + (rareLv - 1) * 0.05f));
                //    stats.EDNature  = (int)Math.Round(stats.EDNature    * (1 + (rareLv - 1) * 0.05f));
                //    stats.Recovery  = (int)(5 * (stats.Lv - 1) * 2.1f * 2);                    
                //}


            }
        }

        private void Multiply(fmStats stats, int extraLv)
        {
            if (extraLv <= 0)
                return;

            float multipy = (1 + extraLv * BaseMulipy);

            Dictionary<eOption, fmOption> dic = null;
            if (false == stats.TryGetRefDic(out dic))
                return;

            foreach (var node in dic.Values)
            {
                if (eOption.Element == node.Kind) continue;
                else if (eOption.AS == node.Kind) continue;
                else if (eOption.ASRate == node.Kind) continue;
                else if (eOption.ItemDropRate == node.Kind) continue;
                else if (eOption.GoldDropRate == node.Kind) continue;
                else if (eOption.FindMagicItemRate == node.Kind) continue;
                else if (eOption.DEF == node.Kind) continue;
                else if (eOption.ResistAll == node.Kind) continue;
                else if (eOption.CriRate == node.Kind) continue;
                else if (eOption.CriDamageRate == node.Kind) continue;

                node.Value = node.Value * multipy;
            }
        }
    }

    public static partial class fmStatEx
    {
        public static fmBattleLord ToDummy(this fmDataPvpDummy data)
        {
            return new fmBattleLord(new fmAbility(CreateMonsterStat(eUnit.Dummy, eTrait.CrushBlow, eRareLv.Gold, data.m_nLv, 0, eElement.None)));
        }

        public static fmBattleMonster ToBattleMon(this fmDataMonster data)
        {
            return new fmBattleMonster(data.m_eRareLv, new fmAbility(CreateMonsterStat(data.m_eUnit, data.m_eTrait, data.m_eRareLv, data.m_nLv, data.m_nExtraLv, data.m_eElement)));
        }

        public static fmBattleMonster ToGoblin(this fmDataMonster data, int lv)
        {
            return new fmBattleMonster(data.m_eRareLv, new fmAbility(CreateMonsterStat(data.m_eUnit, data.m_eTrait, data.m_eRareLv, lv, data.m_nExtraLv, data.m_eElement)));
        }

        public static fmBattleMonster ToDragon(this fmDataMonster data, int lv, eElement element)
        {
            return new fmBattleMonster(data.m_eRareLv, new fmAbility(CreateMonsterStat(data.m_eUnit, data.m_eTrait, data.m_eRareLv, lv, data.m_nExtraLv, element)));
        }

        private static void Absorb(this fmStats stat, List<rdItem> items)
        {
            Dictionary<eOption, fmOption> dic = null;
            if (true == stat.TryGetRefDic(out dic))
            {

                foreach (var nodes in items)
                {
                    if (nodes.Equip)
                    {
                        foreach (var node in nodes.BaseOpt)
                        {
                            if (true == dic.ContainsKey(node.Kind))
                            {
                                dic[node.Kind].Value += node.Value;
                            }
                        }

                        foreach (var node in nodes.AddOpts)
                        {
                            eOption opt = node.Kind;
                            int index = (int)node.Kind;


                            eOptGrade cc = (eOptGrade)((int)opt >> 8);

                            if (cc == eOptGrade.Epic)
                                opt = (eOption)((int)opt - ((int)eOptGrade.Normal << 8));

                            if (true == dic.ContainsKey(opt))
                            {
                                dic[opt].Value += node.Value;
                            }
                        }

                    }
                }
            }
        }

        public static fmStats TofmStat(this List<rdItem> list)
        {
            fmStats stat = new fmStats();
            stat.Absorb(list);
            return stat;
        }
    }
}
