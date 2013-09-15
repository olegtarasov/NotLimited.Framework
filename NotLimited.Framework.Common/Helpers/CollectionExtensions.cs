using System;
using System.Collections.Generic;

namespace NotLimited.Framework.Common.Helpers
{
	public static class CollectionExtensions
	{
		 public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
		 {
			 foreach (var item in source)
				collection.Add(item);
		 }

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var item in source)
				action(item);
		}
	}
}