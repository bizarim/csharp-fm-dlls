using fmCommon;
using fmLibrary;
using System.Collections.Generic;

namespace fmServerCommon
{
    // 순차 호출 방식으로 한다. 단순히 network단에서 오는 걸 분배 하는 식으로 생각.
    // 서버에 붙여서 사용 하려고 singleton으로 하지 않았다.
    // start 할때 thread 수 넣을 수 있도록 수정

    public class MessageExecuter
    {
        private readonly object m_objLock = new object();

        private DispatcherBase m_dispatcher = null;

        private int m_nMaxCount = 0;
        private int m_nCurId = 0;
        private List<WorkerQueue> m_listWorker = new List<WorkerQueue>();

        public enum eState { None, Run, };
        private eState m_eState;

        private WorkerQueue GetWorker()
        {
            lock(m_objLock)
            {
                if (m_nMaxCount <= ++m_nCurId) m_nCurId = 0;
            }

            return m_listWorker[m_nCurId];
        }

        public bool Start(DispatcherBase dispatcher, int cnt = 1)
        {
            if (null == dispatcher) return false;
            if (eState.None != m_eState) return false;

            m_dispatcher = dispatcher;
            m_nMaxCount = cnt;
            m_listWorker.Clear();

            for (int i = 0; i < m_nMaxCount; ++i)
            {
                m_listWorker.Add(new WorkerQueue(i + 1));
            }

            m_eState = eState.Run;
            Logger.Info("Start MessageExecuter Count:{0}", m_nMaxCount);
            return true;
        }

        public bool Push(IMessage message)
        {
            return GetWorker().Push(message);
        }

        public void Stop()
        {
            m_eState = eState.None;
        }

        public bool TryGetMessage(SessionBase session, Packet packet, out IMessage msg)
        {
            msg = null;

            if (eState.None == m_eState)
            {
                Logger.Warn("MessageExecuter do not run");
                return false;
            }

            msg = m_dispatcher.AllocMessage(session, packet);
            if (null == msg)
            {
                Logger.Warn("Do not Dispatch: {0}", packet.GeteProtocolType());
                return false;
            }

            return true;
        }

        public void Dispatch(SessionBase session, Packet packet)
        {
            if (eState.None == m_eState)
            {
                Logger.Warn("MessageExecuter do not run");
                return;
            }

            IMessage msg = m_dispatcher.AllocMessage(session, packet);
            if (null == msg)
            {
                Logger.Warn("Do not Dispatch: {0}", packet.GeteProtocolType());
                return;
            }

            Logger.Debug("{0}", packet.GeteProtocolType());
            Push(msg);
        }
    }
}
