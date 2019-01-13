using StackExchange.Redis;

namespace fmServerCommon
{
    public static partial class RedisMapping
    {
        public static bool IsExistsName(this IDatabase db, string name)
        {
            return db.KeyExists(GetKeyLordName(name));
        }

        public static void InsertName(this ITransaction trans, string name, long accid)
        {
            trans.AddCondition(Condition.KeyNotExists(GetKeyLordName(name)));
            trans.StringSetAsync(GetKeyLordName(name), accid);
        }

        public static long GetAccIdWithName(this IDatabase db, string name)
        {
            string strValue = db.StringGet(GetKeyLordName(name));
            if (true == string.IsNullOrEmpty(strValue))
                return 0;

            return long.Parse(strValue);
        }

    }
}
