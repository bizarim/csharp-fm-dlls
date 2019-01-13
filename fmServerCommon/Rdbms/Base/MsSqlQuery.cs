using System.Data;
using System.Data.SqlClient;

namespace fmServerCommon
{
    public abstract class MsSqlQuery : IQuery
    {
        public CommandType m_eCommandType { get; protected set; }
        public eExecuteMode m_eExecuteMode { get; protected set; }

        protected MsSqldbConn m_conn = null;
        protected SqlCommand m_sqlCommand = null;

        public MsSqlQuery(string strConn)
        {
            m_conn = new MsSqldbConn(strConn);
            m_eCommandType = CommandType.StoredProcedure;
            m_eExecuteMode = eExecuteMode.Reader;
        }

        // Pool을 쓰기 위해서 바꿨다.
        public sealed override bool Execute()
        {
            return m_conn.Execute(this);
        }

        protected override void Dispose(bool disposing)
        {
            Free();
            base.Dispose(disposing);
        }

        protected override void Free()
        {
            // Pool을 쓰기 위해서 바꿨다.
            //m_connectionMysql = null;
            FreeParams();
        }

        public sealed override void OnResult(object reader, eDBError eError, string strError)
        {
            SqlDataReader myReader = (SqlDataReader)reader;
            OnResult(myReader, eError);
        }

        public bool Init(SqlCommand command)
        {
            m_sqlCommand = command;
            return InitParams(command);
        }

        protected abstract bool InitParams(SqlCommand command);
        protected abstract void FreeParams();
        protected abstract void OnResult(SqlDataReader reader, eDBError eErrorcode);
    }
}
