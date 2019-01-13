using fmCommon;
using System;

namespace fmServerCommon
{
    public class fmMissionBase
    {
        public int RefreshCnt { get; set; }
        public DateTime MissionTime { get; set; }

        public rdMissionBase ToClient()
        {
            DateTime next = fmServerTime.Date.AddDays(1);
            int temp = 0;

            if (fmServerTime.Date <= MissionTime)
            {
                //TimeSpan tp = (next - fmServerTime.Now);
                //temp = (int)(next - MissionTime ).TotalSeconds;
                temp = (int)(next - fmServerTime.Now).TotalSeconds;
            }

            return new rdMissionBase
            {
                RefreshCnt = RefreshCnt,
                RemainSec = temp,
            };
        }
    }
}

