

namespace fmCommon
{
    public enum eErrorCode
    {
        Success = 0,

        // Auth
        Auth_cVerError = 1,
        Auth_sVerError = 2,
        Auth_InvalidToken = 3,
        Auth_PleaseLogin = 4,
        Auth_CheckingServer = 5,

        // Lord
        Lord_NameLegth,
        Lord_NoneExist = 11,
        Lord_AlreadyExistName = 12,
        //Lord_AlreadyExist = 13,
        Lord_StatPoint_NotEnough,
        Lord_StateError,
        Gold_NotEnough,
        Ruby_NotEnough,
        Stone_NotEnough,
        Ticket_NotEnough,
        PvpPoint_NotEnough,
        DHeart_NotEnough,
        Seal_NotEnough,
        SCKey_NotEnough,
        // Item
        //Item_InvalidParts,
        Item_OutOfSlot,
        Item_NoneExist,
        Item_FailToEquip,
        Item_FailToSell,
        Item_FailRemove,
        //Item_NotEnough,
        Item_DoNotSellEquiped,
        Item_OutOfType,
        Item_Remelt_AlreadyOther,
        Item_NotEnoughSlot,
        Item_NotEnouhgLv,
        Item_Reroll_OverGrade,
        Item_Reroll_OverOpt,
        Item_Reroll_CanNotJewel,
        Item_UpToAncient_NotEngoughGrade,
        Item_DoNotUpToAncient_Equiped,
        Item_Enchant_ExitsPreList,
        Item_Enchant_NoneExitsList,
        Item_NoneOption,
        Item_AlreadySameOpt,
        Item_NotMythic,
        Item_NotAncient,
        Item_CanNotMythic,
        Item_NeedRune,
        Item_FailCombine,
        Item_CannotAncientMythic,
        Item_CanSameGrade,
        Item_NoRemelt,

        Mission_AlreadyComplete,
        Mission_NotEnoughCondition,
        Mission_AlreadyCondition,
        Mission_NotEnoughCnt,


        Explore_AbilityError,
        Explore_NotSearch,
        Explore_InvalidRound,
        Explore_InvalidMap,
        Explore_CloseMap,

        Maze_NotSearch,
        Maze_InvalidState,
        Maze_InvalidFloor,
        Maze_NotEngouhLv,
        Maze_MaxFloor,

        DragonTomb_Fail,
        DragonTomb_AbilityError,
        DragonTomb_NotEngouhCnt,
        DragonTomb_NotSearch,
        DragonTomb_NotEngoughNormalMap,

        Pvp_DoNotFoundUser,

        Shop_NoneExistGood,

        IAB_PleaseCheckState,
        IAB_NotPrepare,
        IAB_InvalidToken,
        IAB_GoogleReciptFail,

        DHeart_NotEngouhLv,
        DHeart_NotEnoughCnt,
        DHeart_NotSearch,

        InDun_CannotMovePlace,
        InDun_NotSearch,
        InDun_AlreadyUsedForge,
        InDun_NotYetUseForge,

        // Params
        Params_InvalidParam,

        // Check
        OP_FailedModifyInfo,
        OP_FailedSetNotice,
        OP_NoneNotice,
        Check_Server = 800,

        Qeury_NoneKey = 995,
        Query_Params = 996,
        Query_SyntaxError = 997,
        Query_Fail = 998,
        Error = 999,
        Server_Error = 10000,
        Server_TableError,
        Server_FailRegister,
        Server_CannotFindGameServer,
        Server_Block,
    }
}
