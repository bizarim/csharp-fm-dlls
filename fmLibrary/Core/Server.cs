using System;
using System.IO;
using System.Xml.Serialization;

namespace fmLibrary
{
    public class Server : IServer
    {
        // [ MUSTBE BY KWJ ] : 2014.12.22
        // stopped, running 추가하자.
        public fmBool m_bStopped = null;
        public fmBool m_bRunning = null;

        protected ServerConfig m_config = new ServerConfig();       // Config
        //public ServerConfig GetConfig() { return m_config; }
        protected ConsoleHandler m_consoleHandler = new ConsoleHandler();     // ConsoleHandler

        public virtual bool Initialize()
        {
            m_bStopped = new fmBool();
            m_bRunning = new fmBool();

            // 관리되지 않는 예외 일 경우 핸들러
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);

            // 정책: 서버별 내부 쓰레드 수 조절
            // ThreadPoolEx 안에 추가 설명 적어 놓음.
            TheadPoolEx.SetMinMaxThreads(
                m_config.m_thread.m_nMinSync == 0 ? Environment.ProcessorCount : m_config.m_thread.m_nMinSync,
                m_config.m_thread.m_nMaxSync == 0 ? (int)(Environment.ProcessorCount * 2) : m_config.m_thread.m_nMaxSync,
                m_config.m_thread.m_nMinAsync == 0 ? Environment.ProcessorCount : m_config.m_thread.m_nMinAsync,
                m_config.m_thread.m_nMaxAsync == 0 ? (int)(Environment.ProcessorCount * 3) : m_config.m_thread.m_nMaxAsync);

            int maxsync;
            int maxAsync;
            System.Threading.ThreadPool.GetMaxThreads(out maxsync, out maxAsync);

            Logger.Info("Server Initialize-> maxsync:{0}, maxAsync:{1}]", maxsync, maxAsync);
            return true;
        }

        public virtual void Uninitialize() { }

        public virtual bool Start() { return true; }

        public virtual bool Stop()
        {
            if (false == m_bStopped.SetTrue())
                return false;

            Logger.Info("Server Stop");
            return true;
        }

        public virtual bool LoadConfig(string[] args)
        {
            // 목적 : 번거롭게 만자.
            // 서버 실행 args를 받는다. arguments에는 파일이름만 받자!!. 번거롭긴해도 외부 사람들이 잘못 이용하는 것 보다?

            if (args.Length < 1)
            {
                Logger.Info("Failed. Load Config Without filename");
                return false;
            }

            string configfilename = args[0];
            string path = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), configfilename);
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ServerConfig));
                    m_config = serializer.Deserialize(sr) as ServerConfig;

                    Logger.Info("Load Config: {0}", configfilename);
                    Logger.Info("db Acc: {0}", m_config.m_db.m_myAcc);
                    

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return false;
            }
        }

        public virtual bool LoadData() { return true; }

        public virtual void Ready()
        {
            m_consoleHandler.TurnOn();
        }

        protected virtual void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Logger.Error("OnUnhandledException: {0}", args.ExceptionObject.ToString());
#if DEBUG
            Logger.Error("OnUnhandled sender: {0}", sender);
            ShutDown();
#else
            ShutDown();
#endif

        }

        public void ShutDown()
        {
            Environment.Exit(1);
        }
    }
}
