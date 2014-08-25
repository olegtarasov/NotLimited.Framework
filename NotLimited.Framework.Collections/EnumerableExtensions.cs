using System;
using System.Collections.Generic;
using System.Linq;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Collections
{
	public static class EnumerableExtensions
	{
		public static MinMax<T> MinMax<T>(this IEnumerable<T> en, Func<T, double> selector)
		{
			var result = new MinMax<T>();
			double curItem, curMin, curMax;

			result.Max = result.Min = en.First();
			curMax = curMin = selector(result.Min);

			foreach (var item in en.Skip(1))
			{
				curItem = selector(item);

				if (curItem > curMax)
				{
					result.Max = item;
					curMax = curItem;
				}

				if (curItem < curMin)
				{
					result.Min = item;
					curMin = curItem;
				}
			}

			return result;
		}
	}
}