using System;
using System.Collections.Generic;

namespace fmCommon.Battle
{
    public class fmStats : IDisposable
    {
        //delegate void fnAdd(float value);

        //private Dictionary<eOption, fnAdd>      m_fnDic = new Dictionary<eOption, fnAdd>();
        private Dictionary<eOption, fmOption>   m_dic = new Dictionary<eOption, fmOption>();

        public fmStats()
        {
            Initialize();
        }

        public bool TryGetRefDic(out Dictionary<eOption, fmOption> dic)
        {
            if (null == m_dic)
            {
                dic = null;
                return false;
            }

            dic = m_dic;
            return true;
        }

        public void IncSetEffect(fmSetEffect data)
        {
            if (null == m_dic) return;
            if (null == data) return;

            m_dic[data.AddOpt].Value += data.Value;
        }

        public void DecSetEffect(fmSetEffect data)
        {
            if (null == m_dic) return;
            if (null == data) return;

            m_dic[data.AddOpt].Value -= data.Value;
        }

        public bool TryReset()
        {
            if (null == m_dic)
                return false;

            foreach (var node in m_dic)
                node.Value.Value = 0f;

            return true;
        }

        private void Initialize()
        {

            m_dic.Clear();
            m_dic.Add(eOption.ED,                   new fmOption { Kind = eOption.ED,               Value = 0f });

            m_dic.Add(eOption.ResistFireRate,       new fmOption { Kind = eOption.ResistFireRate,   Value = 0f });
            m_dic.Add(eOption.ResistIceRate,        new fmOption { Kind = eOption.ResistIceRate,    Value = 0f });
            m_dic.Add(eOption.ResistNatureRate,     new fmOption { Kind = eOption.ResistNatureRate, Value = 0f });
            m_dic.Add(eOption.ResistNoneRate,       new fmOption { Kind = eOption.ResistNoneRate,   Value = 0f });
            m_dic.Add(eOption.Sturn,                new fmOption { Kind = eOption.Sturn,            Value = 0f });
            m_dic.Add(eOption.EXPRate,              new fmOption { Kind = eOption.EXPRate,          Value = 0f });
            m_dic.Add(eOption.ThornRate,            new fmOption { Kind = eOption.ThornRate,        Value = 0f });
            m_dic.Add(eOption.PoisonRate,           new fmOption { Kind = eOption.PoisonRate,       Value = 0f });
            m_dic.Add(eOption.ExtraStone,           new fmOption { Kind = eOption.ExtraStone,       Value = 0f });
            m_dic.Add(eOption.BurnRate,             new fmOption { Kind = eOption.BurnRate,         Value = 0f });
            m_dic.Add(eOption.FreezeRate,           new fmOption { Kind = eOption.FreezeRate,       Value = 0f });
            //m_dic.Add(eOption.IceRecoveryRate,      new fmOption { Kind = eOption.IceRecoveryRate,  Value = 0f });


            m_dic.Add(eOption.Element,              new fmOption { Kind = eOption.Element,          Value = 0f });
            m_dic.Add(eOption.CriRate,              new fmOption { Kind = eOption.CriRate,          Value = 0f });
            m_dic.Add(eOption.CriDamageRate,        new fmOption { Kind = eOption.CriDamageRate,    Value = 0f });
            m_dic.Add(eOption.ResistAll,            new fmOption { Kind = eOption.ResistAll,        Value = 0f });
            m_dic.Add(eOption.ResistFire,           new fmOption { Kind = eOption.ResistFire,       Value = 0f });
            m_dic.Add(eOption.ResistIce,            new fmOption { Kind = eOption.ResistIce,        Value = 0f });
            m_dic.Add(eOption.ResistNature,         new fmOption { Kind = eOption.ResistNature,     Value = 0f });
            m_dic.Add(eOption.ResistNone,           new fmOption { Kind = eOption.ResistNone,       Value = 0f });
            m_dic.Add(eOption.EDFire,               new fmOption { Kind = eOption.EDFire,           Value = 0f });
            m_dic.Add(eOption.EDIce,                new fmOption { Kind = eOption.EDIce,            Value = 0f });
            m_dic.Add(eOption.EDNature,             new fmOption { Kind = eOption.EDNature,         Value = 0f });
            m_dic.Add(eOption.EDNone,               new fmOption { Kind = eOption.EDNone,           Value = 0f });
            m_dic.Add(eOption.EDRateFire,           new fmOption { Kind = eOption.EDRateFire,       Value = 0f });
            m_dic.Add(eOption.EDRateIce,            new fmOption { Kind = eOption.EDRateIce,        Value = 0f });
            m_dic.Add(eOption.EDRateNature,         new fmOption { Kind = eOption.EDRateNature,     Value = 0f });
            m_dic.Add(eOption.EDRateNone,           new fmOption { Kind = eOption.EDRateNone,       Value = 0f });
            m_dic.Add(eOption.DEF,                  new fmOption { Kind = eOption.DEF,              Value = 0f });
            m_dic.Add(eOption.HP,                   new fmOption { Kind = eOption.HP,               Value = 0f });
            m_dic.Add(eOption.BWDMin,               new fmOption { Kind = eOption.BWDMin,           Value = 0f });
            m_dic.Add(eOption.BWDMax,               new fmOption { Kind = eOption.BWDMax,           Value = 0f });
            m_dic.Add(eOption.WD,                   new fmOption { Kind = eOption.WD,               Value = 0f });
            m_dic.Add(eOption.WDRate,               new fmOption { Kind = eOption.WDRate,           Value = 0f });
            m_dic.Add(eOption.AS,                   new fmOption { Kind = eOption.AS,               Value = 0f });
            m_dic.Add(eOption.ASRate,               new fmOption { Kind = eOption.ASRate,           Value = 0f });
            m_dic.Add(eOption.ItemDropRate,         new fmOption { Kind = eOption.ItemDropRate,     Value = 0f });
            m_dic.Add(eOption.GoldDropRate,         new fmOption { Kind = eOption.GoldDropRate,     Value = 0f });
            m_dic.Add(eOption.Recovery,             new fmOption { Kind = eOption.Recovery,         Value = 0f });
            m_dic.Add(eOption.FindMagicItemRate,    new fmOption { Kind = eOption.FindMagicItemRate,Value = 0f });
            m_dic.Add(eOption.DEFRate,              new fmOption { Kind = eOption.DEFRate,          Value = 0f });
            m_dic.Add(eOption.HPRate,               new fmOption { Kind = eOption.HPRate,           Value = 0f });
            m_dic.Add(eOption.RecoveryRate,         new fmOption { Kind = eOption.RecoveryRate,     Value = 0f });
                                                                     
            m_dic.Add(eOption.ExtraAtkChance,       new fmOption { Kind = eOption.ExtraAtkChance,   Value = 0f });
            m_dic.Add(eOption.CrushingBlow,         new fmOption { Kind = eOption.CrushingBlow,     Value = 0f });
            m_dic.Add(eOption.PlusSetEffect,        new fmOption { Kind = eOption.PlusSetEffect,    Value = 0f });
            m_dic.Add(eOption.ExtraDMGToRareMon,    new fmOption { Kind = eOption.ExtraDMGToRareMon,Value = 0f });
            m_dic.Add(eOption.LegnendThornRate,     new fmOption { Kind = eOption.LegnendThornRate, Value = 0f });
            m_dic.Add(eOption.LegnendPoisonRate,    new fmOption { Kind = eOption.LegnendPoisonRate, Value = 0f });
            m_dic.Add(eOption.LegnendBurnRate,      new fmOption { Kind = eOption.LegnendBurnRate,      Value = 0f });
            m_dic.Add(eOption.LegnendFreezeRate,    new fmOption { Kind = eOption.LegnendFreezeRate,    Value = 0f });
            m_dic.Add(eOption.LegnendDMGReduceRate, new fmOption { Kind = eOption.LegnendDMGReduceRate, Value = 0f });


            m_dic.Add(eOption.SETAttack,            new fmOption { Kind = eOption.SETAttack,        Value = 0f });
            m_dic.Add(eOption.SETLuck,              new fmOption { Kind = eOption.SETLuck,          Value = 0f });
            m_dic.Add(eOption.SETFindMagicItemRate, new fmOption { Kind = eOption.SETFindMagicItemRate,  Value = 0f });
            m_dic.Add(eOption.SETExpRate          , new fmOption { Kind = eOption.SETExpRate          ,  Value = 0f });
            m_dic.Add(eOption.SETFire             , new fmOption { Kind = eOption.SETFire             ,  Value = 0f });
            m_dic.Add(eOption.SETIce              , new fmOption { Kind = eOption.SETIce              ,  Value = 0f });
            m_dic.Add(eOption.SETNature           , new fmOption { Kind = eOption.SETNature           ,  Value = 0f });
            m_dic.Add(eOption.SETNone             , new fmOption { Kind = eOption.SETNone             ,  Value = 0f });
            m_dic.Add(eOption.SETThorn            , new fmOption { Kind = eOption.SETThorn            ,  Value = 0f });
            m_dic.Add(eOption.SETPoison           , new fmOption { Kind = eOption.SETPoison           ,  Value = 0f });
            m_dic.Add(eOption.SETExtraStone       , new fmOption { Kind = eOption.SETExtraStone       ,  Value = 0f });
            m_dic.Add(eOption.SETRecovery         , new fmOption { Kind = eOption.SETRecovery         ,  Value = 0f });
            m_dic.Add(eOption.SETHP               , new fmOption { Kind = eOption.SETHP               ,  Value = 0f });
            m_dic.Add(eOption.SETBurn             , new fmOption { Kind = eOption.SETBurn             ,  Value = 0f });
            m_dic.Add(eOption.SETFreeze           , new fmOption { Kind = eOption.SETFreeze           ,  Value = 0f });

            m_dic.Add(eOption.AcDMGToRareMonRate    , new fmOption { Kind = eOption.AcDMGToRareMonRate  ,  Value = 0f });
            m_dic.Add(eOption.AcAllResistRate       , new fmOption { Kind = eOption.AcAllResistRate     ,  Value = 0f });
            m_dic.Add(eOption.AcHPRate              , new fmOption { Kind = eOption.AcHPRate            ,  Value = 0f });
            m_dic.Add(eOption.AcRecoveryRate        , new fmOption { Kind = eOption.AcRecoveryRate      ,  Value = 0f });

        }
        
        
        public int      ED                      { get { return (int)m_dic[eOption.ED                    ].Value; } set { m_dic[eOption.ED      ].Value = value;} }

