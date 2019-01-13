using fmCommon;
using fmLibrary;

namespace fmServerCommon
{
    public class MsgTestRQ : IMessage
    {
        Session m_session = null;
        Packet m_recvPacket = null;

        public MsgTestRQ(appServer server, SessionBase session, Packet packet)
        {
            m_server = server;
            m_recvPacket = packet;
            m_session = session as Session;
        }

        public override void Process()
        {
            TestStopWatch watch = new TestStopWatch();
            watch.Start();
            watch.name = "MsgTestRQ";

            using (PT_Test_RQ RQ = new PT_Test_RQ())
            {
                RQ.Deserialize(m_recvPacket);

                using (PT_Test_RS RS = new PT_Test_RS())
                {
                    for (int i = 0; i < 1999999; ++i)
                    {
                    }

                    RS.m_eErrorCode = eErrorCode.Success;
                    m_session.SendPacket(RS);
                }
            }

            watch.Stop();
        }
        protected override void Release()
        {
            m_session = null;
            m_recvPacket.Dispose();
            m_recvPacket = null;
        }
        public override void Exclude()
        {
            // 클라이언트 세션일때
            //m_session.Logout();
            //Logger.Debug("Exclude");
        }
    }

    public class MsgTestRS : IMessage
    {
        Session m_session = null;
        fmProtocol m_fmProtocol = null;

        public MsgTestRS(appServer server, SessionBase session, Packet packet)
        {
            m_fmProtocol = new PT_Test_RS();
            m_fmProtocol.Deserialize(packet);
            m_session = session as Session;
        }

        public override void Process()
        {
            Logger.Debug("MsgTestRS");
        }
        protected override void Release()
        {
            m_session = null;
            m_fmProtocol.Dispose();
            m_fmProtocol = null;
        }
        public override void Exclude()
        {
            // 클라이언트 세션일때
            //m_session.Logout();
        }
    }
}
