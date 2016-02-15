using System;

namespace NotLimited.Framework.Collections.Chunked
{
	public class Bucket<T>
	{
		private readonly int _maxElements;
		private readonly T[] _array;

		private int _curIdx = 0;

		public int Index => _curIdx;
		public bool IsEmpty => _curIdx == 0;
		public T LastItem => _array[_curIdx - 1];
		public int FreeSpace => _maxElements - _curIdx;
		public T[] Buffer => _array;

		public T this[int idx]
		{
			get { return _array[idx]; }
			set
			{
				if (idx > _curIdx - 1)
				{
					throw new IndexOutOfRangeException("Can't set elements past current index!");
				}

				_array[idx] = value;
			}
		}

		public Bucket(int maxElements)
		{
			this._maxElements = maxElements;	
			_array = new T[maxElements];
		}

		public void Add(T item)
		{
			if (_curIdx == _maxElements)
				throw new InvalidOperationException("The bucket is full!");

			_array[_curIdx] = item;
			_curIdx++;
		}

		public void AddRange(T[] items)
		{
			if ((_maxElements - _curIdx) < items.Length)
				throw new InvalidOperationException("Not enough space in this bucket!");

			for (int i = 0; i < items.Length; i++, _curIdx++)
				_array[_curIdx] = items[i];
		}
	}
}