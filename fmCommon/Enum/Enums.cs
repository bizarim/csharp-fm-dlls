
namespace fmCommon
{
    /// <summary>
    /// 재화
    /// </summary>
    public enum eFinance
    {
        None = 0,
        Gold,
        Stone,
        Ruby,
        PvpPoint,
        DHeart,
        Seal,
    }

    /// <summary>
    /// 등급
    /// </summary>
    public enum eGrade
    {
        None        = 0,
        Normal      = 1,
        Magic       = 2,
        Rare        = 3,
        Epic        = 4,
        Legend      = 5,
        Set         = 6,
    }

    public enum eBeyond
    {
        None        = 0,
        Ancient     = 1,
        Mythic      = 2,
    }

    public enum eWeapon
    {
        None = 0,
        Speed = 1,
        Sharp = 2,
        Steel = 3,
    }

    /// <summary>
    /// 부위
    /// </summary>
    public enum eParts
    {
        None        = 0,
        Weapon      = 1,
        Necklace    = 2,
        Ring        = 3,
        Belt        = 4,
        Gloves      = 5,
        Pants       = 6,
        Armor       = 7,
        Head        = 8,
        Jewel       = 9,
    }

    //public enum eOptGrade
    //{
    //    Normal,
    //    Epic,
    //    Legend,
    //}

    public enum eOptGrade
    {
        None    = 0,
        Normal  = 1,
        Epic    = 2,
        Legend  = 3,
        Set     = 4,

        Ancient = 5,
    }

    /// <summary>
    /// 옵션
    /// </summary>
    public enum eOption
    {
        None                    = eOptGrade.None << 8,    // 없음
        Element                 = 1,
        //--------------------------
        // 드랍 옵션에 포함 안됨
        //--------------------------
        ED                      ,
        ResistFireRate          ,
        ResistIceRate           ,
        ResistNatureRate        ,
        ResistNoneRate          ,
        Sturn                   ,
        EXPRate                 ,
        ThornRate               ,
        PoisonRate              ,
        ExtraStone              ,
        BurnRate                ,
        FreezeRate              ,
        //IceRecoveryRate         ,

        //--------------------------

        //--------------------------
        // 드랍 옵션
        //--------------------------
        CriRate                 = eOptGrade.Normal << 8,
        CriDamageRate           ,
        ResistAll               ,
        ResistFire              ,
        ResistIce               ,
        ResistNature            ,
        ResistNone              ,
        EDFire                  ,
        EDIce                   ,
        EDNature                ,
        EDNone                  ,
        EDRateFire              ,
        EDRateIce               ,
        EDRateNature            ,
        EDRateNone              ,
        DEF                     ,
        HP                      ,
        BWDMin                  ,
        BWDMax                  ,
        WD                      ,
        WDRate                  ,
        AS                      ,
        ASRate                  ,
        ItemDropRate            ,
        GoldDropRate            ,
        Recovery                ,
        FindMagicItemRate       ,   // add
        DEFRate                 ,
        HPRate                  ,
        RecoveryRate            ,

        EpCriRate               = eOptGrade.Epic << 8,
        EpCriDamageRate         ,
        EpResistAll             ,
        EpResistFire            ,
        EpResistIce             ,
        EpResistNature          ,
        EpResistNone            ,
        EpEDFire                ,
        EpEDIce                 ,
        EpEDNature              ,
        EpEDNone                ,
        EpEDRateFire            ,
        EpEDRateIce             ,
        EpEDRateNature          ,
        EpEDRateNone            ,
        EpDEF                   ,
        EpHP                    ,
        EpBWDMin                ,
        EpBWDMax                ,
        EpWD                    ,
        EpWDRate                ,
        EpAS                    ,
        EpASRate                ,
        EpItemDropRate          ,
        EpGoldDropRate          ,
        EpRecovery              ,
        EpFindMagicItemRate     ,   // add
        EpDEFRate               ,
        EpHPRate                ,
        EpRecoveryRate          ,

        ExtraAtkChance          = eOptGrade.Legend << 8,
        CrushingBlow            ,
        PlusSetEffect           ,
        ExtraDMGToRareMon       ,   // add

        LegnendThornRate        ,
        LegnendPoisonRate       ,
        LegnendBurnRate         ,
        LegnendFreezeRate       ,
        LegnendDMGReduceRate    ,

        SETAttack               = eOptGrade.Set << 8,
        SETLuck                 ,
        SETFindMagicItemRate    ,   // add// 마법아이템 확률
        SETExpRate              ,   // 경험치 셋

        //// 저항 셋 - 3셋
        SETFire                 ,    // 화염    // add
        SETIce                  ,   // add
        SETNature               ,   // add
        SETNone                 ,   // add
        SETThorn                ,   // 가시   // add
        SETPoison               ,  // 독   // add
        SETExtraStone           ,   // add
        SETRecovery             ,
        SETHP                   ,
        SETBurn                 ,
        SETFreeze               ,

        // t3
        SETFireT3               ,
        SETIceT3                ,
        SETNatureT3             ,
        SETNoneT3               ,


