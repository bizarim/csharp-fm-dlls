using fmCommon;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace fmServerCommon
{
    public static partial class RedisMapping
    {
        // items
        public static bool GetLordItems(this IDatabase db, long accid, out List<rdItem> obj)
        {
            obj = null;
            string strValue = db.StringGet(GetKeyLordItem(accid));
            if (true == string.IsNullOrEmpty(strValue))
                return false;
            obj = new JavaScriptSerializer().Deserialize<List<rdItem>>(strValue);
            return true;
        }

        public static void SetLordItems(this ITransaction trans, long accid, List<rdItem> dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            trans.StringSetAsync(GetKeyLordItem(accid), strValue);
        }

        public static bool SetLordItems(this IDatabase db, long accid, List<rdItem> dataObj)
        {
            string strValue = new JavaScriptSerializer().Serialize(dataObj);
            return db.StringSet(GetKeyLordItem(accid), strValue);
        }

        //public static bool GetPreEnchantList(this IDatabase db, long accid, out rdEnchantList list)
        //{
        //    list = null;
        //    string strValue = db.StringGet(GetKeyEnchantList(accid));

        //    if (true == string.IsNullOrEmpty(strValue))
        //    {
        //        list = new rdEnchantList();
        //        return true;
        //    }

        //    list = new JavaScriptSerializer().Deserialize<rdEnchantList>(strValue);

        //    if (null == list)
        //        list = new rdEnchantList();

        //    return true;

        //}

        //public static bool SetEnchantList(this IDatabase db, long accid, rdEnchantList list)
        //{
        //    return db.StringSet(GetKeyEnchantList(accid), new JavaScriptSerializer().Serialize(list));
        //}

        //public static void SetEnchantList(this ITransaction trans, long accid, rdEnchantList list)
        //{
        //    trans.StringSetAsync(GetKeyEnchantList(accid), new JavaScriptSerializer().Serialize(list));
        //}
    }
}
