using System;

namespace fmLibrary
{
    // [COMMENT BY KWJ] : 2016-03-22
    // 로그 좀 더 쉽게 쓰도록 수정

    public static class Logger
    {
        private static ILog m_info = new InfoLog();
        private static ILog m_error = new ErrorLog();

        public static void Log(string message, params object[] args)
        {
            m_info.Write(eLogLevel.Log, message, args);
        }

        public static void Debug(string message, params object[] args)
        {
#if DEBUG
            m_info.Write(eLogLevel.Debug, message, args);
#endif
        }

        public static void Info(string message, params object[] args)
        {
            m_info.Write(eLogLevel.Info, message, args);
        }

        public static void Warn(string message, params object[] args)
        {
            m_info.Write(eLogLevel.Warn, message, args);
        }

        public static void Error(string message, params object[] args)
        {
            m_error.Write(eLogLevel.Error, message, args);
        }
    }
}