        public int      ResistFireRate          { get { return (int)m_dic[eOption.ResistFireRate        ].Value; } set { m_dic[eOption.ResistFireRate   ].Value = value;} }
        public int      ResistIceRate           { get { return (int)m_dic[eOption.ResistIceRate         ].Value; } set { m_dic[eOption.ResistIceRate    ].Value = value;} }
        public int      ResistNatureRate        { get { return (int)m_dic[eOption.ResistNatureRate      ].Value; } set { m_dic[eOption.ResistNatureRate ].Value = value;} }
        public int      ResistNoneRate          { get { return (int)m_dic[eOption.ResistNoneRate        ].Value; } set { m_dic[eOption.ResistNoneRate   ].Value = value;} }
        public int      Sturn                   { get { return (int)m_dic[eOption.Sturn                 ].Value; } set { m_dic[eOption.Sturn            ].Value = value;} }
        public int      EXPRate                 { get { return (int)m_dic[eOption.EXPRate               ].Value; } set { m_dic[eOption.EXPRate          ].Value = value;} }
        public int      ThornRate               { get { return (int)m_dic[eOption.ThornRate             ].Value; } set { m_dic[eOption.ThornRate        ].Value = value;} }
        public int      PoisonRate              { get { return (int)m_dic[eOption.PoisonRate            ].Value; } set { m_dic[eOption.PoisonRate       ].Value = value;} }
        public int      ExtraStone              { get { return (int)m_dic[eOption.ExtraStone            ].Value; } set { m_dic[eOption.ExtraStone       ].Value = value;} }
        public int      BurnRate                { get { return (int)m_dic[eOption.BurnRate              ].Value; } set { m_dic[eOption.BurnRate         ].Value = value;} }
        public int      FreezeRate              { get { return (int)m_dic[eOption.FreezeRate            ].Value; } set { m_dic[eOption.FreezeRate       ].Value = value;} }
        //public int      IceRecoveryRate         { get { return (int)m_dic[eOption.IceRecoveryRate       ].Value; } set { m_dic[eOption.IceRecoveryRate  ].Value = value;} }
        
        
        


