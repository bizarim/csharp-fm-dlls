using fmCommon;
using System.Collections.Generic;

namespace fmServerCommon.Redis.Query
{
    // user redis query

    public class urq_SetItemInven : UserRedisQuery
    {
        private long m_biAccId = 0;
        public string i_strName = string.Empty;
        public List<rdItem> i_items = null;

        public urq_SetItemInven(eRedis db)
        {
            m_eDataBase = db;
        }

        public override eErrorCode Execute()
        {
            m_biAccId = GetAccIdWithName(i_strName);
            if (0 == m_biAccId)
                return eErrorCode.Lord_NoneExist;

            var db = GetDatabase();
            if (null == db)
                return eErrorCode.Server_Error;

            db.SetLordItems(m_biAccId, i_items);

            return eErrorCode.Success;
        }

        public override void Release()
        {
            //i_dsBatleState = null;
        }
    }
}
