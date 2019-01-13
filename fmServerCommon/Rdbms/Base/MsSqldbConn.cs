using System;
using System.Data;
using System.Data.SqlClient;

namespace fmServerCommon
{
    // mssql 컨넥션
    public class MsSqldbConn : IdbConnection
    {
        private string m_strConn;

        public MsSqldbConn(string strConn)
        {
            m_strConn = strConn;
        }

        public override bool Execute(IQuery query)
        {
            if (null == query)
                return false;

            MsSqlQuery msQuery = query as MsSqlQuery;

            try
            {

                using (SqlConnection m_sqlConnection = new SqlConnection(m_strConn))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        // command 설정
                        if (false == msQuery.Init(command))
                        {
                            m_sqlConnection.Close();
                            msQuery.OnResult(null, eDBError.InitParamFailed, "Failed");
                            return false;
                        }

                        // open
                        m_sqlConnection.Open();

                        command.CommandType = msQuery.m_eCommandType;
                        command.CommandText = msQuery.m_strCommand;
                        command.Connection = m_sqlConnection;

                        switch (msQuery.m_eExecuteMode)
                        {
                            case eExecuteMode.Adapter:
                                {
                                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                    using (DataSet ds = new DataSet())
                                    {
                                        adapter.Fill(ds);

                                        using (DataTableReader reader = ds.CreateDataReader())
                                        {
                                            msQuery.OnResult(reader, eDBError.Success, "success");
                                            reader.Close();
                                        }
                                    }
                                }
                                break;
                            case eExecuteMode.Reader:
                                {
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        msQuery.OnResult(reader, eDBError.Success, "success");
                                        reader.Close();
                                    }
                                }
                                break;
                            case eExecuteMode.NonQuery:
                                {
                                    IAsyncResult resultAsync = null;
                                    resultAsync = command.BeginExecuteReader();
                                    resultAsync.AsyncWaitHandle.WaitOne();
                                    int effectedRows = command.EndExecuteNonQuery(resultAsync);
                                    msQuery.OnResult(null, eDBError.Success, string.Format("effected rows: {0}", effectedRows));
                                }
                                break;

                            default:
                                msQuery.OnResult(null, eDBError.InvalidExcuteMode, "Failed");
                                return false;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                fmLibrary.Logger.Error(string.Format("Failed Query {0}", this.GetType().Name), ex);
                return false;
            }

            return true;
        }
    }

}
