using System;

namespace NotLimited.Framework.Common.Helpers
{
	public static class RandomExtensions
	{
		public static double NextDouble(this Random rnd, double high)
		{
			return rnd.NextDouble() * high;
		}

		public static double NextDouble(this Random rnd, double low, double high)
		{
			return rnd.NextDouble() * (high - low) + low;
		}
	}
}