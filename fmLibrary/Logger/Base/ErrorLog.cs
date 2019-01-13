using System;
using System.Text;
using System.IO;
//using System.Diagnostics;

namespace fmLibrary
{
    public class ErrorLog : ILog
    {
        private ConsoleLog m_console = new ConsoleLog();
        private readonly object m_lockObject = new object();
        private string m_strPath = null;

        public ErrorLog()
        {
            m_strPath = string.Format("{0}/{1}", Directory.GetCurrentDirectory(), "Error");

            DirectoryInfo di = new DirectoryInfo(m_strPath);

            if (false == di.Exists)
                di.Create();
        }

        private void Write(string text)
        {   
            string strFile = string.Format("{0}/Log_ERROR_{1}.txt", m_strPath, DateTime.Now.ToString("yyyyMMddHHmmssfff"));

            lock (m_lockObject)
            {
                try
                {
                    using (FileStream fs = new FileStream(strFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(text);
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
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("{0} ", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        //    sb.AppendFormat("{0} ", eLog.ToString().PadRight(5, ' '));
        //    sb.AppendFormat("[{0}] ", System.Threading.Thread.CurrentThread.ManagedThreadId);
        //    sb.AppendFormat("{0}", ex.ToString());
        //    Write(sb.ToString());
        //    m_console.Write(eLog, sb.ToString());
        //}

        //private void Write(Exception ex)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine(string.Format("Source : [{0}]", ex.Source));
        //    sb.AppendLine(string.Format("TargetSite : [{0}]", ex.TargetSite));
        //    sb.AppendLine(ex.Message);
        //    sb.AppendLine(ex.StackTrace);

        //    // Create a StackTrace that captures filename, 
        //    // line number and column information.
        //    sb.AppendLine();
        //    sb.AppendLine();
        //    StackTrace st = new StackTrace(true);
        //    string stackIndent = "";
        //    for (int i = 0; i < st.FrameCount; i++)
        //    {
        //        // Note that at this level, there are four 
        //        // stack frames, one for each method invocation.
        //        StackFrame sf = st.GetFrame(i);
        //        sb.AppendLine();

        //        sb.AppendLine(string.Format("{0} Method: {1} => {2}", stackIndent, sf.GetMethod().ReflectedType, sf.GetMethod()));
        //        sb.AppendLine(string.Format("{0} File: {1}", stackIndent, sf.GetFileName()));
        //        sb.AppendLine(string.Format("{0} Line Number: {1}", stackIndent, sf.GetFileLineNumber()));
        //        stackIndent += "  ";
        //    }

        //    Write(ex.ToString());
        //}
    }
}
