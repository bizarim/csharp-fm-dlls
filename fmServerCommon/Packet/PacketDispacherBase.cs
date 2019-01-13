using fmCommon;
using fmLibrary;
using System.Collections.Generic;

namespace fmServerCommon
{
    //패킷 처리 관리자.
    public delegate IMessage fnCreate(SessionBase session, Packet packet);

    public class DispatcherBase
    {
        protected appServer m_server;
        protected Dictionary<eProtocolType, fnCreate> m_dicMessage = new Dictionary<eProtocolType, fnCreate>();

        public DispatcherBase(appServer server)
        {
            m_server = server;
            InitMessage();
        }

        public IMessage AllocMessage(SessionBase session, Packet packet)
        {
            eProtocolType type = packet.GeteProtocolType();

            if (false == m_dicMessage.ContainsKey(type))
                return null;

            return m_dicMessage[type](session, packet);
        }

        // fmProtocol 초기화
        protected virtual void InitMessage()
        {
            m_dicMessage.Add(eProtocolType.PT_Test_RQ, PT_Test_RQ);
            m_dicMessage.Add(eProtocolType.PT_Test_RS, PT_Test_RS);
        }

        protected virtual IMessage PT_Test_RQ(SessionBase session, Packet packet) { return new MsgTestRQ(m_server, session, packet); }
        protected virtual IMessage PT_Test_RS(SessionBase session, Packet packet) { return new MsgTestRS(m_server, session, packet); }
    }
}
