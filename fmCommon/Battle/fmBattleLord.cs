using System;

namespace fmCommon.Battle
{
    public class fmBattleLord : fmBattleUnit
    {
        public fmBattleLord(fmAbility ab) : base(ab)
        {
            m_ab = ab;
        }

        //public override long DPS(bool isCri)
        //{
        //    // BWD // WD // WD Rate // ED Rate // ED // AS
        //    // ((((BWD * (1 + WDRate)) + WD) * (1 + EDRate)) + ED) * AS;

        //    int BWD = m_refAtkTable.GetBaseAtk(m_ab.BWDMin, m_ab.BWDMax);

        //    int ED = 0;
        //    float EDRate = 0f;
        //    if (GetElement() == eElement.None) { ED = m_ab.EDFire; EDRate = m_ab.EDRateNone; }
        //    else if (GetElement() == eElement.Fire) { ED = m_ab.EDIce; EDRate = m_ab.EDRateFire; }
        //    else if (GetElement() == eElement.Ice) { ED = m_ab.EDNature; EDRate = m_ab.EDRateIce; }
        //    else if (GetElement() == eElement.Nature) { ED = m_ab.EDNone; EDRate = m_ab.EDRateNature; }

        //    double dps = ((((BWD * (1 + m_ab.WDRate * 0.01)) + m_ab.WD) * (1 + EDRate * 0.01)) + ED) * (m_ab.AS * (1 + m_ab.ASRate * 0.01));

        //    dps = isCri ? (dps * (1 + m_ab.CriDamage * 0.01)) : dps;

        //    return (long)Math.Round(dps);
        //}

        //public override long Defend(long damage, int lv, eElement element)
        //{
        //    //= A12 - (A12 * (A9 / (A9 + A1 * 100)))

        //    int elementResist = 0;
        //    if (element == eElement.None) elementResist = m_ab.ResistNone;
        //    else if (element == eElement.Fire) elementResist = m_ab.ResistFire;
        //    else if (element == eElement.Ice) elementResist = m_ab.ResistIce;
        //    else if (element == eElement.Nature) elementResist = m_ab.ResistNature;

        //    // 1. 단저
        //    long reduceElementResist = damage - (long)Math.Round((damage * ((float)elementResist / (elementResist + lv * 100))));
        //    // 2. 올저
        //    long reduceAllResist = reduceElementResist - (long)Math.Round((reduceElementResist * ((float)m_ab.ResistAll / (m_ab.ResistAll + lv * 100))));
        //    // 3. 방어력
        //    long reduceArmor = reduceAllResist - (long)Math.Round(reduceAllResist * (float)m_ab.DEF / (m_ab.DEF + lv * 500));


        //    return reduceArmor;
        //}
    }
}
