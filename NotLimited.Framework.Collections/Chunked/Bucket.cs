using System;

namespace NotLimited.Framework.Collections.Chunked
{
	public class Bucket<T>
	{
		private readonly int maxElements;
		private readonly T[] array;

		private int curIdx = 0;

		public int Index { get { return curIdx; } }
		public bool IsEmpty { get { return curIdx == 0; } }
		public T LastItem { get { return array[curIdx - 1]; } }
		public int FreeSpace { get { return maxElements - curIdx; } }
		public T[] Buffer { get { return array; } }

		public T this[int idx] { get { return array[idx]; } set { array[idx] = value; } }

		public Bucket(int maxElements)
		{
			this.maxElements = maxElements;	
			array = new T[maxElements];
		}

		public Bucket(T[] buff, int index)
		{
			array = buff;
			curIdx = index;
		}

		public void Add(T item)
		{
			if (curIdx == maxElements)
				throw new InvalidOperationException("The bucket is full!");

			array[curIdx] = item;
			curIdx++;
		}

		public void AddRange(T[] items)
		{
			if ((maxElements - curIdx) < items.Length)
				throw new InvalidOperationException("Not enough space in this bucket!");

			for (int i = 0; i < items.Length; i++, curIdx++)
				array[curIdx] = items[i];
		}
	}
}