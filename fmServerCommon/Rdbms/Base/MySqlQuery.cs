using System.Data;
using MySql.Data.MySqlClient;

namespace fmServerCommon
{
    public abstract class MySqlQuery : IQuery
    {
        public CommandType m_eCommandType { get; protected set; }
        public eExecuteMode m_eExecuteMode { get; protected set; }
        public IsolationLevel m_eIsolationLevel { get; protected set; }

        protected MySqldbConn m_conn = null;
        protected MySqlCommand m_mySqlCommand = null;

        public MySqlQuery(string strConn)
        {
            m_conn = new MySqldbConn(strConn);
            m_eCommandType = CommandType.StoredProcedure;
            m_eExecuteMode = eExecuteMode.Reader;
        }
        
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
            m_conn = null;
            m_mySqlCommand = null;
            FreeParams();
        }

        public sealed override void OnResult(object reader, eDBError eError, string strError)
        {
            DataTableReader myReader = (DataTableReader)reader;
            OnResult(myReader, eError);
        }

        public bool Init(MySqlCommand command)
        {
            m_mySqlCommand = command;
            return InitParams(command);
        }

        protected abstract bool InitParams(MySqlCommand command);
        protected abstract void FreeParams();
        protected abstract void OnResult(DataTableReader reader, eDBError eError);
    }
}
