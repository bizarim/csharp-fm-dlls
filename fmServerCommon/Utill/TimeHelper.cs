using System;

namespace fmServerCommon
{
    public class fmServerTime
    {
        public static DateTime Epoch { get { return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); } }
        public static DateTime Now { get { return DateTime.UtcNow; } }
        public static DateTime Date { get { return DateTime.UtcNow.Date; } }
        public static DayOfWeek DayOfWeek { get { return DateTime.UtcNow.DayOfWeek; } }

        public static DateTime LimitBroadcast { get { return DateTime.UtcNow.AddMinutes(-15); } }
        public static DateTime LimitSleep { get { return DateTime.UtcNow.AddMinutes(-600); } }
    }
}
