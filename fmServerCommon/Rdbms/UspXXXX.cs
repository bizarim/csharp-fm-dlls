using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace fmServerCommon
{
    // 테스트
    public class UspXXXX : MySqlQuery
    {
        public UspXXXX(string strConn) : base(strConn) { }

        protected override bool InitParams(MySqlCommand command)
        {
            m_eIsolationLevel = IsolationLevel.ReadUncommitted;
            m_strCommand = "usp_xxxx";
            m_eCommandType = CommandType.StoredProcedure;
            return true;
        }

        protected override void OnResult(DataTableReader reader, eDBError eError)
        {
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                }
            }
        }

        protected override void FreeParams()
        {
        }
    }
}
