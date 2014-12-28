using System;

namespace NotLimited.Framework.Common.Helpers
{
	public static class TimeHelpers
	{
		public static DateTime FromUnixTime(this long seconds)
		{
			var dateTime = new DateTime(1970, 1, 1);
			dateTime = dateTime.AddSeconds((double)seconds);
			return dateTime.ToLocalTime();
		}

		public static long ToUnixTime(this DateTime dateTime)
		{
			return (long)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
		} 
	}
}