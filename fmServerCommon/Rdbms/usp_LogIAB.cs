using fmCommon;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace fmServerCommon
{
    //public class rdIABLog
    //{
    //    public DateTime Time = fmServerTime.Epoch;
    //    public string Token = string.Empty;
    //    public long AccId = 0;
    //    public eShop Shop = eShop.Google;
    //    public int Amount = 0;
    //    public float Cash = 0;
    //}

    public class usp_LogIAB : MySqlQuery
    {
        public DateTime     i_dateTime  = fmServerTime.Epoch;
        public long         i_biAccId   = 0;
        public eShop        i_Shop      = eShop.Google;
        public int          i_nAmount   = 0;
        public float        i_fCash     = 0;

        public int o_nResult = 999;

        public usp_LogIAB(string strConn) : base(strConn) { }

        protected override bool InitParams(MySqlCommand command)
        {
            m_strCommand = "usp_LogIAB";
            m_eCommandType = CommandType.StoredProcedure;
            m_eIsolationLevel = IsolationLevel.ReadCommitted;
            m_eExecuteMode = eExecuteMode.NonQuery;

            command.Parameters.AddWithValue("@i_dateTime", i_dateTime);
            command.Parameters.AddWithValue("@i_biAccId", i_biAccId);
            command.Parameters.AddWithValue("@i_Shop", i_Shop);
            command.Parameters.AddWithValue("@i_nAmount", i_nAmount);
            command.Parameters.AddWithValue("@i_fCash", i_fCash);

            //command.Parameters.Add("@o_nResult", MySqlDbType.Int32).Direction = ParameterDirection.Output;

            return true;
        }

        protected override void OnResult(DataTableReader reader, eDBError eError)
        {
            //o_nResult = Convert.ToInt32(m_mySqlCommand.Parameters["@o_nResult"].Value);
        }

        protected override void FreeParams()
        {
            i_dateTime  = fmServerTime.Epoch;
            i_biAccId   = 0;
            i_Shop      = eShop.Google;
            i_nAmount   = 0;
            i_fCash     = 0;
        }
    }
}
