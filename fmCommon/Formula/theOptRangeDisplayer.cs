using System;
using System.Collections.Generic;

namespace fmCommon.Formula
{
    public enum eMinMax
    {
        Min = 0,
        Max = 1,
        Avg = 2,
    }

    public static class theOptRangeDisplayer
    {
        delegate float fnFormula(int lv, eMinMax range);

        static Dictionary<eOption, fnFormula> m_values = new Dictionary<eOption, fnFormula>();

        static Dictionary<eOption, fnFormula> m_valuesAncient = new Dictionary<eOption, fnFormula>();

        static bool bLoad = false;

        private static void Load()
        {
            m_values.Clear();
            m_values.Add(eOption.CriRate, OnCriRate);
            m_values.Add(eOption.CriDamageRate, OnCriDamageRate);
            m_values.Add(eOption.ResistAll, OnResistAll);
            m_values.Add(eOption.ResistFire, OnResistFire);
            m_values.Add(eOption.ResistIce, OnResistIce);
            m_values.Add(eOption.ResistNature, OnResistNature);
            m_values.Add(eOption.ResistNone, OnResistNone);
            m_values.Add(eOption.EDFire, OnEDFire);
            m_values.Add(eOption.EDIce, OnEDIce);
            m_values.Add(eOption.EDNature, OnEDNature);
            m_values.Add(eOption.EDNone, OnEDNone);
            m_values.Add(eOption.EDRateFire, OnEDRateFire);
            m_values.Add(eOption.EDRateIce, OnEDRateIce);
            m_values.Add(eOption.EDRateNature, OnEDRateNature);
            m_values.Add(eOption.EDRateNone, OnEDRateNone);
            m_values.Add(eOption.DEF, OnDEF);
            m_values.Add(eOption.HP, OnHP);
            m_values.Add(eOption.BWDMin, OnBWDMin);
            m_values.Add(eOption.BWDMax, OnBWDMax);
            m_values.Add(eOption.WD, OnWD);
            m_values.Add(eOption.WDRate, OnWDRate);
            m_values.Add(eOption.AS, OnAS);
            m_values.Add(eOption.ASRate, OnASRate);
            m_values.Add(eOption.ItemDropRate, OnItemDropRate);
            m_values.Add(eOption.GoldDropRate, OnGoldDropRate);
            m_values.Add(eOption.Recovery, OnRecovery);
            m_values.Add(eOption.FindMagicItemRate, OnFindMagicItemRate);
            m_values.Add(eOption.DEFRate, OnDEFRate);
            m_values.Add(eOption.HPRate, OnHPRate);
            m_values.Add(eOption.RecoveryRate, OnRecoveryRate);

            m_values.Add(eOption.EpCriRate, OnEpCriRate);
            m_values.Add(eOption.EpCriDamageRate, OnEpCriDamageRate);
            m_values.Add(eOption.EpResistAll, OnEpResistAll);
            m_values.Add(eOption.EpResistFire, OnEpResistFire);
            m_values.Add(eOption.EpResistIce, OnEpResistIce);
            m_values.Add(eOption.EpResistNature, OnEpResistNature);
            m_values.Add(eOption.EpResistNone, OnEpResistNone);
            m_values.Add(eOption.EpEDFire, OnEpEDFire);
            m_values.Add(eOption.EpEDIce, OnEpEDIce);
            m_values.Add(eOption.EpEDNature, OnEpEDNature);
            m_values.Add(eOption.EpEDNone, OnEpEDNone);
            m_values.Add(eOption.EpEDRateFire, OnEpEDRateFire);
            m_values.Add(eOption.EpEDRateIce, OnEpEDRateIce);
            m_values.Add(eOption.EpEDRateNature, OnEpEDRateNature);
            m_values.Add(eOption.EpEDRateNone, OnEpEDRateNone);
            m_values.Add(eOption.EpDEF, OnEpDEF);
            m_values.Add(eOption.EpHP, OnEpHP);
            m_values.Add(eOption.EpBWDMin, OnEpBWDMin);
            m_values.Add(eOption.EpBWDMax, OnEpBWDMax);
            m_values.Add(eOption.EpWD, OnEpWD);
            m_values.Add(eOption.EpWDRate, OnEpWDRate);
            m_values.Add(eOption.EpAS, OnEpAS);
            m_values.Add(eOption.EpASRate, OnEpASRate);
            m_values.Add(eOption.EpItemDropRate, OnEpItemDropRate);
            m_values.Add(eOption.EpGoldDropRate, OnEpGoldDropRate);
            m_values.Add(eOption.EpRecovery, OnEpRecovery);
            m_values.Add(eOption.EpFindMagicItemRate, OnEpFindMagicItemRate);
            m_values.Add(eOption.EpDEFRate, OnEpDEFRate);
            m_values.Add(eOption.EpHPRate, OnEpHPRate);
            m_values.Add(eOption.EpRecoveryRate, OnEpRecoveryRate);

            m_values.Add(eOption.ExtraAtkChance,        OnExtraAtkChance        );
            m_values.Add(eOption.CrushingBlow,          OnCrushingBlow          );
            m_values.Add(eOption.PlusSetEffect,         OnPlusSetEffect         );
            m_values.Add(eOption.ExtraDMGToRareMon,     OnExtraDMGToRareMon     );
            m_values.Add(eOption.LegnendThornRate,      OnLegnendThornRate      );
            m_values.Add(eOption.LegnendPoisonRate,     OnLegnendPoisonRate     );
            m_values.Add(eOption.LegnendBurnRate  ,     OnLegnendBurnRate       );
            m_values.Add(eOption.LegnendFreezeRate,     OnLegnendFreezeRate);
            m_values.Add(eOption.LegnendDMGReduceRate,  OnLegnendDMGReduceRate);


            m_values.Add(eOption.SETAttack,             OnSETAttack             );
            m_values.Add(eOption.SETLuck,               OnSETLuck               );
            m_values.Add(eOption.SETFindMagicItemRate,  OnSETFindMagicItemRate  );
            m_values.Add(eOption.SETExpRate,            OnSETExpRate            );
            m_values.Add(eOption.SETFire,               OnSETFire               );
            m_values.Add(eOption.SETIce,                OnSETIce                );
            m_values.Add(eOption.SETNature,             OnSETNature             );
            m_values.Add(eOption.SETNone,               OnSETNone               );
            m_values.Add(eOption.SETThorn,              OnSETThorn              );
            m_values.Add(eOption.SETPoison,             OnSETPoison             );
            m_values.Add(eOption.SETExtraStone,         OnSETExtraStone         );
            m_values.Add(eOption.SETRecovery,           OnSETRecovery           );
            m_values.Add(eOption.SETHP      ,           OnSETHP                 );
            m_values.Add(eOption.SETBurn    ,           OnSETBurn               );
            m_values.Add(eOption.SETFreeze  ,           OnSETFreeze             );


            m_values.Add(eOption.SETFireT3, OnSETFireT3);
            m_values.Add(eOption.SETIceT3, OnSETIceT3);
            m_values.Add(eOption.SETNatureT3, OnSETNatureT3);
            m_values.Add(eOption.SETNoneT3, OnSETNoneT3);

            LoadAncient();
            bLoad = true;
        }

