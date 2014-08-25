using System;
using System.Diagnostics;

namespace NotLimited.Framework.Common.Helpers
{
	public static class PerformanceTimer
	{
		public static TimeSpan MeasureTime(Action action)
		{
			var watch = new Stopwatch();
			watch.Start();
			action();
			watch.Stop();

			return watch.Elapsed;
		}
	}
}