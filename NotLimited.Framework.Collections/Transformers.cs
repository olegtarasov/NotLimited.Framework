using System;
using System.Collections.Generic;
using System.Linq;

namespace NotLimited.Framework.Collections
{
	public static class Transformers
	{
		public static Func<T, IEnumerable<T>> Filter<T>(Predicate<T> predicate)
		{
			return x => predicate(x) ? new[] { x } : Enumerable.Empty<T>();
		}

		public static Func<T, IEnumerable<E>> SingleTransform<T, E>(Func<T, E> transform)
		{
			return x => new[] {transform(x)};
		}
	}
}