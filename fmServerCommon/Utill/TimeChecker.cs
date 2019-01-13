using System;

namespace fmServerCommon
{
    public class TimeChecker
    {
        private int m_nPeriodSec;
        private DateTime m_dateLastTime;

        public TimeChecker(int periodSec)
        {
            m_nPeriodSec = periodSec;
            m_dateLastTime = fmServerTime.Epoch;
        }

        public bool Check()
        {
            DateTime calcTime = m_dateLastTime.AddSeconds(m_nPeriodSec);
            DateTime now = fmServerTime.Now;

            if (now < calcTime)
                return false;

            m_dateLastTime = now;

            return true;
        }
    }
}
