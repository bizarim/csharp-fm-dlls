
// 20160627
//using fmCommon.Battle;
//using System.Collections.Generic;

//namespace fmCommon
//{
//    public static partial class fmStatEx
//    {
//        public static int BaseHp { get { return 430; } }
//        public static int BaseDef { get { return 20; } }

//        public static int IncWDPerPoint { get { return 8; } }
//        public static int IncDefPerPoint { get { return 15; } }
//        public static int IncHpPerPoint { get { return 25; } }

//        //public static int IncWDPerPoint { get { return 8; } }
//        //public static int IncDefPerPoint { get { return 3; } }
//        //public static int IncHpPerPoint { get { return 15; } }

//        private static bool m_bInit = false;
//        private static Dictionary<eOption, List<fmSetEffect>> m_dic = new Dictionary<eOption, List<fmSetEffect>>();

//        private static void Load()
//        {
//            m_bInit = true;
//            m_dic.Clear();
//            m_dic.Add(eOption.SETAttack, new List<fmSetEffect>());
//            m_dic.Add(eOption.SETLuck, new List<fmSetEffect>());
//            m_dic[eOption.SETAttack].Clear();
//            m_dic[eOption.SETLuck].Clear();

//            m_dic[eOption.SETAttack].Add(new fmSetEffect { SetOpt = eOption.SETAttack, SetCnt = 2, AddOpt = eOption.HP, Value = 300 });
//            m_dic[eOption.SETAttack].Add(new fmSetEffect { SetOpt = eOption.SETAttack, SetCnt = 4, AddOpt = eOption.WD, Value = 300 });
//            m_dic[eOption.SETAttack].Add(new fmSetEffect { SetOpt = eOption.SETAttack, SetCnt = 6, AddOpt = eOption.WDRate, Value = 25f });

//            m_dic[eOption.SETLuck].Add(new fmSetEffect { SetOpt = eOption.SETLuck, SetCnt = 2, AddOpt = eOption.HP, Value = 300 });
//            m_dic[eOption.SETLuck].Add(new fmSetEffect { SetOpt = eOption.SETLuck, SetCnt = 4, AddOpt = eOption.GoldDropRate, Value = 120f });
//            m_dic[eOption.SETLuck].Add(new fmSetEffect { SetOpt = eOption.SETLuck, SetCnt = 6, AddOpt = eOption.ItemDropRate, Value = 120f });

//        }

//        public static bool LiftSetEffect(this fmStats stats)
//        {
//            if (false == m_bInit) Load();

//            int setAtkCnt = stats.SETAttack + (int)stats.PlusSetEffect;
//            int setLuckCnt = stats.SETLuck + (int)stats.PlusSetEffect;

//            {
//                foreach (var node in m_dic[eOption.SETAttack])
//                {
//                    if (node.SetCnt <= setAtkCnt)
//                    {
//                        stats.DecSetEffect(node);
//                    }
//                }
//            }
//            {
//                foreach (var node in m_dic[eOption.SETLuck])
//                {
//                    if (node.SetCnt <= setLuckCnt)
//                    {
//                        stats.DecSetEffect(node);
//                    }
//                }
//            }

//            return true;
//        }

//        public static bool ApplySetEffect(this fmStats stats)
//        {
//            if (false == m_bInit) Load();

//            int setAtkCnt = stats.SETAttack;
//            int setLuckCnt = stats.SETLuck;

//            if (0 < setAtkCnt)
//                setAtkCnt += (int)stats.PlusSetEffect;

//            if (0 < setLuckCnt)
//                setLuckCnt += (int)stats.PlusSetEffect;

//            {
//                foreach (var node in m_dic[eOption.SETAttack])
//                {
//                    if (node.SetCnt <= setAtkCnt)
//                    {
//                        stats.IncSetEffect(node);
//                    }
//                }
//            }
//            {
//                foreach (var node in m_dic[eOption.SETLuck])
//                {
//                    if (node.SetCnt <= setLuckCnt)
//                    {
//                        stats.IncSetEffect(node);
//                    }
//                }
//            }

//            return true;
//        }

//        public static bool TryGetRefList(eOption opt, out List<fmSetEffect> list)
//        {
//            if (false == m_bInit) Load();

//            list = null;

//            if (false == m_dic.ContainsKey(opt))
//                return false;

//            list = m_dic[opt];

//            return true;
//        }
//    }

//    public static partial class fmStatEx
//    {
//        private static fmStats CreateMonsterStat(eTrait trait, eRareLv eRare, int lv, int extarLv, eElement element)
//        {
//            fmStats stat = new fmStats();
//            stat.Lv = lv;
//            stat.Element = element;

//            int rareLv = (int)eRare;

//            // data.m_nLv
//            // data.m_nRareLv
//            int BWDMin = 112;
//            int BWDMax = 127;
//            float CriRate = 18f;
//            float CriDamage = 30f;
//            int HP = 350;
//            //int DEF = 48;
//            int DEF = 39;
//            int ResistAll = 20;
//            int ranED = 30;
//            int ranResist = 7;
//            int recovery = 0;

//            float fBWDMin = 1;
//            float fBWDMax = 1;
//            float fCriRate = CriRate;
//            float fCriDamage = CriDamage;
//            float fHP = 1;
//            float franED = 1;
//            float franResist = 1;

