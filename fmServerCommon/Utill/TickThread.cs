using fmLibrary;
using System;
using System.Threading;

namespace fmServerCommon
{
    // http://msdn.microsoft.com/ko-kr/library/2x96zfy7(v=vs.110).aspx

    public class TickThread
    {
        public delegate void TickHandler(object context);
        public TickHandler m_delegateTickHandler;

        private Server m_server;
        private int m_nPeriod;
        private Thread m_thread;
        //private AutoResetEvent m_eventExit;
        private AutoResetEvent m_eventCompletion;

        public TickThread(Server server, TickHandler tickHandler, int period = 1000)
        {
            m_server = server;
            //m_eventExit = new AutoResetEvent(false);
            m_eventCompletion = new AutoResetEvent(false);

            m_delegateTickHandler += tickHandler;
            m_nPeriod = period;

            m_thread = new Thread(Run);
            m_thread.Start();
        }

        private void Run()
        {
            try
            {
                while (true)
                {
                    if (false == m_eventCompletion.WaitOne(m_nPeriod))
                        m_delegateTickHandler(null);

                    //if (true == m_eventExit.WaitOne(0))
                    //    break;
                }
            }
            catch (Exception ex)
            {
                //m_eventExit.Dispose();
                //m_eventExit = null;
                m_eventCompletion.Dispose();
                m_eventCompletion = null;

                Logger.Error("TickThread", ex);
                m_server.ShutDown();
            }
        }

        //public void Stop()
        //{
        //    m_eventExit.Set();
        //}
    }
}
