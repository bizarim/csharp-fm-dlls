using System;
using System.Collections.Generic;

namespace fmCommon.Battle
{
    public class fmAbility : IDisposable
    {
        private rdStat m_base = null;
        private fmStats m_stats = null;

        public fmAbility(int lv, rdStat stat, List<rdItem> items)
        {
            // 여기서는 clone 되어진다.
            m_stats = items.TofmStat();

            Lv = lv;
            InitBaseStat(stat);
            InitSetEffect();
            Refresh();
        }

        public fmAbility(int lv, rdStat stat, fmStats stats)
        {
            // 여기서는 clone 해야 한다.
            m_stats = stats.Clone();

            Lv = lv;
            InitBaseStat(stat);
            InitSetEffect();
            Refresh();
        }

        public fmAbility(fmStats stats)
        {
            // 여기서는 clone 하지 않아도 되고
            m_stats = stats;
            InitSetEffect();
            Refresh();
        }

        public bool TryGetRefDic(out Dictionary<eOption, fmOption> dic)
        {
            if (null == m_stats)
            {
                dic = null;
                return false;
            }

            return m_stats.TryGetRefDic(out dic);
        }

        // -----------------------------------------------------------------------------------------------------------------------------
        private void InitBaseStat(rdStat stat)
        {
            m_base = stat.Clone() as rdStat;
            m_base.Point = 0;
            //m_base.TotalPoint = 0;
            
            SetBaseStat();
        }

        private void SetBaseStat()
        {
            if (null == m_base)
                return;

            HP += fmStatEx.BaseHp;
            DEF += fmStatEx.BaseDef;

            int incAtk = m_base.Atk * fmStatEx.IncWDPerPoint;
            if (0 != incAtk)
            {
                BWDMin += incAtk;
                BWDMax += incAtk;
            }

            int incDef = m_base.Def * fmStatEx.IncDefPerPoint;
            if (0 != incDef)
            {
                DEF += incDef;
                ResistAll += incDef;
            }

            int incHp = m_base.Hp * fmStatEx.IncHpPerPoint;
            if (0 != incHp)
                HP += incHp;

            CriRate += fmStatEx.CriRate;
            CriDamageRate += fmStatEx.CriDamageRate;
        }

        private void InitSetEffect()
        {
            m_stats.ApplySetEffect();   
        }

        // -----------------------------------------------------------------------------------------------------------------------------
        public void ResetBaseStat(rdStat stat)
        {
            if (null == m_base) return;

            BWDMin -= (m_base.Atk * fmStatEx.IncWDPerPoint);
            BWDMax -= (m_base.Atk * fmStatEx.IncWDPerPoint);
            DEF -= (m_base.Def * fmStatEx.IncDefPerPoint);
            ResistAll -= (m_base.Def * fmStatEx.IncDefPerPoint);
            HP -= (m_base.Hp * fmStatEx.IncHpPerPoint);

            m_base.Atk = stat.Atk;
            m_base.Def = stat.Def;
            m_base.Hp = stat.Hp;

            BWDMin += (m_base.Atk * fmStatEx.IncWDPerPoint);
            BWDMax += (m_base.Atk * fmStatEx.IncWDPerPoint);
            DEF += (m_base.Def * fmStatEx.IncDefPerPoint);
            ResistAll += (m_base.Def * fmStatEx.IncDefPerPoint);
            HP += (m_base.Hp * fmStatEx.IncHpPerPoint);
        }

        public void ResetEquipStat(List<rdItem> items)
        {
            if (null != m_stats)
            {
                m_stats.Dispose();
                m_stats = null;
            }
            m_stats = items.TofmStat();
            SetBaseStat();
            InitSetEffect();
            Refresh();
        }
        public void Refresh()
        {
            if (BWDMax < BWDMin)
                BWDMin = BWDMax;
        }

        // -----------------------------------------------------------------------------------------------------------------------------
        public int      ED                      { get { return m_stats.ED            ; } private set { m_stats.ED             = value; } }

        public int      ResistFireRate          { get { return m_stats.ResistFireRate       ; } private set { m_stats.ResistFireRate        = value; } }
        public int      ResistIceRate           { get { return m_stats.ResistIceRate        ; } private set { m_stats.ResistIceRate         = value; } }
        public int      ResistNatureRate        { get { return m_stats.ResistNatureRate     ; } private set { m_stats.ResistNatureRate      = value; } }
        public int      ResistNoneRate          { get { return m_stats.ResistNoneRate       ; } private set { m_stats.ResistNoneRate        = value; } }
        public int      Sturn                   { get { return m_stats.Sturn                ; } private set { m_stats.Sturn                 = value; } }
        public int      EXPRate                 { get { return m_stats.EXPRate              ; } private set { m_stats.EXPRate               = value; } }
        public int      ThornRate               { get { return m_stats.ThornRate            ; } private set { m_stats.ThornRate             = value; } }
        public int      PoisonRate              { get { return m_stats.PoisonRate           ; } private set { m_stats.PoisonRate            = value; } }
        public int      ExtraStone              { get { return m_stats.ExtraStone           ; } private set { m_stats.ExtraStone            = value; } }
        public int      BurnRate                { get { return m_stats.BurnRate             ; } private set { m_stats.BurnRate              = value; } }
        public int      FreezeRate              { get { return m_stats.FreezeRate           ; } private set { m_stats.FreezeRate            = value; } }
        //public int      IceRecoveryRate         { get { return m_stats.IceRecoveryRate      ; } private set { m_stats.IceRecoveryRate       = value; } }