        AcDMGToRareMonRate      = eOptGrade.Ancient << 8,
        AcAllResistRate         ,
        AcHPRate                ,
        AcRecoveryRate          ,
        //AcDEFRate,
    }


    /// <summary>
    /// 탐사 발견
    /// </summary>
    //public enum eDropKind
    //{
    //    None = 0,
    //    Gold = 1,
    //    Item = 2,
    //    Stone = 3,
    //    Ruby = 4,
    //    SCKey = 5,
    //}

    public enum eReward
    {
        None = 0,
        Gold = 1,
        Item = 2,
        Stone = 3,
        Ruby = 4,
        SCKey = 5,

        Exp = 98,
        ResetStats = 99,
    }

    /// <summary>
    /// 탐사 결과
    /// </summary>
    public enum eExploreResult
    {
        None = 0,
        Fail = 1,
        Capture = 2,
        Item = 3,
    }

    //public enum eKindMission
    //{
    //    None    = 0,
    //    Explore = 1,
    //    Maze    = 2,
    //    Dragon  = 3,
    //    Loot    = 4,
    //    Item    = 5,
    //    Shop    = 6,
    //}

    public enum eMission
    {
        None                = 0,
        ClearExplore,
        ClearInDun,
        ClearMaze,
        ClearDTomb,
        LootItemWhatEver,
        UseForge,
        ItemRemelt,
        ItemCombine,
        ItemSell,
    }


    public enum eBattleResult
    {
        Lose,
        Win,
    }

    public enum eAbnormal
    {
        None = 0,
        Sturn = 1,
        Freeze = 2,
    }

    public enum eKindAct
    {
        Pre     = 1,
        In      = 2,
        Post    = 3,
    }

    public enum eActType
    {
        None            = 0,

        Recovery        = eKindAct.Pre << 8,
        //IceRecovery     ,

        Nomal           = eKindAct.In << 8,
        ExtraAtk        ,
        CrushingBlow    ,
        Thorn           ,
        Sturn           ,
        Freeze          ,

        Posion          = eKindAct.Post << 8,
        Burn            ,
        
    }

    public enum eElement
    {
        None = 0,
        Fire = 1,
        Ice = 2,
        Nature = 3,
    }

    public enum eLevel
    {
        Normal      = 0,
        Hard        = 1,
        Nightmare   = 2,
        Hell        = 3,
    }

    public enum eTrait
    {
        Normal      = 0,

        HP          = 1,
        Power       = 2,
        CriCriD     = 3,
        Boss        = 4,

        Recovery    = 11,
        ExtraAtk    = 12,
        CrushBlow   = 13,
        Sturn       = 14,
        Poison      = 15,
        Thron       = 16,
        Burn        = 17,
        Freeze      = 18,
    }

    public enum eRareLv
    {
        Bronze = 1,
        Silver = 2,
        Gold = 3,
    }

    public enum eRefresh
    {
        Ruby,
        Gold,
        Ad,
        SKey,
    }

    public enum eShop
    {
        Google,
        IOS,
        Game,
    }

    public enum eAppOs
    {
        Google,
        IOS,
    }

    //public enum eLanguage
    //{
    //    None    = 0,
    //    ALL     = 1,

    //    KOR,    // 한국
    //    ENG,    // 영어
    //    CHN,    // 중국
    //    JPN,    // 일본
    //    RUS,    // 러시아
    //    ESP,    // 스페인
    //    FRA,    // 프랑스
    //    POR,    // 포루투칼
    //}

    public enum eLanguage
    {
        Afrikaans       = 0,
        Arabic          = 1,
        Basque          = 2,
        Belarusian      = 3,
        Bulgarian       = 4,
        Catalan         = 5,
        Chinese         = 6,
        Czech           = 7,
        Danish          = 8,
        Dutch           = 9,
        English         = 10,
        Estonian        = 11,
        Faroese         = 12,
        Finnish         = 13,
        French          = 14,
        German          = 15,
        Greek           = 16,
        Hebrew          = 17,
        Hungarian       = 18,
        Hugarian        = 18,
        Icelandic       = 19,
        Indonesian      = 20,
        Italian         = 21,
        Japanese        = 22,
        Korean          = 23,
        Latvian         = 24,
        Lithuanian      = 25,
        Norwegian       = 26,
        Polish          = 27,
        Portuguese      = 28,
        Romanian        = 29,
        Russian         = 30,
        SerboCroatian   = 31,
        Slovak          = 32,
        Slovenian       = 33,
        Spanish         = 34,
        Swedish         = 35,
        Thai            = 36,
        Turkish         = 37,
        Ukrainian       = 38,
        Vietnamese      = 39,
        Unknown         = 40,
    }

    //---------------------------------------------------

    /// <summary>
    /// 인핸스
    /// </summary>
    //public enum eEnhance
    //{
    //    None = 0,
    //    Reroll = 1,
    //    Resmelt = 2,
    //}


    public enum eUnit
    {
        Monster = 0,
        Dragon = 1,
        Goblin = 2,
        Dummy = 3,
    }



}
