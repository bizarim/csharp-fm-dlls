using fmCommon;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace fmServerCommon
{
    public static partial class RedisMapping
    {
        public static bool IsExistsLord(this IDatabase db, long accid)
        {
            return db.KeyExists(GetKeyLordInfo(accid));
        }

        // lordInfo
        public static bool GetLordBase(this IDatabase db, long accid, out fmLordBase obj)
        {
            obj = null;
            string strValue = db.StringGet(GetKeyLordInfo(accid));
            if (true == string.IsNullOrEmpty(strValue))
                return false;

            obj = new JavaScriptSerializer().Deserialize<fmLordBase>(strValue);

            return true;
        }

        public static bool SetLordBase(this IDatabase db, long accid, fmLordBase dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            return db.StringSet(GetKeyLordInfo(accid), strValue);
        }

        public static void SetLordBase(this ITransaction trans, long accid, fmLordBase dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            trans.StringSetAsync(GetKeyLordInfo(accid), strValue);
        }
        // Stat
        public static bool GetLordStat(this IDatabase db, long accid, out rdStat obj)
        {
            obj = null;
            string strValue = db.StringGet(GetKeyLordStat(accid));
            if (true == string.IsNullOrEmpty(strValue))
                return false;
            obj = new JavaScriptSerializer().Deserialize<rdStat>(strValue);
            return true;
        }

        public static bool SetLordStat(this IDatabase db, long accid, rdStat dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            return db.StringSet(GetKeyLordStat(accid), strValue);
        }

        public static void SetLordStat(this ITransaction trans, long accid, rdStat dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            trans.StringSetAsync(GetKeyLordStat(accid), strValue);
        }

        // Mission
        public static bool GetMissions(this IDatabase db, long accid, out List<rdMission> obj)
        {
            obj = null;
            string strValue = db.StringGet(GetKeyLordMission(accid));
            if (true == string.IsNullOrEmpty(strValue))
                return false;
            obj = new JavaScriptSerializer().Deserialize<List<rdMission>>(strValue);
            return true;
        }
        public static bool SetMissions(this IDatabase db, long accid, List<rdMission> dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            return db.StringSet(GetKeyLordMission(accid), strValue);
        }
        public static void SetMissions(this ITransaction trans, long accid, List<rdMission> dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            trans.StringSetAsync(GetKeyLordMission(accid), strValue);
        }

        public static bool GetMissionBase(this IDatabase db, long accid, out fmMissionBase obj)
        {
            obj = null;
            string strValue = db.StringGet(GetKeyLordMissionBase(accid));

            if (true == string.IsNullOrEmpty(strValue))
            {
                obj = new fmMissionBase
                {
                    RefreshCnt = 3,
                    MissionTime = fmServerTime.Epoch,
                };
                return true;
            }

            obj = new JavaScriptSerializer().Deserialize<fmMissionBase>(strValue);

            if (null == obj)
                obj = new fmMissionBase
                {
                    RefreshCnt = 3,
                    MissionTime = fmServerTime.Epoch,
                };

            return true;
        }

        public static bool SetMissionBase(this IDatabase db, long accid, fmMissionBase dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            return db.StringSet(GetKeyLordMissionBase(accid), strValue);
        }
        public static void SetMissionBase(this ITransaction trans, long accid, fmMissionBase dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            trans.StringSetAsync(GetKeyLordMissionBase(accid), strValue);
        }

        // Map
        public static bool GetMaps(this IDatabase db, long accid, out List<rdMap> obj)
        {
            obj = null;
            string strValue = db.StringGet(GetKeyLordMap(accid));
            if (true == string.IsNullOrEmpty(strValue))
                return false;
            obj = new JavaScriptSerializer().Deserialize<List<rdMap>>(strValue);
            return true;
        }
        public static bool SetMaps(this IDatabase db, long accid, List<rdMap> dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            return db.StringSet(GetKeyLordMap(accid), strValue);
        }
        public static void SetMaps(this ITransaction trans, long accid, List<rdMap> dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            trans.StringSetAsync(GetKeyLordMap(accid), strValue);
        }

        //GetLordInDuns
        public static bool GetLordInDuns(this IDatabase db, long accid, out List<rdInDun> obj)
        {
            obj = null;
            string strValue = db.StringGet(GetKeyLordInDun(accid));
            if (true == string.IsNullOrEmpty(strValue))
                return false;
            obj = new JavaScriptSerializer().Deserialize<List<rdInDun>>(strValue);
            return true;
        }
        public static bool SetLordInDuns(this IDatabase db, long accid, List<rdInDun> dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            return db.StringSet(GetKeyLordInDun(accid), strValue);
        }
        public static void SetLordInDuns(this ITransaction trans, long accid, List<rdInDun> dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            trans.StringSetAsync(GetKeyLordInDun(accid), strValue);
        }
        
    }
}
