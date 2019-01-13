using fmCommon;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace fmServerCommon
{
    public class usp_LogRQ : MySqlQuery
    {
        public DateTime i_dateTime = fmServerTime.Epoch;
        public eProtocolType i_eProtocolType = eProtocolType.PT_Unkwon;

        public int o_nResult = 999;

        public usp_LogRQ(string strConn) : base(strConn) { }

        protected override bool InitParams(MySqlCommand command)
        {
            m_strCommand = "usp_LogRQ";
            m_eCommandType = CommandType.StoredProcedure;
            m_eIsolationLevel = IsolationLevel.ReadCommitted;
            m_eExecuteMode = eExecuteMode.NonQuery;

            command.Parameters.AddWithValue("@i_dateTime", i_dateTime);
            command.Parameters.AddWithValue("@i_protocolType", i_eProtocolType);

            //command.Parameters.Add("@o_nResult", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            return true;
        }

        protected override void OnResult(DataTableReader reader, eDBError eError)
        {
            //o_nResult = Convert.ToInt32(m_mySqlCommand.Parameters["@o_nResult"].Value);
        }

        protected override void FreeParams()
        {
            i_dateTime = fmServerTime.Epoch;
            i_eProtocolType = eProtocolType.PT_Unkwon;
        }
    }
}
