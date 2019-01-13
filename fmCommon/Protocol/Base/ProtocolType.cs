namespace fmCommon
{
    /// <summary>
    ///  0 ~ 38개의 카테코리만 사용, 40부터는 서버 내부 용
    ///  한 카테고리 안에 128 개 프로토콜 사용
    /// </summary>
    enum eProtocolCategory
    {
        Test        = 0,
        Auth        = 1,    // 인증        // 1: 128 ~ 255
        Lord        = 2,    // 영주        // 2: 256 ~ 383
        Item        = 3,    // 아이템

        Shop        = 4,    // 상점
        Mission     = 5,    // 미션
        Explore     = 6,    // 탐사
        DragonTomb  = 7,    // 용의무덤
        Maze        = 8,    // 미궁
        Archive     = 9,    // 업적
        Rank        = 10,   // 랭킹
        IAB         = 11,   // 결제
        DHeart      = 12,   // 용의심장
        InDun       = 13,   // 인던

        OP          = 36,   // 운영
        Broadcast   = 37,   // 방송
        MAX         = 38,   
        Server      = 79,   // 서버 내부용
    }

    /// <summary>
    /// 프로토콜 열거값 네이밍 규칙 
    /// PT_ => 프로토콜
    /// PT_CA_ => client to auth
    /// PT_CA_Auth_ => 인증 카테코리
    /// PT_CA_Auth_Login => 구체적인 메타포
    /// PT_CA_Auth_Login_RQ => RQ, RS, NT (reqeust, response, notices)
    /// </summary>
    public enum eProtocolType
    {
        PT_Unkwon = 0,              // none
        PT_Test_RQ = 1,             // test request
        PT_Test_RS = 2,             // test response

        PT_Auth_Begin = eProtocolCategory.Auth << 7,
        PT_CA_Auth_Login_RQ,
        PT_CA_Auth_Login_RS,
        PT_CA_Auth_GetWorldList_RQ,
        PT_CA_Auth_GetWorldList_RS,
        PT_CG_Auth_GetConstant_RQ,
        PT_CG_Auth_GetConstant_RS,
        PT_Auth_End,

        PT_Lord_Begin = eProtocolCategory.Lord << 7,
        PT_CG_Lord_EnterWorld_RQ,
        PT_CG_Lord_EnterWorld_RS,
        PT_CG_Lord_CreateLord_RQ,
        PT_CG_Lord_CreateLord_RS,
        PT_CG_Lord_GetLord_RQ,
        PT_CG_Lord_GetLord_RS,
        PT_Lord_End,

        PT_Item_Begin = eProtocolCategory.Item << 7,
        PT_CG_Item_GetList_RQ,
        PT_CG_Item_GetList_RS,
        PT_CG_Item_Equip_RQ,
        PT_CG_Item_Equip_RS,
        PT_Item_End,

        PT_Shop_Begin = eProtocolCategory.Shop << 7,
        PT_CG_Shop_GetList_RQ,
        PT_CG_Shop_GetList_RS,
        PT_Shop_End,

        PT_CG_Mission_Begin = eProtocolCategory.Mission << 7,
        PT_CG_Mission_End,

        PT_Explore_Begin = eProtocolCategory.Explore << 7,

        PT_Explore_End,

        PT_DragonTomb = eProtocolCategory.DragonTomb << 7,

        PT_DragonTomb_End,

        PT_Maze_Begin = eProtocolCategory.Maze << 7,
        PT_Maze_End,

        PT_Archive_Begin = eProtocolCategory.Archive << 7,
        PT_Archive_End,

        PT_Rank_Begin = eProtocolCategory.Rank << 7,
        PT_CG_Rank_GetList_RQ,
        PT_CG_Rank_GetList_RS,
        PT_Rank_End,

        PT_IAB_Begin = eProtocolCategory.IAB << 7,
        PT_Purchase_End,

        PT_DHeart_Begin = eProtocolCategory.DHeart << 7,
        PT_DHeart_End,

        PT_InDun_Begin = eProtocolCategory.InDun << 7,
        PT_InDun_End,

        PT_OP_Begin = eProtocolCategory.OP << 7,
        PT_OP_End,

        PT_Broadcast_Begin = eProtocolCategory.Broadcast << 7,
        PT_GC_Broadcast_Public_NT,
        PT_OC_Broadcast_Public_NT,
        PT_OC_Broadcast_Private_NT,
        PT_OA_Broadcast_SetNotice_RQ,
        PT_OA_Broadcast_SetNotice_RS,
        PT_CA_Broadcast_GetNotice_RQ,
        PT_CA_Broadcast_GetNotice_RS,
        PT_Broadcast_End,

        // Server 용
        PT_Server_Begin = eProtocolCategory.Server << 7,
        PT_Server_RegisterAtCenter_RQ,
        PT_Server_RegisterAtCenter_RS,
        PT_Server_ReadyToStart_RQ,
        PT_Server_ReadyToStart_RS,
        PT_Server_UpdateWorldState_NT,
        PT_AC_Server_GetWorldList_RQ,
        PT_AC_Server_GetWorldList_RS,
        PT_CA_Server_GetWorldList_NT,
        PT_Server_RegisterAtChat_RQ,
        PT_Server_RegisterAtChat_RS,

        PT_Server_End,
    }
}
