using StackExchange.Redis;
using fmCommon;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace fmServerCommon
{
    public static class RedisDataConverter
    {
        public static List<rdRubyLog> ToRubyLog(this List<HashEntry> list)
        {
            return list.Select(item => new JavaScriptSerializer().Deserialize<rdRubyLog>(item.Value)).ToList();
        }

        public static List<rdRubyLog> ToRubyLog(this HashEntry[] array)
        {
            return array.Select(item => new JavaScriptSerializer().Deserialize<rdRubyLog>(item.Value)).ToList();
        }

        public static List<rdIABLog> ToIABLog(this List<HashEntry> list)
        {
            return list.Select(item => new JavaScriptSerializer().Deserialize<rdIABLog>(item.Value)).ToList();
        }

        public static List<rdIABLog> ToIABLog(this HashEntry[] array)
        {
            return array.Select(item => new JavaScriptSerializer().Deserialize<rdIABLog>(item.Value)).ToList();
        }

        public static List<rdMail> ToRdMall(this List<HashEntry> list)
        {
            return list.Select(item => new JavaScriptSerializer().Deserialize<rdMail>(item.Value)).ToList();
        }

        public static List<rdMail> ToRdMall(this HashEntry[] array)
        {
            return array.Select(item => new JavaScriptSerializer().Deserialize<rdMail>(item.Value)).ToList();
        }

        public static List<fmRanker> ToRanker(this SortedSetEntry[] values)
        {
            int cnt = values.Length;

            List<fmRanker> list = new List<fmRanker>();

            for (int i = 0; i < cnt; ++i)
            {
                fmRankerKey temp = new JavaScriptSerializer().Deserialize<fmRankerKey>(values[i].Element);
                if (null == temp) continue;

                list.Add(new fmRanker
                {
                    Rank = i + 1,
                    Name = temp.Name,
                    Floor = (int)values[i].Score
                });
            }

            return list;
        }

    }
}
