using fmCommon.Battle.Base;
using fmCommon.Formula;
using System;
using System.Collections.Generic;

namespace fmCommon.Battle
{
    public class fmBattleUnit : IBattleUnit
    {
        private fmTbAttack m_atkTable = new fmTbAttack();
        protected fmAbility m_ab = null;
        protected int m_nPoisonCnt = 0;

        List<eActType> m_listPreActType = new List<eActType>();
        List<eActType> m_listPostActType = new List<eActType>();

        public fmBattleUnit(fmAbility ab)
        {
            m_ab = ab;
        }

        public bool Initialize()
        {
            if (null == m_ab)
                return false;

            //if (false == m_ab.Initialize())
            //    return false;

            InitCurHp();
            InitializePreAct();
            InitializePostAct();
            InitPoisonCnt();
            InitFreezeCnt();
            return true;
        }

        protected void InitPoisonCnt() { m_nPoisonCnt = 0; }
        protected void InitFreezeCnt() { m_nFreezeCnt = 0; }
        protected int GetPosionCnt() { return ++m_nPoisonCnt; }

        protected long m_curHp = 0;
        protected void InitCurHp()
        {
            m_curHp = GetMaxHp();
        }
        public long GetCurHp() { return m_curHp; }
        public long GetMaxHp() { return (long)Math.Round(m_ab.HP * (1 + (m_ab.HPRate + m_ab.AcHPRate) * 0.01)); }
        public int GetLv() { return m_ab.Lv; }

        public eElement GetElement(eActType act = eActType.Nomal)
        {
            if (act == eActType.Thorn)
                return eElement.None;
            else if (act == eActType.Posion)
                return eElement.Nature;

            return m_ab.Element;
        }
        public virtual eRareLv GetRareLv() { return eRareLv.Bronze; }
        public virtual int GetExtraCnt() { return m_ab.ExtraAtkChance; }
        public bool IsCritical() { return m_atkTable.IsCritical(m_ab); }
        public eActType GetInAtkType()
        {
            if (m_eAbnormal != eAbnormal.None)
                return eActType.None;

            eActType eAct = m_atkTable.GetInAtkType(m_ab);
            SetFreezeCnt(eAct);

            return eAct;
        }

        private void SetFreezeCnt(eActType eAct)
        {
            if (eAct == eActType.Freeze)
                m_nFreezeCnt += 1;
            else
                m_nFreezeCnt = 0;
        }

        private int m_nFreezeCnt = 0;
        private eAbnormal m_eAbnormal = eAbnormal.None;

        public void SetAbnormal(eAbnormal eAb)
        {
            m_eAbnormal = eAb;
        }

        public eAbnormal GetAbnormal()
        {
            return m_eAbnormal;
        }

        public bool TryGetPreActType(out List<eActType> list)
        {
            list = m_listPreActType;
            return true;
        }

        public bool TryGetPostActType(out List<eActType> list)
        {
            list = m_listPostActType;
            return true;
        }

        private void InitializePreAct()
        {
            if (null == m_listPreActType)
                m_listPreActType = new List<eActType>();

            m_listPreActType.Clear();
            if (0 < m_ab.Recovery) m_listPreActType.Add(eActType.Recovery);
            //if (0 < m_ab.LegnendIceRecoveryRate) m_listPreActType.Add(eActType.IceRecovery);
        }
        private void InitializePostAct()
        {
            if (null == m_listPostActType)
                m_listPostActType = new List<eActType>();

            m_listPostActType.Clear();

            //if (0 < m_ab.SETThorn) m_listPostActType.Add(eActType.Thorn);
            if (0 < m_ab.PoisonRate) m_listPostActType.Add(eActType.Posion);
            if (0 < m_ab.BurnRate) m_listPostActType.Add(eActType.Burn);
        }

