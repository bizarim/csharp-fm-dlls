using System;
using System.Threading;

namespace fmLibrary
{
    public class ConsoleHandler
    {
        private ManualResetEvent m_stoper;
        private Thread m_threadConsole;

        public void TurnOn()
        {
            Console.WriteLine("CTRL+C to interrupt the read operation");
            m_stoper = new ManualResetEvent(false);
            Console.CancelKeyPress += KeyPressHandler;
            m_threadConsole = new Thread(Loop);
            m_threadConsole.Start();
        }

        void Loop()
        {
            while (true)
            {
                if (true == m_stoper.WaitOne())
                    break;
            }
        }

        void KeyPressHandler(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("The read operation has been interrupted.");
            Console.WriteLine("Do you want to Stop Server?  Enter 'exit'");
            Console.Write("Enter:");

            string msg = Console.ReadLine();
            if (null == msg)
            {
                Console.Write("Message is Null");
                return;
            }

            if (!msg.Equals("exit"))
                args.Cancel = true;

            Console.WriteLine("Input: {0}", msg);
            Console.WriteLine("The read operation will resume...");
        } 
    }
}