        // -----------------------------------------------------------------------------------------------------------------------------
        public int      Lv                      { get { return m_stats.Lv;             }         set { m_stats.Lv             = value; } }

        public eElement Element                 { get { return m_stats.Element       ; } private set { m_stats.Element        = value; } }
        public float    CriRate                 { get { return m_stats.CriRate       ; } private set { m_stats.CriRate        = value; } }
        public float    CriDamageRate           { get { return m_stats.CriDamageRate ; } private set { m_stats.CriDamageRate  = value; } }
        public int      ResistAll               { get { return m_stats.ResistAll     ; } private set { m_stats.ResistAll      = value; } }
        public int      ResistFire              { get { return m_stats.ResistFire    ; } private set { m_stats.ResistFire     = value; } }
        public int      ResistIce               { get { return m_stats.ResistIce     ; } private set { m_stats.ResistIce      = value; } }
        public int      ResistNature            { get { return m_stats.ResistNature  ; } private set { m_stats.ResistNature   = value; } }
        public int      ResistNone              { get { return m_stats.ResistNone    ; } private set { m_stats.ResistNone     = value; } }
        public int      EDFire                  { get { return m_stats.EDFire        ; } private set { m_stats.EDFire         = value; } }
        public int      EDIce                   { get { return m_stats.EDIce         ; } private set { m_stats.EDIce          = value; } }
        public int      EDNature                { get { return m_stats.EDNature      ; } private set { m_stats.EDNature       = value; } }
        public int      EDNone                  { get { return m_stats.EDNone        ; } private set { m_stats.EDNone         = value; } }
        public float    EDRateFire              { get { return m_stats.EDRateFire    ; } private set { m_stats.EDRateFire     = value; } }
        public float    EDRateIce               { get { return m_stats.EDRateIce     ; } private set { m_stats.EDRateIce      = value; } }
        public float    EDRateNature            { get { return m_stats.EDRateNature  ; } private set { m_stats.EDRateNature   = value; } }
        public float    EDRateNone              { get { return m_stats.EDRateNone    ; } private set { m_stats.EDRateNone     = value; } }
        public int      DEF                     { get { return m_stats.DEF           ; } private set { m_stats.DEF            = value; } }
        public long     HP                      { get { return m_stats.HP            ; } private set { m_stats.HP             = value; } }
        public int      BWDMin                  { get { return m_stats.BWDMin        ; } private set { m_stats.BWDMin         = value; } }
        public int      BWDMax                  { get { return m_stats.BWDMax        ; } private set { m_stats.BWDMax         = value; } }
        public int      WD                      { get { return m_stats.WD            ; } private set { m_stats.WD             = value; } }
        public float    WDRate                  { get { return m_stats.WDRate        ; } private set { m_stats.WDRate         = value; } }
        public float    AS                      { get { return m_stats.AS            ; } private set { m_stats.AS             = value; } }
        public float    ASRate                  { get { return m_stats.ASRate        ; } private set { m_stats.ASRate         = value; } }
        public float    ItemDropRate            { get { return m_stats.ItemDropRate  ; } private set { m_stats.ItemDropRate   = value; } }
        public float    GoldDropRate            { get { return m_stats.GoldDropRate  ; } private set { m_stats.GoldDropRate   = value; } }
        public int      Recovery                { get { return m_stats.Recovery      ; } private set { m_stats.Recovery       = value; } }
        public int      FindMagicItemRate       { get { return m_stats.FindMagicItemRate; } private set { m_stats.FindMagicItemRate = value; } }
        public int      DEFRate                 { get { return m_stats.DEFRate      ; } private set { m_stats.DEFRate           = value; } }
        public int      HPRate                  { get { return m_stats.HPRate       ; } private set { m_stats.HPRate            = value; } }
        public int      RecoveryRate            { get { return m_stats.RecoveryRate ; } private set { m_stats.RecoveryRate      = value; } }

