using System;

namespace NotLimited.Framework.Collections.Chunked
{
	public static class ChunkedEnumerable
	{
		public static IChunkedEnumerable<T> Reverse<T>(this IChunkedEnumerable<T> source)
		{
			return new ReverseChunkedEnumerator<T>(source.GetEnumerator());
		}

		public static IChunkedEnumerable<E> Transform<T, E>(this IChunkedEnumerable<T> source, Func<T, E> transform)
		{
			return new TransformingChunkedEnumerator<T, E>(source.GetEnumerator(), transform);
		}

		public static IChunkedEnumerable<T> FromIndex<T>(this IChunkedEnumerable<T> source, int idx)
		{
			return new OffsetChunkedEnumerator<T>(source.GetEnumerator(), idx);
		}

		public static IChunkedEnumerable<T> When<T>(this IChunkedEnumerable<T> source, Predicate<T> predicate)
		{
			return new ConditionalChunkedEnumerator<T>(source.GetEnumerator(), predicate);
		}

		public static IChunkedEnumerable<T> Revealing<T>(this IChunkedEnumerable<T> source, RevelationToken token)
		{
			return new RevealingChunkedEnumerator<T>(source.GetEnumerator(), token);
		}

		public static T ElementAt<T>(this IChunkedEnumerable<T> source, int idx)
		{
			var en = source.GetEnumerator();
			if (!en.FromIndex(idx + 1))
				throw new IndexOutOfRangeException();
			return en.Current;
		}

		public static T ElementAtOrDefault<T>(this IChunkedEnumerable<T> source, int idx)
		{
			var en = source.GetEnumerator();
			return en.FromIndex(idx + 1) ? en.Current : default(T);
		}

		public static T First<T>(this IChunkedEnumerable<T> source)
		{
			var en = source.GetEnumerator();
			if (!en.MoveNext())
				throw new InvalidOperationException("Sequence contains no elements!");
			return en.Current;
		}

		public static T Last<T>(this IChunkedEnumerable<T> source)
		{
			var en = source.GetEnumerator();
			en.FromLast();
			if (!en.MovePrev())
				throw new InvalidOperationException("Sequence contains no elements!");
			return en.Current;
		}

		public static int Count<T>(this IChunkedEnumerable<T> source)
		{
			var list = source as ChunkedList<T>;

			if (list != null)
				return list.Count;

			return source.GetEnumerator().Count;
		}

		public static IChunkedEnumerable<T> PrependWith<T>(this IChunkedEnumerable<T> source, IChunkedEnumerable<T> collection)
		{
			return new PrependingChunkedEnumerator<T>(source.GetEnumerator(), collection);
		}

		public static IChunkedEnumerable<T> Take<T>(this IChunkedEnumerable<T> source, int count)
		{
			return new LimitingChunkedEnumerator<T>(source.GetEnumerator(), count);
		}

		public static IChunkedEnumerator<T> Repeat<T>(T item, int count)
		{
			return new RepeatingChunkedEnumerator<T>(item, count);
		}
	}
}