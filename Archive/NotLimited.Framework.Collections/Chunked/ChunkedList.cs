﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;

namespace NotLimited.Framework.Collections.Chunked
{
	/// <summary>
	/// List with fixed-size buckets. Sequential enumerator access should 
	/// be much faster than for-loop with indexer
	/// </summary>
	public class ChunkedList<T> : ICollection<T>, IChunkedEnumerable<T>
	{
		protected const int DefaultBucketSize = 84000;

		protected readonly List<Bucket<T>> buckets = new List<Bucket<T>>();

		public int MaxBucketElements { get; private set; }
		public IList<Bucket<T>> Buckets { get { return buckets.AsReadOnly(); } }

		public ChunkedList() : this(DefaultBucketSize)
		{
		}

		public ChunkedList(int bucketSize)
		{
			MaxBucketElements = bucketSize / (typeof(T).IsValueType ? Marshal.SizeOf(typeof(T)) : IntPtr.Size);
		}

		public IChunkedEnumerator<T> GetEnumerator()
		{
			return new ChunkedEnumerator<T>(buckets, MaxBucketElements);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			if (buckets.Count == 0 || buckets[buckets.Count - 1].FreeSpace == 0)
				buckets.Add(new Bucket<T>(MaxBucketElements));

			buckets[buckets.Count - 1].Add(item);

			if (ListUpdated != null)
				ListUpdated(this, new ListUpdatedArgs(1));
		}

		public void Clear()
		{
			buckets.Clear();
		}

		public bool Contains(T item)
		{
			return Enumerable.Contains(this, item, null);
		}

		/// <exception cref="ArgumentNullException"><paramref name="array"/> is <see langword="null" />.</exception>
		/// <exception cref="ArgumentOutOfRangeException">arrayIndex is &lt; 0 or target array is too small</exception>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null) throw new ArgumentNullException("array");
			if (arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex");
			if (array.Length - arrayIndex < Count) throw new ArgumentOutOfRangeException("array");
			
			int i = arrayIndex;
			foreach (T item in this)
			{
				array[i] = item;
				i++;
			}
		}

		public bool Remove(T item)
		{
			throw new NotSupportedException("Sorry dude, no removals from this one!");
		}

		public int Count
		{
			get
			{
				return buckets.Count == 0 ? 0 : (MaxBucketElements * (buckets.Count - 1)) + buckets[buckets.Count - 1].Index;
			}
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public int BucketCount
		{
			get { return buckets.Count; }
		}

		public bool IsEmpty
		{
			get { return (buckets.Count == 0 || buckets[0].IsEmpty); }
		}

		public T First
		{
			get
			{
				return IsEmpty ? default(T) : buckets[0][0];
			}
		}

		public T Last
		{
			get
			{
				return IsEmpty ? default(T) : buckets[buckets.Count - 1].LastItem;
			}
		}

		public T this[int idx]
		{
			get
			{
				int bIdx = idx / MaxBucketElements;

				return buckets[bIdx][idx - (MaxBucketElements * bIdx)];
			}
			set
			{
				int bIdx = idx / MaxBucketElements;

				buckets[bIdx][idx - (MaxBucketElements * bIdx)] = value;
			}
		}

		public void AddRange(T[] items)
		{
			if (buckets.Count == 0)
				buckets.Add(new Bucket<T>(MaxBucketElements));

			var curBucket = buckets[buckets.Count - 1];
			int cur = 0, cnt, delta;

			while (cur < items.Length)
			{
				delta = items.Length - cur;
				cnt = delta >= curBucket.FreeSpace ? curBucket.FreeSpace : delta;

				for (int i = cur; i < cnt + cur; i++)
					curBucket.Add(items[i]);

				cur += cnt;

				if (curBucket.FreeSpace == 0 && cur < items.Length)
					buckets.Add(curBucket = new Bucket<T>(MaxBucketElements));
			}

			if (ListUpdated != null)
				ListUpdated(this, new ListUpdatedArgs(items.Length));
		}

		public event EventHandler<ListUpdatedArgs> ListUpdated;
	}

	public class ListUpdatedArgs : EventArgs
	{
		public ListUpdatedArgs(int count)
		{
			Count = count;
		}

		public int Count { get; set; }
	}
}