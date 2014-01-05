using System;
using System.Collections.Generic;
using System.Linq;

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

		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
		{
			return new HashSet<T>(source);
		}

		public static HashSet<T> ConcatHashSet<T>(this IEnumerable<T> source, params T[] set)
		{
			var result = new HashSet<T>(source);
			foreach (var item in set)
				result.Add(item);

			return result;
		}

		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			return source ?? Enumerable.Empty<T>();
		}

		public static bool IsNullOrEmpty<T>(this ICollection<T> source)
		{
			return source == null || source.Count == 0;
		}
	}
}