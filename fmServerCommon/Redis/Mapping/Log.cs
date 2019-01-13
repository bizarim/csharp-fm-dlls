using fmCommon;
using fmLibrary;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace fmServerCommon
{
    public class rdRubyLog
    {
        public DateTime Time = fmServerTime.Epoch;
        public eProtocolType Type = eProtocolType.PT_Unkwon;
        public long AccId = 0;
        public int Amount = 0;

        //public string Contents = string.Empty;
    }

    public class rdIABLog
    {
        public DateTime Time = fmServerTime.Epoch;
        //public string Token = string.Empty;
        public long AccId = 0;
        public eShop Shop = eShop.Google;
        public int Amount = 0;
        public float Cash = 0;
    }

    public static partial class RedisMapping
    {
        //Mail
        public static bool GetRubyLog(this IDatabase db, long accid, out List<rdRubyLog> obj)
        {
            obj = null;
            var vals = db.HashGetAll(GetKeyRubyLog(accid));
            if (null == vals) return false;
            if (vals.Count() <= 0) return false;

            var list = vals.OrderByDescending(x => x.Name).Select(x => x).ToList();
            obj = list.ToRubyLog();

            return true;
        }

        public static bool SetRubyLog(this IDatabase db, long accid, rdRubyLog obj)
        {
            return db.HashSet(GetKeyRubyLog(accid), obj.Time.Ticks, new JavaScriptSerializer().Serialize(obj));
        }


        public static bool GetIABLog(this IDatabase db, long accid, DateTime time, out List<rdIABLog> obj)
        {
            obj = null;
            var vals = db.HashGetAll(GetKeyIABLog(time));
            if (null == vals) return false;
            if (vals.Count() <= 0) return false;

            var list = vals.OrderByDescending(x => x.Name).Select(x => x).ToList();
            obj = list.ToIABLog();

            return true;
        }

        //public static bool SetIABLog(this IDatabase db, long accid, rdIABLog obj)
        //{
        //    return db.HashSet(GetKeyIABLog(obj.Time), obj.Token, new JavaScriptSerializer().Serialize(obj));
        //}

        //public static bool GetOpIABLog(this IDatabase db, string key, out List<rdIABLog> outList)
        //{
        //    outList = null;
        //    var vals = db.HashGetAll(string.Format("Log_IAB_{0}", key));
        //    if (null == vals) return false;
        //    if (vals.Count() <= 0) return false;

        //    var temp = vals.OrderByDescending(x => x.Name).Select(x => x).ToList();
        //    outList = temp.ToIABLog();

        //    return true;
        //}
    }
}
