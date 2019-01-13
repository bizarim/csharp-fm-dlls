using fmCommon;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace fmServerCommon
{

    public static partial class RedisMapping
    {
        // Region Total Rank
        public static bool GetMazeTopRank(this IDatabase db, out List<fmRanker> rankers)
        {
            rankers = null;
            // 30명
            int maxCnt = 29;

            var list = db.SortedSetRangeByRankWithScores(GetKeyMazeRank(), start: 0, stop: maxCnt, order: Order.Descending);
            rankers = list.ToRanker();
            return true;
        }

        public static bool GetMyMazeRank(this IDatabase db, fmRankerKey myRankKey, out long myRank)
        {
            myRank = 0;
            string key = new JavaScriptSerializer().Serialize(myRankKey);
            var ranking = db.SortedSetRank(GetKeyMazeRank(), key, order: Order.Descending);
            if (null != ranking)
            {
                myRank = (long)ranking + 1;
            }

            return true;
        }

        public static bool SetMazeRank(this ITransaction trans, long accid, string name, int floor)
        {
            string strValue = new JavaScriptSerializer().Serialize(new fmRankerKey { AccId = accid, Name = name });

            trans.SortedSetAddAsync(GetKeyMazeRank(), strValue, floor);

            return true;
        }

        public static bool SetMazeRank(this IDatabase db, fmRankerKey frk, int floor)
        {
            string strValue = new JavaScriptSerializer().Serialize(frk);
            return db.SortedSetAdd(GetKeyMazeRank(), strValue, floor);
        }

    }
}