        private static void LoadAncient()
        {
            m_valuesAncient.Clear();
            m_valuesAncient.Add(eOption.CriRate,               OnAncientCriRate);
            m_valuesAncient.Add(eOption.CriDamageRate,         OnAncientCriDamageRate);
            m_valuesAncient.Add(eOption.ResistAll,             OnAncientResistAll);
            m_valuesAncient.Add(eOption.ResistFire,            OnAncientResistFire);
            m_valuesAncient.Add(eOption.ResistIce,             OnAncientResistIce);
            m_valuesAncient.Add(eOption.ResistNature,          OnAncientResistNature);
            m_valuesAncient.Add(eOption.ResistNone,            OnAncientResistNone);
            m_valuesAncient.Add(eOption.EDFire,                OnAncientEDFire);
            m_valuesAncient.Add(eOption.EDIce,                 OnAncientEDIce);
            m_valuesAncient.Add(eOption.EDNature,              OnAncientEDNature);
            m_valuesAncient.Add(eOption.EDNone,                OnAncientEDNone);
            m_valuesAncient.Add(eOption.EDRateFire,            OnAncientEDRateFire);
            m_valuesAncient.Add(eOption.EDRateIce,             OnAncientEDRateIce);
            m_valuesAncient.Add(eOption.EDRateNature,          OnAncientEDRateNature);
            m_valuesAncient.Add(eOption.EDRateNone,            OnAncientEDRateNone);
            m_valuesAncient.Add(eOption.DEF,                   OnAncientDEF);
            m_valuesAncient.Add(eOption.HP,                    OnAncientHP);
            m_valuesAncient.Add(eOption.BWDMin,                OnAncientBWDMin);
            m_valuesAncient.Add(eOption.BWDMax,                OnAncientBWDMax);
            m_valuesAncient.Add(eOption.WD,                    OnAncientWD);
            m_valuesAncient.Add(eOption.WDRate,                OnAncientWDRate);
            m_valuesAncient.Add(eOption.AS,                    OnAncientAS);
            m_valuesAncient.Add(eOption.ASRate,                OnAncientASRate);
            m_valuesAncient.Add(eOption.ItemDropRate,          OnAncientItemDropRate);
            m_valuesAncient.Add(eOption.GoldDropRate,          OnAncientGoldDropRate);
            m_valuesAncient.Add(eOption.Recovery,              OnAncientRecovery);
            m_valuesAncient.Add(eOption.FindMagicItemRate,     OnAncientFindMagicItemRate);
            m_valuesAncient.Add(eOption.DEFRate,               OnAncientDEFRate);
            m_valuesAncient.Add(eOption.HPRate,                OnAncientHPRate);
            m_valuesAncient.Add(eOption.RecoveryRate,          OnAncientRecoveryRate);

            m_valuesAncient.Add(eOption.EpCriRate,             OnAncientEpCriRate);
            m_valuesAncient.Add(eOption.EpCriDamageRate,       OnAncientEpCriDamageRate);
            m_valuesAncient.Add(eOption.EpResistAll,           OnAncientEpResistAll);
            m_valuesAncient.Add(eOption.EpResistFire,          OnAncientEpResistFire);
            m_valuesAncient.Add(eOption.EpResistIce,           OnAncientEpResistIce);
            m_valuesAncient.Add(eOption.EpResistNature,        OnAncientEpResistNature);
            m_valuesAncient.Add(eOption.EpResistNone,          OnAncientEpResistNone);
            m_valuesAncient.Add(eOption.EpEDFire,              OnAncientEpEDFire);
            m_valuesAncient.Add(eOption.EpEDIce,               OnAncientEpEDIce);
            m_valuesAncient.Add(eOption.EpEDNature,            OnAncientEpEDNature);
            m_valuesAncient.Add(eOption.EpEDNone,              OnAncientEpEDNone);
            m_valuesAncient.Add(eOption.EpEDRateFire,          OnAncientEpEDRateFire);
            m_valuesAncient.Add(eOption.EpEDRateIce,           OnAncientEpEDRateIce);
            m_valuesAncient.Add(eOption.EpEDRateNature,        OnAncientEpEDRateNature);
            m_valuesAncient.Add(eOption.EpEDRateNone,          OnAncientEpEDRateNone);
            m_valuesAncient.Add(eOption.EpDEF,                 OnAncientEpDEF);
            m_valuesAncient.Add(eOption.EpHP,                  OnAncientEpHP);
            m_valuesAncient.Add(eOption.EpBWDMin,              OnAncientEpBWDMin);
            m_valuesAncient.Add(eOption.EpBWDMax,              OnAncientEpBWDMax);
            m_valuesAncient.Add(eOption.EpWD,                  OnAncientEpWD);
            m_valuesAncient.Add(eOption.EpWDRate,              OnAncientEpWDRate);
            m_valuesAncient.Add(eOption.EpAS,                  OnAncientEpAS);
            m_valuesAncient.Add(eOption.EpASRate,              OnAncientEpASRate);
            m_valuesAncient.Add(eOption.EpItemDropRate,        OnAncientEpItemDropRate);
            m_valuesAncient.Add(eOption.EpGoldDropRate,        OnAncientEpGoldDropRate);
            m_valuesAncient.Add(eOption.EpRecovery,            OnAncientEpRecovery);
            m_valuesAncient.Add(eOption.EpFindMagicItemRate,   OnAncientEpFindMagicItemRate);
            m_valuesAncient.Add(eOption.EpDEFRate,             OnAncientEpDEFRate);
            m_valuesAncient.Add(eOption.EpHPRate,              OnAncientEpHPRate);
            m_valuesAncient.Add(eOption.EpRecoveryRate,        OnAncientEpRecoveryRate);


            m_valuesAncient.Add(eOption.AcDMGToRareMonRate,     OnAcDMGToRareMon);
            m_valuesAncient.Add(eOption.AcAllResistRate,        OnAcAllResistRate);
            m_valuesAncient.Add(eOption.AcHPRate,               OnAcHPRate);
            //m_valuesAncient.Add(eOption.AcDEFRate,             OnAcDEFRate         );
            m_valuesAncient.Add(eOption.AcRecoveryRate,         OnAcRecoveryRate);
        }

