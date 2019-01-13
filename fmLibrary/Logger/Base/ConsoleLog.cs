using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmLibrary
{
    public class ConsoleLog : IDisposable
    {
        protected delegate void fnLogging(string msg);
        private Dictionary<eLogLevel, fnLogging> m_dic = null;

        public ConsoleLog()
        {
            m_dic = new Dictionary<eLogLevel, fnLogging>();
            m_dic.Clear();

            m_dic.Add(eLogLevel.Log,    Log);
            m_dic.Add(eLogLevel.Debug,  Debug);
            m_dic.Add(eLogLevel.Info,   Info);
            m_dic.Add(eLogLevel.Warn,   Warning);
            m_dic.Add(eLogLevel.Error,  Error);
        }

        public void Write(eLogLevel eLog, string msg)
        {
            if (null == m_dic) return;
            if (!m_dic.ContainsKey(eLog)) return;
            m_dic[eLog](msg);
        }

        private void Log(string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(msg);
        }

        private void Debug(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(msg);
        }

        private void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
        }

        private void Warning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
        }

        private void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
        }


        bool m_disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ConsoleLog()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            // if (disposing) { }
            if (null != m_dic)
            {
                m_dic.Clear();
                m_dic = null;
            }
            m_disposed = true;
        }
    }
}
