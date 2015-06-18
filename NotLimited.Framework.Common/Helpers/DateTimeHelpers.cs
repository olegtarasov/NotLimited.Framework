using System;

namespace NotLimited.Framework.Common.Helpers
{
    public static class DateTimeHelpers
    {
        public static string ToShortDateTime(this DateTime time)
        {
            if (time.Date == DateTime.Now.Date)
            {
                return time.ToShortTimeString();
            }

            return time.ToShortDateString() + " " + time.ToShortTimeString();
        }
    }
}