        public virtual long WDPS(bool isCri, eRareLv rareLv)
        {
            int BWD = m_atkTable.GetBaseAtk(m_ab.BWDMin, m_ab.BWDMax);
            double dps = (((BWD * (1 + m_ab.WDRate * 0.01)) + m_ab.WD)) * (m_ab.AS * (1 + m_ab.ASRate * 0.01));
            dps = isCri ? (dps * (1 + m_ab.CriDamageRate * 0.01)) : dps;

            double multiply = (1 + m_ab.AcDMGToRareMon * 0.01);

            if (0 < m_ab.PoisonRate)
            {
                return (long)Math.Round(dps * multiply / 100);
            }
            else
            {
                return (long)Math.Round(dps * multiply);
            }
        }

        public virtual long EDPS(bool isCri, eRareLv rareLv)
        {
            int BWD = m_atkTable.GetBaseAtk(m_ab.BWDMin, m_ab.BWDMax);

            int ED = 0;
            float EDRate = 0f;

            if (GetElement() == eElement.None) { ED = m_ab.EDNone; EDRate = m_ab.EDRateNone; }
            else if (GetElement() == eElement.Fire) { ED = m_ab.EDFire; EDRate = m_ab.EDRateFire; }
            else if (GetElement() == eElement.Ice) { ED = m_ab.EDIce; EDRate = m_ab.EDRateIce; }
            else if (GetElement() == eElement.Nature) { ED = m_ab.EDNature; EDRate = m_ab.EDRateNature; }

            int extrED = (int)Math.Round((m_ab.EDNone + m_ab.EDFire + m_ab.EDIce + m_ab.EDNature) * 0.1);

            double dps = ((BWD * (1 + EDRate * 0.01)) + (ED + m_ab.ED + extrED)) * (m_ab.AS * (1 + m_ab.ASRate * 0.01));

            dps = isCri ? (dps * (1 + m_ab.CriDamageRate * 0.01)) : dps;

            double multiply = (1 + m_ab.AcDMGToRareMon * 0.01);

            return (long)Math.Round(dps * multiply);
        }

        public virtual long Thorn(long damage, int lv)
        {
            float rate = (GetDEF() / (GetDEF() + lv * 50f));

            int ddED = m_ab.EDNone;
            float ddEDRate = m_ab.EDRateNone;

            //0.35
            double addEd = (ddED * (1 + ddEDRate * 0.01)) * 0.11;

            //1.75
            double dddd = (damage * rate * 0.95 + addEd) * ((m_ab.ThornRate + m_ab.LegnendThornRate) * 0.01);

            //long dddd = (long)Math.Round(((ddED * (1 + ddEDRate * 0.01))) * (m_ab.Thorn * 0.01));

            double multiRareMon = (1 + m_ab.AcDMGToRareMon * 0.01);

            return (long)Math.Round(dddd * multiRareMon);
        }

        public virtual long Poison()
        {
            int poisonED = 0;
            float poisonEDRate = 0f;

            poisonED += m_ab.ResistNone;
            poisonED += m_ab.ResistFire;
            poisonED += m_ab.ResistIce;
            poisonED += m_ab.ResistNature;
            poisonED += m_ab.ResistAll;

            poisonED = poisonED / 5;

            poisonEDRate += m_ab.ResistNoneRate;
            poisonEDRate += m_ab.ResistFireRate;
            poisonEDRate += m_ab.ResistIceRate;
            poisonEDRate += m_ab.ResistNatureRate;
            poisonEDRate += m_ab.AcAllResistRate;

            poisonEDRate = poisonEDRate / 5;

            //0.25
            poisonED = (int)Math.Round(poisonED * (1 + poisonEDRate * 0.01) * 0.015);

            //0.25
            double ddED = m_ab.EDNature * (1 + m_ab.EDRateNature * 0.01) * 0.015;

            double multi = GetPosionCnt() * 1 * 1.75 * ((m_ab.PoisonRate + m_ab.LegnendPoisonRate) * 0.01);

            double dddd = (poisonED + ddED) * multi;

            //int BWD = m_atkTable.GetBaseAtk(m_ab.BWDMin, m_ab.BWDMax);

            //int ED = 0;
            //float EDRate = 0f;
            //if (GetElement() == eElement.None) { ED = m_ab.EDNone; EDRate = m_ab.EDRateNone; }
            //else if (GetElement() == eElement.Fire) { ED = m_ab.EDFire; EDRate = m_ab.EDRateFire; }
            //else if (GetElement() == eElement.Ice) { ED = m_ab.EDIce; EDRate = m_ab.EDRateIce; }
            //else if (GetElement() == eElement.Nature) { ED = m_ab.EDNature; EDRate = m_ab.EDRateNature; }

            //int extrED = (int)Math.Round((m_ab.EDNone + m_ab.EDFire + m_ab.EDIce + m_ab.EDNature) * 0.1);

            //double dps = ((((BWD * (1 + m_ab.WDRate * 0.01)) + m_ab.WD) * (1 + EDRate * 0.01)) + (ED + m_ab.ED + extrED)) * (m_ab.AS * (1 + m_ab.ASRate * 0.01));
            double multiRareMon = (1 + m_ab.AcDMGToRareMon * 0.01);

            return (long)Math.Round(dddd * multiRareMon);
        }

