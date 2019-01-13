using fmCommon;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace fmServerCommon
{
    public class usp_LogAct : MySqlQuery
    {
        public fmLogAct i_logAct = null;

        public int o_nResult = 999;

        public usp_LogAct(string strConn) : base(strConn) { }

        protected override bool InitParams(MySqlCommand command)
        {
            m_strCommand = "usp_LogAct";
            m_eCommandType = CommandType.StoredProcedure;
            m_eIsolationLevel = IsolationLevel.ReadCommitted;
            m_eExecuteMode = eExecuteMode.NonQuery;

            command.Parameters.AddWithValue("@i_dateTime", i_logAct.Time);
            command.Parameters.AddWithValue("@i_packetType", i_logAct.PType);

            command.Parameters.AddWithValue("@i_biAccId", i_logAct.AccId);
            command.Parameters.AddWithValue("@i_nLv", i_logAct.Lv);
            command.Parameters.AddWithValue("@i_biGold", i_logAct.Gold);
            command.Parameters.AddWithValue("@i_nC1", i_logAct.C1);
            command.Parameters.AddWithValue("@i_nC2", i_logAct.C2);

            //command.Parameters.Add("@o_nResult", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            return true;
        }

        protected override void OnResult(DataTableReader reader, eDBError eError)
        {
            //o_nResult = Convert.ToInt32(m_mySqlCommand.Parameters["@o_nResult"].Value);
        }

        protected override void FreeParams()
        {
            i_logAct = null;
        }
    }
}
