using System;
using System.Threading;

namespace NotLimited.Framework.Common.Helpers
{
	public static class Awaiter
	{
		public static bool Await(Func<bool> func, TimeSpan timeout)
		{
			var startTime = DateTime.Now;
			while (true)
			{
				if (func())
				{
					return true;
				}

				if ((DateTime.Now - startTime) >= timeout)
				{
					return false;
				}

				Thread.Sleep(50);
			}
		}
	}
}