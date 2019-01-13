using System;
using System.Linq;
using System.Collections.Generic;
using fmCommon.Formula;

namespace fmCommon.Battle
{
    public class fmTbAttack
    {
        delegate eActType fnHit(fmAbility ab);

        private Random m_random = new Random();

        private Dictionary<eOption, fnHit> m_hitList = null;

        public fmTbAttack()
        {
            m_hitList = new Dictionary<eOption, fnHit>();
            m_hitList.Clear();
            m_hitList.Add(eOption.ExtraAtkChance,   OnHitExtraAtk);
            m_hitList.Add(eOption.CrushingBlow,     OnHitCrushingBlow);
            m_hitList.Add(eOption.Sturn,            OnHitStun);
            m_hitList.Add(eOption.FreezeRate,       OnHitFreeze);
        }

        public eActType GetInAtkType(fmAbility ab)
        {
            List<eOption> temp = new List<eOption>();

            if (0 < ab.ExtraAtkChance)  temp.Add(eOption.ExtraAtkChance);
            if (0 < ab.CrushingBlow)    temp.Add(eOption.CrushingBlow);
            if (0 < ab.Sturn)           temp.Add(eOption.Sturn);
            if (0 < ab.FreezeRate)      temp.Add(eOption.FreezeRate);

            if (temp.Count <= 0)
                return eActType.Nomal;

            int hit = m_random.Next(0, temp.Count);

            eOption opt = temp.ElementAt(hit);

            if (false == m_hitList.ContainsKey(opt))
                return eActType.Nomal;

            return m_hitList[opt](ab);
        }

        protected eActType OnHitExtraAtk(fmAbility ab)
        {
            if (0 < ab.ExtraAtkChance)
            {
                int hit = m_random.Next(0, 10000);

                if (hit < 2500)
                    return eActType.ExtraAtk;
            }

            return eActType.Nomal;
        }

        protected eActType OnHitCrushingBlow(fmAbility ab)
        {
            if (0 < ab.CrushingBlow)
            {
                int cb = ab.CrushingBlow * theFormula.CrushingBlowRate * 100;

                int hit = m_random.Next(0, 10000);

                if (hit < cb)
                    return eActType.CrushingBlow;
            }

            return eActType.Nomal;
        }

        protected eActType OnHitStun(fmAbility ab)
        {
            if (0 < ab.Sturn)
            {
                int st = ab.Sturn * theFormula.SturnRate * 100;

                int hit = m_random.Next(0, 10000);

                if (hit < st)
                    return eActType.Sturn;
            }

            return eActType.Nomal;
        }

        protected eActType OnHitFreeze(fmAbility ab)
        {
            if (0 < ab.FreezeRate)
            {
                int st = (ab.FreezeRate + ab.LegnendFreezeRate) * 100;

                int hit = m_random.Next(0, 10000);

                if (hit < st)
                    return eActType.Freeze;
            }

            return eActType.Nomal;
        }

        public bool IsCritical(fmAbility ab)
        {
            if (null == ab)
                return false;

            int criRate = (int)Math.Round(ab.CriRate) * 100;
            if (6000 < criRate)
                criRate = 6000;

            int hit = m_random.Next(0, 10000);

            if (0 <= hit && hit <= criRate)
                return true;

            return false;
        }

        public int GetBaseAtk(int min, int max)
        {
            return m_random.Next(min, max);
        }

        //public eAbnormal GetAbnormal(fmAbility ab)
        //{
        //    if (null == ab)
        //        return eAbnormal.None;

        //    // 하나당 5% 총 40%
        //    int sturn = ab.Sturn;

        //    int hit = m_random.Next(0, 100);

        //    if (hit < sturn)
        //        return eAbnormal.Sturn;

        //    //else if (eac <= hit && hit < tot)
        //    //    return eAbnormal.Sturn;


        //    return eAbnormal.None;
        //}
    }
}