        public int      Lv                      { get; set; }
        public eElement Element                 { get { return (eElement)m_dic[eOption.Element          ].Value; } set { m_dic[eOption.Element              ].Value = (float)value; }}
        public float    CriRate                 { get { return      m_dic[eOption.CriRate               ].Value; } set { m_dic[eOption.CriRate              ].Value = value;}}
        public float    CriDamageRate           { get { return      m_dic[eOption.CriDamageRate         ].Value; } set { m_dic[eOption.CriDamageRate        ].Value = value;} }
        public int      ResistAll               { get { return (int)m_dic[eOption.ResistAll             ].Value; } set { m_dic[eOption.ResistAll            ].Value = value;} }
        public int      ResistFire              { get { return (int)m_dic[eOption.ResistFire            ].Value; } set { m_dic[eOption.ResistFire           ].Value = value;} }
        public int      ResistIce               { get { return (int)m_dic[eOption.ResistIce             ].Value; } set { m_dic[eOption.ResistIce            ].Value = value;} }
        public int      ResistNature            { get { return (int)m_dic[eOption.ResistNature          ].Value; } set { m_dic[eOption.ResistNature         ].Value = value;} }
        public int      ResistNone              { get { return (int)m_dic[eOption.ResistNone            ].Value; } set { m_dic[eOption.ResistNone           ].Value = value;} }
        public int      EDFire                  { get { return (int)m_dic[eOption.EDFire                ].Value; } set { m_dic[eOption.EDFire               ].Value = value;} }
        public int      EDIce                   { get { return (int)m_dic[eOption.EDIce                 ].Value; } set { m_dic[eOption.EDIce                ].Value = value;} }
        public int      EDNature                { get { return (int)m_dic[eOption.EDNature              ].Value; } set { m_dic[eOption.EDNature             ].Value = value;} }
        public int      EDNone                  { get { return (int)m_dic[eOption.EDNone                ].Value; } set { m_dic[eOption.EDNone               ].Value = value;} }
        public float    EDRateFire              { get { return      m_dic[eOption.EDRateFire            ].Value; } set { m_dic[eOption.EDRateFire           ].Value = value;} }
        public float    EDRateIce               { get { return      m_dic[eOption.EDRateIce             ].Value; } set { m_dic[eOption.EDRateIce            ].Value = value;} }
        public float    EDRateNature            { get { return      m_dic[eOption.EDRateNature          ].Value; } set { m_dic[eOption.EDRateNature         ].Value = value;} }
        public float    EDRateNone              { get { return      m_dic[eOption.EDRateNone            ].Value; } set { m_dic[eOption.EDRateNone           ].Value = value;} }
        public int      DEF                     { get { return (int)m_dic[eOption.DEF                   ].Value; } set { m_dic[eOption.DEF                  ].Value = value;} }
        public long     HP                      { get { return (int)m_dic[eOption.HP                    ].Value; } set { m_dic[eOption.HP                   ].Value = value;} }
        public int      BWDMin                  { get { return (int)m_dic[eOption.BWDMin                ].Value; } set { m_dic[eOption.BWDMin               ].Value = value;} }
        public int      BWDMax                  { get { return (int)m_dic[eOption.BWDMax                ].Value; } set { m_dic[eOption.BWDMax               ].Value = value;} }
        public int      WD                      { get { return (int)m_dic[eOption.WD                    ].Value; } set { m_dic[eOption.WD                   ].Value = value;} }
        public float    WDRate                  { get { return      m_dic[eOption.WDRate                ].Value; } set { m_dic[eOption.WDRate               ].Value = value;} }
        public float    AS                      { get { return      m_dic[eOption.AS                    ].Value; } set { m_dic[eOption.AS                   ].Value = value;} }
        public float    ASRate                  { get { return      m_dic[eOption.ASRate                ].Value; } set { m_dic[eOption.ASRate               ].Value = value;} }
        public float    ItemDropRate            { get { return      m_dic[eOption.ItemDropRate          ].Value; } set { m_dic[eOption.ItemDropRate         ].Value = value;} }
        public float    GoldDropRate            { get { return      m_dic[eOption.GoldDropRate          ].Value; } set { m_dic[eOption.GoldDropRate         ].Value = value;} }
        public int      Recovery                { get { return (int)m_dic[eOption.Recovery              ].Value; } set { m_dic[eOption.Recovery             ].Value = value;} }
        public int      FindMagicItemRate       { get { return (int)m_dic[eOption.FindMagicItemRate     ].Value; } set { m_dic[eOption.FindMagicItemRate    ].Value = value;} }
        public int      DEFRate                 { get { return (int)m_dic[eOption.DEFRate               ].Value; } set { m_dic[eOption.DEFRate              ].Value = value; } }
        public int      HPRate                  { get { return (int)m_dic[eOption.HPRate                ].Value; } set { m_dic[eOption.HPRate               ].Value = value; } }
        public int      RecoveryRate            { get { return (int)m_dic[eOption.RecoveryRate          ].Value; } set { m_dic[eOption.RecoveryRate         ].Value = value; } }
        

