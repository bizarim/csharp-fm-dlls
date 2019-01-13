using System;
using System.Collections.Generic;
using System.Linq;

namespace fmLibrary
{
    public interface IKeyGenerator
    {
        long Alloc();
        void Free(long resueKey);
        bool IsValid(long key);
    }

    public class LongKeyForClient : IKeyGenerator
    {
        // 클라이언트 관리번호 생성은 3일 마다 초기화하는 방식으로 만든다.
        // 그리고 재사용하지 않는다.
        // 이렇게 하는 이유는 세션은 재사용 하지 않고,
        // 클라이언트 세션은 1시간 이상 액션인 없으면 세션을 끊고, 쿠키만 남겨 놓기 때문에
        // 단순 네트워크 오류로 인한 새로은 컨넥션이면 쿠키 인증만 받으면 된다.
        // 클라이언트 재접속이면 로그인부터 하면 된다.
        private readonly object m_objectLock = new object();

        private readonly long m_biStartNumber = 100;
        private readonly int m_nDays = 30;
        private readonly long INVALKEY = 0;

        private DateTime m_dateLastRefresh = default(DateTime);
        private long m_biCurrentKey = 0;

        public long Alloc()
        {
            lock (m_objectLock)
            {
                DateTime calcDate = m_dateLastRefresh.AddDays(m_nDays);

                if (calcDate <= DateTime.Now)
                {
                    m_biCurrentKey = m_biStartNumber;
                    m_dateLastRefresh = DateTime.Now;
                }
                else
                    ++m_biCurrentKey;

                return m_biCurrentKey;
            }
        }

        public void Free(long resueKey)
        {
            // 재사용 하지 않는다.
        }

        public bool IsValid(long key)
        {
            return INVALKEY != key;
        }
    }

    public class LongKeyForServer : IKeyGenerator
    {
        // 서버 관리 번호는 기존과 같은 방식으로 한다.
        private readonly object m_objectLock = new object();
        private List<long> m_listReuseKey = new List<long>();

        private long m_biCurrentKey = 0;
        private readonly long INVALKEY = 0;

        public long Alloc()
        {
            lock (m_objectLock)
            {
                if (m_listReuseKey.Count <= 0)
                {
                    ++m_biCurrentKey;
                    return m_biCurrentKey;
                }

                long key = m_listReuseKey.ElementAt(0);
                m_listReuseKey.RemoveAt(0);

                return key;
            }
        }

        public void Free(long resueKey)
        {
            lock (m_objectLock)
            {
                m_listReuseKey.Add(resueKey);
            }
        }

        public bool IsValid(long key)
        {
            return INVALKEY != key;
        }
    }
}
