using System;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using fmLibrary;

namespace fmServerCommon
{
    // mysql 컨넥션
    public class MySqldbConn : IdbConnection
    {
        private string m_strConn;

        public MySqldbConn(string strConn)
        {
            m_strConn = strConn;
        }

        public override bool Execute(IQuery query)
        {
            if (null == query)
                return false;

            MySqlQuery myQuery = query as MySqlQuery;

            try
            {

                using (MySqlConnection conn = new MySqlConnection(m_strConn))
                {

                    MySqlCommand command = new MySqlCommand();

                    // command 설정
                    if (false == myQuery.Init(command))
                    {
                        conn.Close();
                        myQuery.OnResult(null, eDBError.InitParamFailed, "Failed");
                        return false;
                    }

                    // open
                    conn.Open();

                    //MySqlTransaction transaction = conn.BeginTransaction(myQuery.m_eIsolationLevel);
                    MySqlTransaction transaction = conn.BeginTransaction();

                    command.CommandType = myQuery.m_eCommandType;
                    command.CommandText = myQuery.m_strCommand;
                    command.Connection = conn;
                    command.Transaction = transaction;

                    try
                    {
                        switch (myQuery.m_eExecuteMode)
                        {
                            case eExecuteMode.Reader:
                                {
                                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                                    using (DataSet ds = new DataSet())
                                    {
                                        Task<int> task = adapter.FillAsync(ds);
                                        task.Wait();

                                        using (DataTableReader reader = ds.CreateDataReader())
                                        {
                                            myQuery.OnResult(reader, eDBError.Success, "success");
                                            reader.Close();
                                            transaction.Commit();
                                        }
                                    }
                                }
                                break;
                            case eExecuteMode.NonQuery:
                                {
                                    Task<int> task = command.ExecuteNonQueryAsync();
                                    task.Wait();
                                    //command.ExecuteNonQuery();
                                    //IAsyncResult resultAsync = null;
                                    //resultAsync = command.BeginExecuteReader();
                                    //resultAsync.AsyncWaitHandle.WaitOne();
                                    //int effectedRows = command.EndExecuteNonQuery(resultAsync);
                                    //myQuery.OnResult(null, eDBError.Success, string.Format("effected rows: {0}", effectedRows));
                                    transaction.Commit();
                                }
                                break;

                            default:
                                myQuery.OnResult(null, eDBError.InvalidExcuteMode, "Failed");
                                transaction.Commit();
                                return false;

                        }
                    }
                    catch (MySqlException ex)
                    {
                        // [ MUSTBE BY KWJ ] : 20150109
                        // 예외 일때 처리 해야한다.
                        // 롤백 밖에 없나?

                        transaction.Rollback();
                        Logger.Error(ex.ToString());
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            //MySqlConnection.ClearAllPools();

            return true;
        }
    }
}