        public int      ExtraAtkChance          { get { return (int)m_dic[eOption.ExtraAtkChance        ].Value; } set { m_dic[eOption.ExtraAtkChance       ].Value = value;} }
        public int      CrushingBlow            { get { return (int)m_dic[eOption.CrushingBlow          ].Value; } set { m_dic[eOption.CrushingBlow         ].Value = value;} }
        public float    PlusSetEffect           { get { return      m_dic[eOption.PlusSetEffect         ].Value; } set { m_dic[eOption.PlusSetEffect        ].Value = value;} }
        public int      ExtraDMGToRareMon       { get { return (int)m_dic[eOption.ExtraDMGToRareMon     ].Value; } set { m_dic[eOption.ExtraDMGToRareMon    ].Value = value;} }
        public int      LegnendThornRate        { get { return (int)m_dic[eOption.LegnendThornRate      ].Value; } set { m_dic[eOption.LegnendThornRate     ].Value = value;} }
        public int      LegnendPoisonRate       { get { return (int)m_dic[eOption.LegnendPoisonRate     ].Value; } set { m_dic[eOption.LegnendPoisonRate    ].Value = value;} }
        public int      LegnendBurnRate         { get { return (int)m_dic[eOption.LegnendBurnRate       ].Value; } set { m_dic[eOption.LegnendBurnRate      ].Value = value;} }
        public int      LegnendFreezeRate       { get { return (int)m_dic[eOption.LegnendFreezeRate     ].Value; } set { m_dic[eOption.LegnendFreezeRate    ].Value = value;} }
        public int      LegnendDMGReduceRate    { get { return (int)m_dic[eOption.LegnendDMGReduceRate  ].Value; } set { m_dic[eOption.LegnendDMGReduceRate ].Value = value; } }