        public long Burn(bool isCri)
        {
            //double ddED = m_ab.EDFire * (1 + m_ab.EDRateFire * 0.01) * 0.045;

            //double multi = ((m_ab.BurnRate + m_ab.LegnendBurnRate) * 0.01);

            //double dddd = ddED * multi;

            //dddd = isCri ? (multi * dddd * (1 + m_ab.CriDamageRate * 0.01)) : dddd;

            //return (long)Math.Round(dddd);

            //0.25
            double ddED = m_ab.EDFire * (1 + m_ab.EDRateFire * 0.01) * 0.13;

            double multi = ((m_ab.BurnRate + m_ab.LegnendBurnRate) * 0.01);

            double dddd = ddED * multi;

            // * multi
            //dddd = isCri ? (0.15 * multi * dddd * (1 + m_ab.CriDamageRate * 0.01)) : dddd;
            dddd = isCri ? (dddd * (1 + m_ab.CriDamageRate * 0.01)) : dddd;

            double multiRareMon = (1 + m_ab.AcDMGToRareMon * 0.01);

            return (long)Math.Round(dddd * multiRareMon);
        }

        public long Freeze(bool isCri)
        {
            //int BWD = m_atkTable.GetBaseAtk(m_ab.BWDMin, m_ab.BWDMax);

            int ED = m_ab.EDIce;
            float EDRate = m_ab.EDRateIce;

            //double dps = ((((BWD * (1 + m_ab.WDRate * 0.01)) + m_ab.WD) * (1 + EDRate * 0.01)) + (ED + m_ab.ED)) * (m_ab.AS * (1 + m_ab.ASRate * 0.01));

            //double iceDmg = ED * (1 + EDRate * 0.01) * 0.35;
            double iceDmg = ED * (1 + EDRate * 0.01) * 0.19;

            double dps = isCri ? (iceDmg * (1 + m_ab.CriDamageRate * 0.01)) : iceDmg;

            //double dddd = dps + (iceDmg * m_nFreezeCnt * m_nFreezeCnt);
            double dddd = dps + (iceDmg * m_nFreezeCnt * m_nFreezeCnt * 0.65);

            double multiRareMon = (1 + m_ab.AcDMGToRareMon * 0.01);

            return (long)Math.Round(dddd * multiRareMon);
        }

        protected int GetDEF()
        {
            int burnRate = m_ab.BurnRate + m_ab.LegnendBurnRate;

            if (0 < burnRate)
            {
                return (int)Math.Round(m_ab.DEF * (1 + (m_ab.DEFRate - (burnRate * 0.029)) * 0.01));
            }
            else
            {
                return (int)Math.Round(m_ab.DEF * (1 + m_ab.DEFRate * 0.01));
            }
        }

        protected int GetAllResist()
        {
            int burnRate = m_ab.BurnRate + m_ab.LegnendBurnRate;

            if (0 < burnRate)
            {
                return (int)Math.Round(m_ab.ResistAll * (1 + (m_ab.AcAllResistRate - (burnRate * 0.029)) * 0.01));
            }
            else
            {
                return (int)Math.Round(m_ab.ResistAll * (1 + m_ab.AcAllResistRate * 0.01));
            }

        }

