using fmCommon;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace fmServerCommon
{
    public static partial class RedisMapping
    {
        // 토큰
        public static void UpdateAuthToken(this IDatabase db, string token, long accid, int gameServer)
        {
            using (var authToken = new fmAuthToken { AccID = accid, GameSvr = gameServer, Token = token })
            {
                // 1. new authToken
                string strValue = new JavaScriptSerializer().Serialize(authToken);
                db.StringSet(GetKeyAuthToken(token), strValue);

                // 2. accid로 이전 authToken 찾기
                var oldToken = db.StringGet(GetKeyOwnerToken(accid));
                if (false == string.IsNullOrEmpty(oldToken))
                {
                    // 3. 이전 거 삭제
                    db.KeyDelete(GetKeyAuthToken(oldToken));
                }

                // 3. accid 토큰 설정
                db.StringSet(GetKeyOwnerToken(accid), token);
            }
        }

        public static bool GetAuthToken(this IDatabase db, string token, out long accid)
        {
            accid = 0;

            string strValue = db.StringGet(GetKeyAuthToken(token));

            if (true == string.IsNullOrEmpty(strValue))
                return false;

            var authToken = new JavaScriptSerializer().Deserialize<fmAuthToken>(strValue);
            if (null == authToken)
                return false;

            using (authToken)
            {
                accid = authToken.AccID;
            }

            return true;
        }

        public static bool GetGameServer(this IDatabase db, long accid, out int gameSvr)
        {
            gameSvr = 0;

            string strToken = db.StringGet(GetKeyOwnerToken(accid));
            string strValue = db.StringGet(GetKeyAuthToken(strToken));

            if (true == string.IsNullOrEmpty(strValue))
                return false;

            var authToken = new JavaScriptSerializer().Deserialize<fmAuthToken>(strValue);
            if (null == authToken)
                return false;

            using (authToken)
            {
                gameSvr = authToken.GameSvr;
            }

            return true;
        }
    }
}