        public int      SETAttack               { get { return (int)m_dic[eOption.SETAttack             ].Value; } set { m_dic[eOption.SETAttack            ].Value = value;} }
        public int      SETLuck                 { get { return (int)m_dic[eOption.SETLuck               ].Value; } set { m_dic[eOption.SETLuck              ].Value = value; } }
        public int      SETFindMagicItemRate    { get { return (int)m_dic[eOption.SETFindMagicItemRate  ].Value; } set { m_dic[eOption.SETFindMagicItemRate ].Value = value; } }
        public int      SETExpRate              { get { return (int)m_dic[eOption.SETExpRate            ].Value; } set { m_dic[eOption.SETExpRate           ].Value = value;} }
        public int      SETFire                 { get { return (int)m_dic[eOption.SETFire               ].Value; } set { m_dic[eOption.SETFire              ].Value = value; } }
        public int      SETIce                  { get { return (int)m_dic[eOption.SETIce                ].Value; } set { m_dic[eOption.SETIce               ].Value = value; } }
        public int      SETNature               { get { return (int)m_dic[eOption.SETNature             ].Value; } set { m_dic[eOption.SETNature            ].Value = value; } }
        public int      SETNone                 { get { return (int)m_dic[eOption.SETNone               ].Value; } set { m_dic[eOption.SETNone              ].Value = value; } }
        public int      SETThorn                { get { return (int)m_dic[eOption.SETThorn              ].Value; } set { m_dic[eOption.SETThorn             ].Value = value; } }
        public int      SETPoison               { get { return (int)m_dic[eOption.SETPoison             ].Value; } set { m_dic[eOption.SETPoison            ].Value = value; } }
        public int      SETExtraStone           { get { return (int)m_dic[eOption.SETExtraStone         ].Value; } set { m_dic[eOption.SETExtraStone        ].Value = value; } }
        public int      SETRecovery             { get { return (int)m_dic[eOption.SETRecovery           ].Value; } set { m_dic[eOption.SETRecovery          ].Value = value; } }
        public int      SETHP                   { get { return (int)m_dic[eOption.SETHP                 ].Value; } set { m_dic[eOption.SETHP                ].Value = value; } }
        public int      SETBurn                 { get { return (int)m_dic[eOption.SETBurn               ].Value; } set { m_dic[eOption.SETBurn              ].Value = value; } }
        public int      SETFreeze               { get { return (int)m_dic[eOption.SETFreeze             ].Value; } set { m_dic[eOption.SETFreeze            ].Value = value; } }


