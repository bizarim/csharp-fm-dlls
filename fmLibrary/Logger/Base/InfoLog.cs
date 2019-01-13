using System;
using System.Text;
using System.IO;
using System.Collections.Concurrent;

namespace fmLibrary
{
    public class InfoLog : ILog
    {
        private ConsoleLog m_console = new ConsoleLog();
        private readonly object m_lockObject = new object();
        private string m_strPath = null;

        private ConcurrentQueue<string> m_queue = new ConcurrentQueue<string>();

        public InfoLog()
        {
            m_strPath = string.Format("{0}/{1}", Directory.GetCurrentDirectory(), "Log");

            DirectoryInfo di = new DirectoryInfo(m_strPath);

            if (false == di.Exists)
                di.Create();
        }


        private void Write(string msg)
        {
            if (msg.Equals(string.Empty)) return;

            m_queue.Enqueue(msg);
            StratWrite();
        }

        private void StratWrite()
        {
            string total = string.Empty;
            string text = null;
            while (m_queue.TryDequeue(out text))
            {
                if (null == text) continue;
                total = string.Format("{0}{1}", total, text);
            }

            string strFile = string.Format("{0}/Log_{1}.txt", m_strPath, DateTime.Now.ToString("yyyyMMdd"));

            lock (m_lockObject)
            {
                try
                {
                    using (FileStream fs = new FileStream(strFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(total);
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void Write(eLogLevel eLog, string msg, params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} ", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
            sb.AppendFormat("{0} ", eLog.ToString().PadRight(5, ' '));
            sb.AppendFormat("[{0}] ", System.Threading.Thread.CurrentThread.ManagedThreadId);
            sb.AppendFormat(msg, args);
            Write(sb.ToString());
            m_console.Write(eLog, sb.ToString());
        }

        //public void Write(eLogLevel eLog, Exception ex)
        //{
        //    // 빈통
        //}

    }
}
