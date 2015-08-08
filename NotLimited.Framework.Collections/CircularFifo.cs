using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NotLimited.Framework.Collections
{
	public class CircularFifo<T> : IList<T>
	{
		private readonly T[] buffer;

		private int curIdx = 0;

		public CircularFifo(int size)
		{
			buffer = new T[size];
		}

		public CircularFifo(int size, T initialValue) : this(size)
		{
			Initialize(initialValue);
		}

		public void Initialize(T initialValue = default(T))
		{
			for (int i = 0; i < buffer.Length; i++)
				buffer[i] = initialValue;
		}

		public int IndexOf(T item)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				if (object.Equals(buffer[i], item))
					return i;
			}

			return -1;
		}

		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public T this[int idx]
		{
			get
			{
				if (idx >= buffer.Length)
					throw new ArgumentOutOfRangeException("idx");

				return (idx >= curIdx) ? buffer[buffer.Length - (idx - curIdx + 1)] : buffer[curIdx - idx - 1];
			}
			set
			{
				if (idx >= buffer.Length)
					throw new ArgumentOutOfRangeException("idx");

				buffer[(idx >= curIdx) ? buffer.Length - (idx - curIdx + 1) : curIdx - idx - 1] = value;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new CircularFifoEnumerator<T>(buffer, curIdx);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			buffer[curIdx] = item;
			if (curIdx == buffer.Length - 1)
				curIdx = 0;
			else
				curIdx++;
		}

		public void Clear(T initialValue)
		{
			curIdx = 0;
			Initialize(initialValue);
		}

		public void Clear()
		{
			Clear(default(T));
		}

		public bool Contains(T item)
		{
			for (int i = 0; i < buffer.Length; i++)
				if (buffer[i].Equals(item))
					return true;

			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			int cnt = 0;

			foreach (var item in this.Skip(0))
			{
				array[cnt] = item;
				cnt++;
			}
		}

		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		public int Count
		{
			get { return buffer.Length; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}
	}
}