        public int      AcDMGToRareMon          { get { return (int)m_dic[eOption.AcDMGToRareMonRate    ].Value; } set { m_dic[eOption.AcDMGToRareMonRate   ].Value = value; } }
        public int      AcAllResistRate         { get { return (int)m_dic[eOption.AcAllResistRate       ].Value; } set { m_dic[eOption.AcAllResistRate      ].Value = value; } }
        public int      AcHPRate                { get { return (int)m_dic[eOption.AcHPRate              ].Value; } set { m_dic[eOption.AcHPRate             ].Value = value; } }
        public int      AcRecoveryRate          { get { return (int)m_dic[eOption.AcRecoveryRate        ].Value; } set { m_dic[eOption.AcRecoveryRate       ].Value = value; } }

        public fmStats Clone()
        {
            //----------------------------------------------------
            fmStats stat = new fmStats();
            //----------------------------------------------------
            stat.Lv                     = Lv;
            stat.Element                = Element;
            //----------------------------------------------------
            stat.ED                     = ED;
            stat.ResistFireRate         = ResistFireRate;
            stat.ResistIceRate          = ResistIceRate;
            stat.ResistNatureRate       = ResistNatureRate;
            stat.ResistNoneRate         = ResistNoneRate;
            stat.Sturn                  = Sturn;
            stat.EXPRate                = EXPRate;
            stat.ThornRate              = ThornRate;
            stat.PoisonRate             = PoisonRate;
            stat.ExtraStone             = ExtraStone;
            stat.BurnRate               = BurnRate;
            stat.FreezeRate             = FreezeRate;
            //stat.IceRecoveryRate        = IceRecoveryRate;
            //----------------------------------------------------
            stat.CriRate                = CriRate;
            stat.CriDamageRate          = CriDamageRate;
            stat.ResistAll              = ResistAll;
            stat.ResistFire             = ResistFire;
            stat.ResistIce              = ResistIce;
            stat.ResistNature           = ResistNature;
            stat.ResistNone             = ResistNone;
            stat.EDFire                 = EDFire;
            stat.EDIce                  = EDIce;
            stat.EDNature               = EDNature;
            stat.EDNone                 = EDNone;
            stat.EDRateFire             = EDRateFire;
            stat.EDRateIce              = EDRateIce;
            stat.EDRateNature           = EDRateNature;
            stat.EDRateNone             = EDRateNone;
            stat.DEF                    = DEF;
            stat.HP                     = HP;
            stat.BWDMin                 = BWDMin;
            stat.BWDMax                 = BWDMax;
            stat.WD                     = WD;
            stat.WDRate                 = WDRate;
            stat.AS                     = AS;
            stat.ASRate                 = ASRate;
            stat.ItemDropRate           = ItemDropRate;
            stat.GoldDropRate           = GoldDropRate;
            stat.Recovery               = Recovery;
            stat.FindMagicItemRate      = FindMagicItemRate;
            stat.DEFRate                = DEFRate;
            stat.HPRate                 = HPRate;
            stat.RecoveryRate           = RecoveryRate;
            //----------------------------------------------------
            stat.ExtraAtkChance         = ExtraAtkChance;
            stat.CrushingBlow           = CrushingBlow;
            stat.PlusSetEffect          = PlusSetEffect;
            stat.ExtraDMGToRareMon      = ExtraDMGToRareMon;
            stat.LegnendThornRate       = LegnendThornRate;
            stat.LegnendPoisonRate      = LegnendPoisonRate;
            stat.LegnendBurnRate        = LegnendBurnRate;
            stat.LegnendFreezeRate      = LegnendFreezeRate;
            stat.LegnendDMGReduceRate   = LegnendDMGReduceRate;
            //----------------------------------------------------
            stat.SETAttack              = SETAttack;
            stat.SETLuck                = SETLuck;
            stat.SETFindMagicItemRate   = SETFindMagicItemRate;
            stat.SETExpRate             = SETExpRate;
            stat.SETFire                = SETFire  ;
            stat.SETIce                 = SETIce   ;
            stat.SETNature              = SETNature;
            stat.SETNone                = SETNone;
            stat.SETThorn               = SETThorn;
            stat.SETPoison              = SETPoison;
            stat.SETExtraStone          = SETExtraStone;
            stat.SETRecovery            = SETRecovery;
            stat.SETHP                  = SETHP      ;
            stat.SETBurn                = SETBurn;
            stat.SETFreeze              = SETFreeze;
            //-----------------------------------------------------
            stat.AcDMGToRareMon         = AcDMGToRareMon;
            stat.AcAllResistRate        = AcAllResistRate;
            stat.AcHPRate               = AcHPRate;
            stat.AcRecoveryRate         = AcRecoveryRate;

            return stat;
        }


        protected bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~fmStats()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            if (disposing)
            {
                //if (null != m_fnDic)
                //{
                //    m_fnDic.Clear();
                //    m_fnDic = null;
                //}
                if (null != m_dic)
                {
                    foreach (var node in m_dic)
                        node.Value.Dispose();

                    m_dic.Clear();
                    m_dic = null;
                }
            }
            m_disposed = true;
        }
    }
}