//            if (trait == eTrait.HP)
//            {
//                fHP = 1.3f;
//            }
//            else if (trait == eTrait.Cri)
//            {
//                fCriRate = 30f;
//            }
//            else if (trait == eTrait.Power)
//            {
//                fBWDMin = 1.2f;
//                fBWDMax = 1.4f;
//            }
//            else if (trait == eTrait.Element)
//            {
//                franED = 1.5f;
//                franResist = 1.2f;
//            }
//            else if (trait == eTrait.CriCriD)
//            {
//                fCriRate = 40f;
//                fCriDamage = 70f;
//            }
//            else if (trait == eTrait.Recovery)
//            {
//                recovery = 5;
//            }

//            stat.BWDMin = (int)(BWDMin * (fBWDMin + (lv * 0.135f) + ((rareLv - 1) * 0.08f)));
//            stat.BWDMax = (int)(BWDMax * (fBWDMax + (lv * 0.145f) + ((rareLv - 1) * 0.09f)));
//            stat.CriRate = fCriRate;
//            stat.CriDamage = fCriDamage;

//            stat.HP = (long)((HP * (fHP + ((lv - 2) * 0.143f))) * (1 + ((rareLv - 1) * 0.165f)));

//            stat.DEF = (int)(DEF * 0.215f * (lv + 1) + DEF * (lv - 1) * 0.5f);

//            stat.ResistAll = (int)(ResistAll * 0.5f * (lv + 1) * (1 + 0.2f * (rareLv - 1)));
//            stat.AS = 1;

//            stat.Recovery = (int)(recovery * lv * 1.03f);

//            if (element == eElement.None)
//            {
//                stat.EDNone = ranED * (int)(franED + (rareLv * lv * 0.8f));
//                stat.ResistNone = (int)(ranResist * 0.5f * (lv + 2) * franResist + ranResist * (lv - 2) * 0.5f);
//            }
//            else if (element == eElement.Fire)
//            {
//                stat.EDFire = ranED * (int)(franED + (rareLv * lv * 0.8f));
//                stat.ResistFire = (int)(ranResist * 1.3f * (lv + 2) * franResist + ranResist * (lv - 2) * 1.3f);
//            }
//            else if (element == eElement.Ice)
//            {
//                stat.EDIce = ranED * (int)(franED + (rareLv * lv * 0.8f));
//                stat.ResistIce = (int)(ranResist * 1.3f * (lv + 2) * franResist + ranResist * (lv - 2) * 1.3f);
//            }
//            else if (element == eElement.Nature)
//            {
//                stat.EDNature = ranED * (int)(franED + (rareLv * lv * 0.8f));
//                stat.ResistNature = (int)(ranResist * 1.3f * (lv + 2) * franResist + ranResist * (lv - 2) * 1.3f);
//            }

//            stat.Multiply(1 + extarLv * 0.2f);

//            return stat;
//        }
//    }

//    public static partial class fmStatEx
//    {
//        public static fmBattleLord ToDummy(this fmDataPvpDummy data)
//        {
//            return new fmBattleLord(new fmAbility(CreateMonsterStat(eTrait.Power, eRareLv.Bronze, data.m_nLv, 0, eElement.None)));
//        }

//        public static fmBattleMonster ToBattleMon(this fmDataMonster data)
//        {
//            return new fmBattleMonster(new fmAbility(CreateMonsterStat(data.m_eTrait, data.m_eRareLv, data.m_nLv, data.m_nExtraLv, data.m_eElement)));
//        }

//        public static fmBattleMonster ToDragon(this fmDataMonster data, int lv, eElement element)
//        {
//            return new fmBattleMonster(new fmAbility(CreateMonsterStat(data.m_eTrait, data.m_eRareLv, lv, data.m_nExtraLv, element)));
//        }

//        private static void Absorb(this fmStats stat, List<rdItem> items)
//        {
//            Dictionary<eOption, fmOption> dic = null;
//            if (true == stat.TryGetRefDic(out dic))
//            {

//                foreach (var nodes in items)
//                {
//                    if (nodes.Equip)
//                    {
//                        foreach (var node in nodes.BaseOpt)
//                        {
//                            if (true == dic.ContainsKey(node.Kind))
//                            {
//                                dic[node.Kind].Value += node.Value;
//                            }
//                        }

//                        foreach (var node in nodes.AddOpts)
//                        {
//                            eOption opt = node.Kind;
//                            int index = (int)node.Kind;


//                            eOptCategory cc = (eOptCategory)((int)opt >> 8);

//                            if (cc == eOptCategory.Epic)
//                                opt = (eOption)((int)opt - ((int)eOptCategory.Normal << 8));

//                            if (true == dic.ContainsKey(opt))
//                            {
//                                dic[opt].Value += node.Value;
//                            }
//                        }

//                    }
//                }
//            }
//        }

//        public static fmStats TofmStat(this List<rdItem> list)
//        {
//            fmStats stat = new fmStats();
//            stat.Absorb(list);
//            return stat;
//        }

//        private static void Multiply(this fmStats stats, float multipy)
//        {
//            Dictionary<eOption, fmOption> dic = null;
//            if (false == stats.TryGetRefDic(out dic))
//                return;

//            foreach (var node in dic.Values)
//            {
//                if (eOption.Element == node.Kind) continue;
//                else if (eOption.AS == node.Kind) continue;
//                else if (eOption.ASRate == node.Kind) continue;
//                else if (eOption.ItemDropRate == node.Kind) continue;
//                else if (eOption.GoldDropRate == node.Kind) continue;


//                node.Value = node.Value * multipy;
//            }
//        }
//    }
//}