        public static float GetBaseValue(int lv, eOption option, eMinMax range, bool ancient)
        {
            if (false == bLoad) Load();

            float value = 0f;
            if (option == eOption.BWDMin)
            {
                value = GetBaseBWDMin(lv, range, ancient);
            }
            else if (option == eOption.BWDMax)
            {
                value = GetBaseBWDMax(lv, range, ancient);
            }
            else if (option == eOption.AS)
            {
                value = GetBaseAs(range, ancient);
            }
            else if (option == eOption.HP)
            {
                value = GetValue(lv, option, range, ancient);
                value = (float)Math.Round(value * 3);
            }
            else if (option == eOption.DEF)
            {
                value = GetValue(lv, option, range, ancient);
                value = (float)Math.Round(value * 3);
            }
            else if (option == eOption.Element)
            {
                value = GetValue(lv, option, range, ancient);
            }
            else if (option == eOption.CriRate)
            {
                value = GetValue(lv, option, range, ancient);
                value = (float)Math.Round(value * 2);
            }
            else
            {
                value = GetValue(lv, option, range, ancient);
                value = (float)Math.Round(value * 4);
            }

            return value;
        }
        private static int GetBaseBWDMin(int lv, eMinMax range, bool ancient)
        {
            int mb = 12;
            int atkRate = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

            int minBWD = (int)Math.Round(mb * 1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

            int min = minBWD;
            int max = minBWD + atkRate * 3;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static int GetBaseBWDMax(int lv, eMinMax range, bool ancient)
        {
            int mb = 18;
            int maxBWD = (int)Math.Round(mb * 1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

            int atkRate = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));

            int min = maxBWD - atkRate;
            int max = maxBWD;

            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static float GetBaseAs(eMinMax range, bool ancient)
        {
            float min = 1.00f;
            float max = 1.09f;
            float hit = range == eMinMax.Min ? min : max;
            return hit;
        }

        public static float GetValue(int lv, eOption opt, eMinMax range, bool ancient)
        {
            if (false == bLoad) Load();

            if (ancient)
            {
                return m_valuesAncient[opt](lv, range);
            }
            else
            {
                return m_values[opt](lv, range);
            }
        }

        private static float OnCriRate(int lv, eMinMax range)
        {
            int min = 15;
            int max = 50;

            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (float)Math.Round(hit * 0.1f, 2);
        }
        private static float OnCriDamageRate(int lv, eMinMax range)
        {
            int min = 80;
            int max = 200;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (float)Math.Round(hit * 0.1f, 2);
        }
        private static float OnResistAll(int lv, eMinMax range)
        {
            int min = 200;
            int max = 700;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private static float OnResistFire(int lv, eMinMax range)
        {
            int min = 200;
            int max = 700;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private static float OnResistIce(int lv, eMinMax range)
        {
            int min = 200;
            int max = 700;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private static float OnResistNature(int lv, eMinMax range)
        {
            int min = 200;
            int max = 700;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private static float OnResistNone(int lv, eMinMax range)
        {
            int min = 200;
            int max = 700;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round(hit * 0.01 * (lv + 1));
        }
        private static float OnEDFire(int lv, eMinMax range)
        {
            int min = 30;
            int max = 60;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private static float OnEDIce(int lv, eMinMax range)
        {
            int min = 30;
            int max = 60;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private static float OnEDNature(int lv, eMinMax range)
        {
            int min = 30;
            int max = 60;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private static float OnEDNone(int lv, eMinMax range)
        {
            int min = 30;
            int max = 60;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private static float OnEDRateFire(int lv, eMinMax range)
        {
            int min = 320;
            int max = 500;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private static float OnEDRateIce(int lv, eMinMax range)
        {
            int min = 320;
            int max = 500;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private static float OnEDRateNature(int lv, eMinMax range)
        {
            int min = 320;
            int max = 500;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private static float OnEDRateNone(int lv, eMinMax range)
        {
            int min = 320;
            int max = 500;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private static float OnDEF(int lv, eMinMax range)
        {
            int min = 110;
            int max = 400;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            float get = (float)Math.Round(hit * 0.01f * (lv + 1));
            return get;
        }
        private static float OnHP(int lv, eMinMax range)
        {
            int min = 8500;
            int max = 14500;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            float get = (float)Math.Round(hit * 0.001 * (1.5 + 0.054 * (lv + 70) + lv * (lv - 1) * 0.01));
            return get;
        }
        private static float OnBWDMin(int lv, eMinMax range)
        {
            int min = 1;
            int max = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002) / 4);
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static float OnBWDMax(int lv, eMinMax range)
        {
            int min = 1;
            int max = (int)Math.Round(1.2 * (1.55 + 0.05 * (lv + 60) + lv * lv * (lv - 1) * 0.0002));
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static float OnWD(int lv, eMinMax range)
        {
            int min = 12;
            int max = 40;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round((lv * hit * 0.1f) + 5);
        }
        private static float OnWDRate(int lv, eMinMax range)
        {
            int min = 320;
            int max = 500;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private static float OnAS(int lv, eMinMax range)
        {
            int min = 10;
            int max = 120;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit * 0.0001f;
        }
        private static float OnASRate(int lv, eMinMax range)
        {
            int min = 320;
            int max = 500;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (float)Math.Round(hit * 0.01f, 2);
        }
        private static float OnItemDropRate(int lv, eMinMax range)
        {
            int min = 30;
            int max = 55;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static float OnGoldDropRate(int lv, eMinMax range)
        {
            int min = 30;
            int max = 55;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static float OnRecovery(int lv, eMinMax range)
        {
            int min = 8;
            int max = 15;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return (int)Math.Round(hit * (lv + 9) * 0.1f);
        }
        private static float OnFindMagicItemRate(int lv, eMinMax range)
        {
            int min = 30;
            int max = 55;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static float OnDEFRate(int lv, eMinMax range)
        {
            int min = 5;
            int max = 35;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static float OnHPRate(int lv, eMinMax range)
        {
            int min = 5;
            int max = 25;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static float OnRecoveryRate(int lv, eMinMax range)
        {
            int min = 2;
            int max = 15;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }


        private static float OnEpCriRate(int lv, eMinMax range) { return OnCriRate(lv, range) * 2; }
        private static float OnEpCriDamageRate(int lv, eMinMax range) { return OnCriDamageRate(lv, range) * 2; }
        private static float OnEpResistAll(int lv, eMinMax range) { return OnResistAll(lv, range) * 2; }
        private static float OnEpResistFire(int lv, eMinMax range) { return OnResistFire(lv, range) * 2; }
        private static float OnEpResistIce(int lv, eMinMax range) { return OnResistIce(lv, range) * 2; }
        private static float OnEpResistNature(int lv, eMinMax range) { return OnResistNature(lv, range) * 2; }
        private static float OnEpResistNone(int lv, eMinMax range) { return OnResistNone(lv, range) * 2; }
        private static float OnEpEDFire(int lv, eMinMax range) { return OnEDFire(lv, range) * 2; }
        private static float OnEpEDIce(int lv, eMinMax range) { return OnEDIce(lv, range) * 2; }
        private static float OnEpEDNature(int lv, eMinMax range) { return OnEDNature(lv, range) * 2; }
        private static float OnEpEDNone(int lv, eMinMax range) { return OnEDNone(lv, range) * 2; }
        private static float OnEpEDRateFire(int lv, eMinMax range) { return OnEDRateFire(lv, range) * 2; }
        private static float OnEpEDRateIce(int lv, eMinMax range) { return OnEDRateIce(lv, range) * 2; }
        private static float OnEpEDRateNature(int lv, eMinMax range) { return OnEDRateNature(lv, range) * 2; }
        private static float OnEpEDRateNone(int lv, eMinMax range) { return OnEDRateNone(lv, range) * 2; }
        private static float OnEpDEF(int lv, eMinMax range) { return OnDEF(lv, range) * 2; }
        private static float OnEpHP(int lv, eMinMax range) { return OnHP(lv, range) * 2; }
        private static float OnEpBWDMin(int lv, eMinMax range) { return OnBWDMin(lv, range) * 2; }
        private static float OnEpBWDMax(int lv, eMinMax range) { return OnBWDMax(lv, range) * 2; }
        private static float OnEpWD(int lv, eMinMax range) { return OnWD(lv, range) * 2; }
        private static float OnEpWDRate(int lv, eMinMax range) { return OnWDRate(lv, range) * 2; }
        private static float OnEpAS(int lv, eMinMax range) { return OnAS(lv, range) * 2; }
        private static float OnEpASRate(int lv, eMinMax range) { return OnASRate(lv, range) * 2; }
        private static float OnEpItemDropRate(int lv, eMinMax range) { return OnItemDropRate(lv, range) * 2; }
        private static float OnEpGoldDropRate(int lv, eMinMax range) { return OnGoldDropRate(lv, range) * 2; }
        private static float OnEpRecovery(int lv, eMinMax range) { return OnRecovery(lv, range) * 2; }
        private static float OnEpFindMagicItemRate(int lv, eMinMax range) { return OnFindMagicItemRate(lv, range) * 2; }
        private static float OnEpDEFRate(int lv, eMinMax range) { return OnDEFRate(lv, range) * 2; }
        private static float OnEpHPRate(int lv, eMinMax range) { return OnHPRate(lv, range) * 2; }
        private static float OnEpRecoveryRate(int lv, eMinMax range) { return OnRecoveryRate(lv, range) * 2; }


        private static float OnAcDMGToRareMon(int lv, eMinMax range)
        {
            int min = 1;
            int max = 50;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }

        private static float OnAcAllResistRate(int lv, eMinMax range)
        {
            int min = 1;
            int max = 30;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }

        private static float OnAcHPRate(int lv, eMinMax range)
        {
            int min = 1;
            int max = 30;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        //private static float OnAcDEFRate(int lv, eMinMax range)
        //{
        //int min = 1;
        //int max = 20;
        //int avg = (min + max) / 2;
        //int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
        //    return hit;
        //}
        private static float OnAcRecoveryRate(int lv, eMinMax range)
        {
            int min = 1;
            int max = 40;
            int avg = (min + max) / 2;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }



        private static float OnExtraAtkChance(int lv, eMinMax rang) { return 1; }
        private static float OnCrushingBlow(int lv, eMinMax rang) { return 1; }
        private static float OnPlusSetEffect(int lv, eMinMax range)
        {
            int min = 1;
            int max = 2;
            int avg = 1;
            int hit = range == eMinMax.Min ? min : range == eMinMax.Max ? max : avg;
            return hit;
        }
        private static float OnExtraDMGToRareMon(int lv, eMinMax rang) { return 1; }
        private static float OnLegnendThornRate(int lv, eMinMax rang) { return 200; }
        private static float OnLegnendPoisonRate(int lv, eMinMax rang) { return 200; }
        private static float OnLegnendBurnRate(int lv, eMinMax rang) { return 200; }
        private static float OnLegnendFreezeRate(int lv, eMinMax rang) { return 5; }
        //private static float OnLegnendDMGReduceRate(int lv, eMinMax rang) { return 10; }
        private static float OnLegnendDMGReduceRate(int lv, eMinMax rang) { return 9; }


        private static float OnSETAttack           (int lv, eMinMax rang) { return 1; }
        private static float OnSETLuck             (int lv, eMinMax rang) { return 1; }
        private static float OnSETFindMagicItemRate(int lv, eMinMax rang) { return 1; }
        private static float OnSETExpRate          (int lv, eMinMax rang) { return 1; }
        private static float OnSETFire             (int lv, eMinMax rang) { return 1; }
        private static float OnSETIce              (int lv, eMinMax rang) { return 1; }
        private static float OnSETNature           (int lv, eMinMax rang) { return 1; }
        private static float OnSETNone             (int lv, eMinMax rang) { return 1; }
        private static float OnSETThorn            (int lv, eMinMax rang) { return 1; }
        private static float OnSETPoison           (int lv, eMinMax rang) { return 1; }
        private static float OnSETExtraStone       (int lv, eMinMax rang) { return 1; }
        private static float OnSETRecovery         (int lv, eMinMax rang) { return 1; }
        private static float OnSETHP               (int lv, eMinMax rang) { return 1; }
        private static float OnSETBurn             (int lv, eMinMax rang) { return 1; }
        private static float OnSETFreeze           (int lv, eMinMax rang) { return 1; }

        private static float OnSETFireT3(int lv, eMinMax rang) { return 1; }
        private static float OnSETIceT3(int lv, eMinMax rang) { return 1; }
        private static float OnSETNatureT3(int lv, eMinMax rang) { return 1; }
        private static float OnSETNoneT3(int lv, eMinMax rang) { return 1; }



        private static float OnAncientCriRate          (int lv, eMinMax range)        { return (float)Math.Round(OnCriRate(lv, range) * 1.2, 2); }
        private static float OnAncientCriDamageRate    (int lv, eMinMax range)        { return (float)Math.Round(OnCriDamageRate(lv, range) * 1.2, 2); }
        private static float OnAncientResistAll        (int lv, eMinMax range)        { return (float)Math.Round(OnResistAll(lv, range) * 1.2); }
        private static float OnAncientResistFire       (int lv, eMinMax range)        { return (float)Math.Round(OnResistFire(lv, range) * 1.2); }
        private static float OnAncientResistIce        (int lv, eMinMax range)        { return (float)Math.Round(OnResistIce(lv, range) * 1.2); }
        private static float OnAncientResistNature     (int lv, eMinMax range)        { return (float)Math.Round(OnResistNature(lv, range) * 1.2); }
        private static float OnAncientResistNone       (int lv, eMinMax range)        { return (float)Math.Round(OnResistNone(lv, range) * 1.2); }
        private static float OnAncientEDFire           (int lv, eMinMax range)        { return (float)Math.Round(OnEDFire(lv, range) * 1.2); }
        private static float OnAncientEDIce            (int lv, eMinMax range)        { return (float)Math.Round(OnEDIce(lv, range) * 1.2); }
        private static float OnAncientEDNature         (int lv, eMinMax range)        { return (float)Math.Round(OnEDNature(lv, range) * 1.2); }
        private static float OnAncientEDNone           (int lv, eMinMax range)        { return (float)Math.Round(OnEDNone(lv, range) * 1.2); }
        private static float OnAncientEDRateFire       (int lv, eMinMax range)        { return (float)Math.Round(OnEDRateFire(lv, range) * 1.2, 2); }
        private static float OnAncientEDRateIce        (int lv, eMinMax range)        { return (float)Math.Round(OnEDRateIce(lv, range) * 1.2, 2); }
        private static float OnAncientEDRateNature     (int lv, eMinMax range)        { return (float)Math.Round(OnEDRateNature(lv, range) * 1.2, 2); }
        private static float OnAncientEDRateNone       (int lv, eMinMax range)        { return (float)Math.Round(OnEDRateNone(lv, range) * 1.2, 2); }
        private static float OnAncientDEF              (int lv, eMinMax range)        { return (float)Math.Round(OnDEF(lv, range) * 1.2); }
        private static float OnAncientHP               (int lv, eMinMax range)        { return (float)Math.Round(OnHP(lv, range) * 1.2); }
        private static float OnAncientBWDMin           (int lv, eMinMax range)        { return (float)Math.Round(OnBWDMin(lv, range) * 1.2); }
        private static float OnAncientBWDMax           (int lv, eMinMax range)        { return (float)Math.Round(OnBWDMax(lv, range) * 1.2); }
        private static float OnAncientWD               (int lv, eMinMax range)        { return (float)Math.Round(OnWD(lv, range) * 1.2); }
        private static float OnAncientWDRate           (int lv, eMinMax range)        { return (float)Math.Round(OnWDRate(lv, range) * 1.2, 2); }
        private static float OnAncientAS               (int lv, eMinMax range)        { return (float)Math.Round(OnAS(lv, range) * 1.2); }
        private static float OnAncientASRate           (int lv, eMinMax range)        { return (float)Math.Round(OnASRate(lv, range) * 1.2, 2); }
        private static float OnAncientItemDropRate     (int lv, eMinMax range)        { return (float)Math.Round(OnItemDropRate(lv, range) * 1.2, 2); }
        private static float OnAncientGoldDropRate     (int lv, eMinMax range)        { return (float)Math.Round(OnGoldDropRate(lv, range) * 1.2, 2); }
        private static float OnAncientRecovery         (int lv, eMinMax range)        { return (float)Math.Round(OnRecovery(lv, range) * 1.2); }
        private static float OnAncientFindMagicItemRate(int lv, eMinMax range)        { return (float)Math.Round(OnFindMagicItemRate(lv, range) * 1.2, 2); }
        private static float OnAncientDEFRate          (int lv, eMinMax range)        { return (float)Math.Round(OnDEFRate(lv, range) * 1.2, 2); }
        private static float OnAncientHPRate           (int lv, eMinMax range)        { return (float)Math.Round(OnHPRate(lv, range) * 1.2, 2); }
        private static float OnAncientRecoveryRate     (int lv, eMinMax range)        { return (float)Math.Round(OnRecoveryRate(lv, range) * 1.2, 2); }



        private static float OnAncientEpCriRate(int lv, eMinMax range)            { return OnAncientCriRate           (lv, range) * 2; }
        private static float OnAncientEpCriDamageRate(int lv, eMinMax range)      { return OnAncientCriDamageRate     (lv, range) * 2; }
        private static float OnAncientEpResistAll(int lv, eMinMax range)          { return OnAncientResistAll         (lv, range) * 2; }
        private static float OnAncientEpResistFire(int lv, eMinMax range)         { return OnAncientResistFire        (lv, range) * 2; }
        private static float OnAncientEpResistIce(int lv, eMinMax range)          { return OnAncientResistIce         (lv, range) * 2; }
        private static float OnAncientEpResistNature(int lv, eMinMax range)       { return OnAncientResistNature      (lv, range) * 2; }
        private static float OnAncientEpResistNone(int lv, eMinMax range)         { return OnAncientResistNone        (lv, range) * 2; }
        private static float OnAncientEpEDFire(int lv, eMinMax range)             { return OnAncientEDFire            (lv, range) * 2; }
        private static float OnAncientEpEDIce(int lv, eMinMax range)              { return OnAncientEDIce             (lv, range) * 2; }
        private static float OnAncientEpEDNature(int lv, eMinMax range)           { return OnAncientEDNature          (lv, range) * 2; }
        private static float OnAncientEpEDNone(int lv, eMinMax range)             { return OnAncientEDNone            (lv, range) * 2; }
        private static float OnAncientEpEDRateFire(int lv, eMinMax range)         { return OnAncientEDRateFire        (lv, range) * 2; }
        private static float OnAncientEpEDRateIce(int lv, eMinMax range)          { return OnAncientEDRateIce         (lv, range) * 2; }
        private static float OnAncientEpEDRateNature(int lv, eMinMax range)       { return OnAncientEDRateNature      (lv, range) * 2; }
        private static float OnAncientEpEDRateNone(int lv, eMinMax range)         { return OnAncientEDRateNone        (lv, range) * 2; }
        private static float OnAncientEpDEF(int lv, eMinMax range)                { return OnAncientDEF               (lv, range) * 2; }
        private static float OnAncientEpHP(int lv, eMinMax range)                 { return OnAncientHP                (lv, range) * 2; }
        private static float OnAncientEpBWDMin(int lv, eMinMax range)             { return OnAncientBWDMin            (lv, range) * 2; }
        private static float OnAncientEpBWDMax(int lv, eMinMax range)             { return OnAncientBWDMax            (lv, range) * 2; }
        private static float OnAncientEpWD(int lv, eMinMax range)                 { return OnAncientWD                (lv, range) * 2; }
        private static float OnAncientEpWDRate(int lv, eMinMax range)             { return OnAncientWDRate            (lv, range) * 2; }
        private static float OnAncientEpAS(int lv, eMinMax range)                 { return OnAncientAS                (lv, range) * 2; }
        private static float OnAncientEpASRate(int lv, eMinMax range)             { return OnAncientASRate            (lv, range) * 2; }
        private static float OnAncientEpItemDropRate(int lv, eMinMax range)       { return OnAncientItemDropRate      (lv, range) * 2; }
        private static float OnAncientEpGoldDropRate(int lv, eMinMax range)       { return OnAncientGoldDropRate      (lv, range) * 2; }
        private static float OnAncientEpRecovery(int lv, eMinMax range)           { return OnAncientRecovery          (lv, range) * 2; }
        private static float OnAncientEpFindMagicItemRate(int lv, eMinMax range)  { return OnAncientFindMagicItemRate (lv, range) * 2; }
        private static float OnAncientEpDEFRate(int lv, eMinMax range)            { return OnAncientDEFRate           (lv, range) * 2; }
        private static float OnAncientEpHPRate(int lv, eMinMax range)             { return OnAncientHPRate            (lv, range) * 2; }
        private static float OnAncientEpRecoveryRate(int lv, eMinMax range)       { return OnAncientRecoveryRate      (lv, range) * 2; }
    }
}
