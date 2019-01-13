

namespace fmServerCommon
{
    public enum eDBError
    {
        Success,
        InitParamFailed,
        DbConnectionMisMatched,
        SqlException,
        InvalidOperationException,
        Exception,
        InvalidExcuteMode,
    }

    public enum eExecuteMode
    {
        Adapter,
        Reader,
        NonQuery,
    }
}
