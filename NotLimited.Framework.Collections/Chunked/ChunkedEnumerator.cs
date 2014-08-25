using System;
using System.Collections;
using System.Collections.Generic;

namespace NotLimited.Framework.Collections.Chunked
{
	internal class ChunkedEnumerator<T> : IChunkedEnumerator<T>
	{
		protected readonly int maxBucketElements;

		protected List<Bucket<T>> list;
		protected int curBucket;
		protected int curIdx;
		protected T current;

		public ChunkedEnumerator(List<Bucket<T>> list, int maxBucketElements)
		{
			this.list = list;
			this.maxBucketElements = maxBucketElements;
			curIdx = -1;
			curBucket = 0;
			current = default(T);
		}

		private ChunkedEnumerator(ChunkedEnumerator<T> source)
		{
			maxBucketElements = source.maxBucketElements;
			list = source.list;
			curBucket = source.curBucket;
			curIdx = source.curIdx;
			current = source.current;
		}

		public void FromFirst()
		{
			if (list.Count == 0 || list[0].IsEmpty)
				throw new InvalidOperationException("List is empty!");

			curBucket = 0;
			curIdx = -1;
			current = default(T);
		}

		public void FromLast()
		{
			if (list.Count == 0 || list[0].IsEmpty)
				throw new InvalidOperationException("List is empty!");

			curBucket = list.Count - 1;
			curIdx = list[curBucket].Index;
			current = default(T);
		}

		public bool FromIndex(int idx)
		{
			if (idx < 0)
				return false;

			int start = idx - 1;
			int newBucket = start < 0 ? 0 : start / maxBucketElements;

			if (list.Count <= newBucket)
				return false;

			int newIdx = start - (maxBucketElements * newBucket);

			if (newIdx > list[newBucket].Index)
				return false;

			curBucket = newBucket;
			curIdx = newIdx;
			current = newIdx < 0 ? default(T) : list[curBucket][curIdx];

			return true;
		}

		public IChunkedEnumerator<T> Clone()
		{
			return new ChunkedEnumerator<T>(this);
		}

		public int Count
		{
			get
			{
				if (list.Count == 0)
					return 0;

				return ((list.Count - 1) * maxBucketElements) + list[list.Count - 1].Index;
			}
		}

		public bool HasMore
		{
			get 
			{
				if (list.Count == 0 || (curIdx >= list[curBucket].Index - 1 && curIdx < maxBucketElements - 1))
					return false;

				if (curIdx >= maxBucketElements - 1 && curBucket >= list.Count - 1)
					return false;

				return true;
			}
		}

		public T Current
		{
			get { return current; }
		}

		object IEnumerator.Current
		{
			get { return current; }
		}

		public void Dispose()
		{
		}

		public virtual void Reset()
		{
			curBucket = 0;
			curIdx = -1;
		}

		IChunkedEnumerator<T> IChunkedEnumerable<T>.GetEnumerator()
		{
			return (IChunkedEnumerator<T>)GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool MoveNext()
		{
			if (list.Count == 0 || (curIdx >= list[curBucket].Index - 1 && curIdx < maxBucketElements - 1))
				return false;

			if (curIdx >= maxBucketElements - 1)
			{
				if (curBucket >= list.Count - 1)
					return false;

				curBucket++;
				curIdx = -1;
			}

			curIdx++;
			current = list[curBucket][curIdx];
			return true;
		}

		public bool MovePrev()
		{
			if (curIdx == -1)
				return false;

			if (curIdx == 0)
			{
				if (curBucket == 0)
					return false;

				curBucket--;
				curIdx = maxBucketElements;
			}

			curIdx--;
			current = list[curBucket][curIdx];
			return true;
		}
	}
}