        public virtual long DefendWD(eActType actType, long damage, int lv, eElement element)
        {
            // 크러싱 블로우 방어 무시
            if (actType == eActType.CrushingBlow)
                return damage;

            long reduceArmor = damage - (long)Math.Round(damage * (GetDEF() / (GetDEF() + lv * 50f)));

            long lastReduc = reduceArmor - (long)Math.Round(reduceArmor * m_ab.LegnendDMGReduceRate * 0.01);

            return lastReduc;
        }

        public virtual long DefendED(eActType actType, long damage, int lv, eElement element)
        {
            // 크러싱 블로우 방어 무시
            if (actType == eActType.CrushingBlow)
                return damage;

            int elementResist = 0;
            float elementResisRate = 0f;
            if (element == eElement.None) { elementResist = m_ab.ResistNone; elementResisRate = m_ab.ResistNoneRate; }
            else if (element == eElement.Fire) { elementResist = m_ab.ResistFire; elementResisRate = m_ab.ResistFireRate; }
            else if (element == eElement.Ice) { elementResist = m_ab.ResistIce; elementResisRate = m_ab.ResistIceRate; }
            else if (element == eElement.Nature) { elementResist = m_ab.ResistNature; elementResisRate = m_ab.ResistNatureRate; }

            elementResist = (int)Math.Round(elementResist * (1 + elementResisRate * 0.01));

            elementResist += (int)Math.Round((m_ab.ResistNone + m_ab.ResistFire + m_ab.ResistIce + m_ab.ResistNature) * 0.1);

            // 1. 단저
            long reduceElementResist = damage - (long)Math.Round((damage * (elementResist / (elementResist + lv * 100f))));
            // 2. 올저 150->130
            long reduceAllResist = reduceElementResist - (long)Math.Round((reduceElementResist * (GetAllResist() / (GetAllResist() + lv * 130f))));

            long lastReduc = reduceAllResist - (long)Math.Round(reduceAllResist * m_ab.LegnendDMGReduceRate * 0.01);

            return lastReduc;
        }

        public long BeAtkCrushingBlow(bool isCri)
        {
            long cb = (long)Math.Round(GetCurHp() * 0.50);
            return cb;
        }

        public void TakeDamge(long damage)
        {
            m_curHp -= damage;
            if (m_curHp < 0)
                m_curHp = 0;
        }

        public bool Surrender()
        {

            if (m_curHp <= 0)
                return true;

            return false;
        }

        protected int GetRecovery()
        {
            return (int)Math.Round(m_ab.Recovery * (1 + (m_ab.RecoveryRate + m_ab.AcRecoveryRate) * 0.01));
        }

        public int Recovery()
        {
            if (GetCurHp() <= 0) return 0;
            if (GetMaxHp() <= GetCurHp()) return 0;

            int recovery = GetRecovery();

            m_curHp += recovery;

            if (GetMaxHp() < GetCurHp())
            {
                recovery = recovery - ((int)(GetCurHp() - GetMaxHp()));
                m_curHp = GetMaxHp();
            }

            return recovery;
        }

        //public int IceRecovery()
        //{
        //    return 0;
        //    //if (m_nFreezeCnt <= 0) return 0;
        //    ////m_nFreezeCnt = 0;

        //    //double resIce = m_ab.ResistIce * (1 + m_ab.ResistIceRate * 0.01) * 0.05;

        //    //resIce = resIce * (m_ab.LegnendIceRecoveryRate * 0.01);

        //    //int recovery = (int)Math.Round(resIce);

        //    //if (GetCurHp() <= 0) return 0;
        //    //if (GetMaxHp() <= GetCurHp()) return 0;

        //    //m_curHp += recovery;

        //    //if (GetMaxHp() < GetCurHp())
        //    //{
        //    //    recovery = recovery - ((int)(GetCurHp() - GetMaxHp()));
        //    //    m_curHp = GetMaxHp();
        //    //}

        //    //return recovery;
        //}

        protected bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~fmBattleUnit()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                if (null != m_ab)
                {
                    m_ab.Dispose();
                    m_ab = null;
                }

                m_atkTable = null;
            }
            m_disposed = true;
        }
    }
}
