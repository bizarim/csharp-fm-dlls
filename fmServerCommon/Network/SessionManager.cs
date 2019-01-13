using fmLibrary;
using System.Collections.Concurrent;

namespace fmServerCommon
{
    // SessionManager는 Session이 생성 파괴 될때. 단순 Dic에 넣다 뺐다. 단순 관리. 혹은 이후 관리 이슈.기능 추가 목적.
    public abstract class SessionManager
    {
        public abstract bool TryAdd(SessionBase session);
        public abstract void Remove(SessionBase session);
        public virtual void Update() { }
    }

    public class ServerSessionManager : SessionManager
    {
        protected ConcurrentDictionary<long, SessionBase> m_dicSessions = new ConcurrentDictionary<long, SessionBase>();

        public override bool TryAdd(SessionBase session)
        {
            return m_dicSessions.TryAdd(session.GetNumber(), session);
        }

        public override void Remove(SessionBase session)
        {
            if (null == session)
            {
                Logger.Error("ServerSessionManager Remove() session == null");
                return;
            }
            if (false == m_dicSessions.ContainsKey(session.GetNumber()))
            {
                Logger.Error("Remove ContainsKey == false");
                return;
            }

            SessionBase outSession = null;
            m_dicSessions.TryRemove(session.GetNumber(), out outSession);
        }
    }
}
