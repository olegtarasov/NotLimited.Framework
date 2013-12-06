using System.Collections;
using System.Collections.Generic;

namespace NotLimited.Framework.Collections.Chunked
{
	internal class RepeatingChunkedEnumerator<T> : IChunkedEnumerator<T>
	{
		private readonly T item;
		private readonly int count;

		private int cur;

		public RepeatingChunkedEnumerator(T item, int count)
		{
			this.item = item;
			this.count = count;
		}

		private RepeatingChunkedEnumerator(RepeatingChunkedEnumerator<T> src)
		{
			item = src.item;
			count = src.count;
			cur = src.cur;
		}

		public IChunkedEnumerator<T> GetEnumerator()
		{
			return this;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			if (cur >= count)
				return false;

			cur++;

			return true;
		}

		public void Reset()
		{
			cur = 0;
		}

		public T Current
		{
			get { return item; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}

		public bool MovePrev()
		{
			if (cur <= 0)
				return false;

			cur--;

			return true;
		}

		public void FromFirst()
		{
			cur = 0;
		}

		public void FromLast()
		{
			cur = count;
		}

		public bool FromIndex(int idx)
		{
			if (idx < 0)
				return false;

			if (idx > count)
				return false;

			cur = idx;

			return true;
		}

		public IChunkedEnumerator<T> Clone()
		{
			return new RepeatingChunkedEnumerator<T>(this);
		}

		public int Count
		{
			get { return count; }
		}

		public bool HasMore
		{
			get { return cur < count; }
		}
	}
}