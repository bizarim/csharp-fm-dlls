using fmCommon;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace fmServerCommon
{
    //public class rdRubyLog
    //{
    //    public DateTime Time = fmServerTime.Epoch;
    //    public PacketType Type = eProtocolType.PT_Unkwon;
    //    public long AccId = 0;
    //    public int Amount = 0;

    //    public string Contents = string.Empty;
    //}

    public class usp_LogRuby : MySqlQuery
    {
        public DateTime     i_dateTime      = fmServerTime.Epoch;
        public long         i_biAccId       = 0;
        public eProtocolType i_eProtocolType = eProtocolType.PT_Unkwon;
        public int          i_nAmount       = 0;

        public int o_nResult = 999;

        public usp_LogRuby(string strConn) : base(strConn) { }

        protected override bool InitParams(MySqlCommand command)
        {
            m_strCommand = "usp_LogRuby";
            m_eCommandType = CommandType.StoredProcedure;
            m_eIsolationLevel = IsolationLevel.ReadCommitted;
            m_eExecuteMode = eExecuteMode.NonQuery;

            command.Parameters.AddWithValue("@i_dateTime", i_dateTime);
            command.Parameters.AddWithValue("@i_packetType", i_eProtocolType);
            command.Parameters.AddWithValue("@i_biAccId", i_biAccId);
            command.Parameters.AddWithValue("@i_nAmount", i_nAmount);

            //command.Parameters.Add("@o_nResult", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            return true;
        }

        protected override void OnResult(DataTableReader reader, eDBError eError)
        {
            //o_nResult = Convert.ToInt32(m_mySqlCommand.Parameters["@o_nResult"].Value);
        }

        protected override void FreeParams()
        {
            i_dateTime      = fmServerTime.Epoch;
            i_biAccId       = 0;
            i_eProtocolType = eProtocolType.PT_Unkwon;
            i_nAmount       = 0;
        }
    }
}
