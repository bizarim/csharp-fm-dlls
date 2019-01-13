using System;

namespace fmLibrary
{
    public interface ILog
    {
        void Write(eLogLevel eLog, string msg, params object[] args);
        //void Write(eLogLevel eLog, Exception ex);
    }
}
