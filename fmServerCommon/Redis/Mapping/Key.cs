
using fmCommon;
using System;

namespace fmServerCommon
{
    public static partial class RedisMapping
    {
        // Token
        private static string GetKeyAuthToken(string token) { return string.Format("Auth_{0}", token); }
        private static string GetKeyOwnerToken(long accid) { return string.Format("Owner_{0}", accid); }

        // Game
        private static string GetKeyLordName(string name) { return string.Format("tbl_Name_{0}", name); }
        private static string GetKeyLordInfo(long accid) { return string.Format("tbl_LordInfo_{0}", accid); }
        private static string GetKeyLordRuby(long accid) { return string.Format("tbl_LordRuby_{0}", accid); }
        private static string GetKeyLordStat(long accid) { return string.Format("tbl_LordStat_{0}", accid); }
        private static string GetKeyLordItem(long accid) { return string.Format("tbl_LordItem_{0}", accid); }
        private static string GetKeyLordMap(long accid) { return string.Format("tbl_LordMap_{0}", accid); }
        private static string GetKeyLordInDun(long accid) { return string.Format("tbl_LordInDun_{0}", accid); }

        private static string GetKeyLordMissionBase(long accid) { return string.Format("tbl_LordMissionBase_{0}", accid); }
        private static string GetKeyLordMission(long accid) { return string.Format("tbl_LordMission_{0}", accid); }
        private static string GetKeyMazeRank() { return string.Format("Maze_Floor"); }

        private static string GetKeyEnchantList(long accid) { return string.Format("tbl_EnchantList_{0}", accid); }

        // Log
        private static string GetKeyRubyLog(long accid) { return string.Format("Log_{0}", accid); }
        private static string GetKeyIABLog(DateTime time) { return string.Format("Log_IAB_{0}", time.ToString("d")); }
    }
}