        public int      ExtraAtkChance          { get { return m_stats.ExtraAtkChance; } private set { m_stats.ExtraAtkChance = value; } }
        public int      CrushingBlow            { get { return m_stats.CrushingBlow  ; } private set { m_stats.CrushingBlow   = value; } }
        public float    PlusSetEffect           { get { return m_stats.PlusSetEffect ; } private set { m_stats.PlusSetEffect  = value; } }
        public int      ExtraDMGToRareMon       { get { return m_stats.ExtraDMGToRareMon; } private set { m_stats.ExtraDMGToRareMon = value; } }
        public int      LegnendThornRate        { get { return m_stats.LegnendThornRate; } private set { m_stats.LegnendThornRate = value; } }
        public int      LegnendPoisonRate       { get { return m_stats.LegnendPoisonRate; } private set { m_stats.LegnendPoisonRate = value; } }
        public int      LegnendBurnRate         { get { return m_stats.LegnendBurnRate; } private set { m_stats.LegnendBurnRate = value; } }
        public int      LegnendFreezeRate       { get { return m_stats.LegnendFreezeRate; } private set { m_stats.LegnendFreezeRate = value; } }
        public int      LegnendDMGReduceRate    { get { return m_stats.LegnendDMGReduceRate; } private set { m_stats.LegnendDMGReduceRate = value; } }

        public int      SETAttack               { get { return m_stats.SETAttack     ; } private set { m_stats.SETAttack      = value; } }
        public int      SETLuck                 { get { return m_stats.SETLuck       ; } private set { m_stats.SETLuck        = value; } }
        public int      SETFindMagicItemRate    { get { return m_stats.SETFindMagicItemRate; } private set { m_stats.SETFindMagicItemRate = value; } }
        public int      SETExpRate              { get { return m_stats.SETExpRate        ; } private set { m_stats.SETExpRate = value; } }
        public int      SETFire                 { get { return m_stats.SETFire          ; } private set { m_stats.SETFire       = value; } }
        public int      SETIce                  { get { return m_stats.SETIce           ; } private set { m_stats.SETIce        = value; } }
        public int      SETNature               { get { return m_stats.SETNature        ; } private set { m_stats.SETNature     = value; } }
        public int      SETNone                 { get { return m_stats.SETNone          ; } private set { m_stats.SETNone       = value; } }
        public int      SETThorn                { get { return m_stats.SETThorn; } private set { m_stats.SETThorn = value; } }
        public int      SETPoison               { get { return m_stats.SETPoison; } private set { m_stats.SETPoison = value; } }
        public int      SETExtraStone           { get { return m_stats.SETExtraStone    ; } private set { m_stats.SETExtraStone     = value; } }
        public int      SETRecovery             { get { return m_stats.SETRecovery      ; } private set { m_stats.SETRecovery       = value; } }
        public int      SETHP                   { get { return m_stats.SETHP            ; } private set { m_stats.SETHP             = value; } }
        public int      SETBurn                 { get { return m_stats.SETBurn          ; } private set { m_stats.SETBurn           = value; } }
        public int      SETFreeze               { get { return m_stats.SETFreeze        ; } private set { m_stats.SETFreeze         = value; } }

        public int      AcDMGToRareMon          { get { return m_stats.AcDMGToRareMon   ; } private set { m_stats.AcDMGToRareMon    = value; } }
        public int      AcAllResistRate         { get { return m_stats.AcAllResistRate  ; } private set { m_stats.AcAllResistRate   = value; } }
        public int      AcHPRate                { get { return m_stats.AcHPRate         ; } private set { m_stats.AcHPRate          = value; } }
        public int      AcRecoveryRate          { get { return m_stats.AcRecoveryRate   ; } private set { m_stats.AcRecoveryRate    = value; } }


        // -----------------------------------------------------------------------------------------------------------------------------

        public long DP_DPS
        {
            get
            {
                Refresh();
                int BWD = (BWDMin + BWDMax) / 2;
                int dpsED = 0;
                float dpsEDRate = 0f;
                if (Element == eElement.None) { dpsED = EDFire; dpsEDRate = EDRateNone; }
                else if (Element == eElement.Fire) { dpsED = EDIce; dpsEDRate = EDRateFire; }
                else if (Element == eElement.Ice) { dpsED = EDNature; dpsEDRate = EDRateIce; }
                else if (Element == eElement.Nature) { dpsED = EDNone; dpsEDRate = EDRateNature; }

                int extrED = (int)Math.Round((EDNone + EDFire + EDIce + EDNature) * 0.1);

                double dps = ((((BWD * (1 + WDRate * 0.01)) + WD) * (1 + dpsEDRate * 0.01)) + (extrED + ED + extrED)) * (AS * (1 + ASRate * 0.01));
                return (long)Math.Round(dps);
            }
        }

        public long DP_HP
        {
            get
            {
                return (long)Math.Round(HP * (1 + (HPRate + AcHPRate) * 0.01));
            }
        }

        public int DP_DEF
        {
            get
            {
                return (int)Math.Round(DEF * (1 + DEFRate * 0.01));
            }
        }


        protected bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~fmAbility()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                
                if (null != m_base)
                {
                    m_base.Dispose();
                    m_base = null;
                }

                if (null != m_stats)
                {
                    m_stats.Dispose();
                    m_stats = null;
                }
            }
            m_disposed = true;
        }
    }
}
