using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NotLimited.Framework.Collections
{
	public sealed class TransformingEnumerator<T, E> : IEnumerator<E>
	{
		private readonly IEnumerator<T> orig;
		private readonly Func<T, IEnumerable<E>> transformer;

		private IEnumerator<E> cur;

		public TransformingEnumerator(IEnumerator<T> orig, Func<T, IEnumerable<E>> transformer)
		{
			this.orig = orig;
			this.transformer = transformer;
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			while (cur == null || !cur.MoveNext())
			{
				IEnumerable<E> en = null;

				while (en == null || en == Enumerable.Empty<E>())
				{
					if (!orig.MoveNext())
						return false;
					en = transformer(orig.Current);
				}

				cur = en.GetEnumerator();
			}

			return true;
		}

		public void Reset()
		{
			cur = null;
			orig.Reset();
		}

		public E Current
		{
			get { return cur.Current; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}
	}
}