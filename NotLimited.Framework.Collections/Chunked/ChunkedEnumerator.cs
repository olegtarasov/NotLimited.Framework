using System;
using System.Collections;
using System.Collections.Generic;

namespace NotLimited.Framework.Collections.Chunked
{
	internal class ChunkedEnumerator<T> : IComparableChunkedEnumerator<T>
	{
		private readonly int _maxBucketElements;

		private readonly List<Bucket<T>> _buckets;
		private int _curBucket;
		private int _curIdx;
		private T _current;

		public ChunkedEnumerator(List<Bucket<T>> buckets, int maxBucketElements)
		{
			_buckets = buckets;
			_maxBucketElements = maxBucketElements;
			_curIdx = -1;
			_curBucket = 0;
			_current = default(T);
		}

		private ChunkedEnumerator(ChunkedEnumerator<T> source)
		{
			_maxBucketElements = source._maxBucketElements;
			_buckets = source._buckets;
			_curBucket = source._curBucket;
			_curIdx = source._curIdx;
			_current = source._current;
		}

		public void FromFirst()
		{
			if (_buckets.Count == 0 || _buckets[0].IsEmpty)
				throw new InvalidOperationException("List is empty!");

			_curBucket = 0;
			_curIdx = -1;
			_current = default(T);
		}

		public void FromLast()
		{
			if (_buckets.Count == 0 || _buckets[0].IsEmpty)
				throw new InvalidOperationException("List is empty!");

			_curBucket = _buckets.Count - 1;
			_curIdx = _buckets[_curBucket].Index;
			_current = default(T);
		}

		public bool FromIndex(int idx)
		{
			if (idx < 0)
				return false;

			int start = idx - 1;
			int newBucket = start < 0 ? 0 : start / _maxBucketElements;

			if (_buckets.Count <= newBucket)
				return false;

			int newIdx = start - (_maxBucketElements * newBucket);

			if (newIdx > _buckets[newBucket].Index)
				return false;

			_curBucket = newBucket;
			_curIdx = newIdx;
			_current = newIdx < 0 ? default(T) : _buckets[_curBucket][_curIdx];

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
				if (_buckets.Count == 0)
					return 0;

				return ((_buckets.Count - 1) * _maxBucketElements) + _buckets[_buckets.Count - 1].Index;
			}
		}

		public bool HasMore
		{
			get 
			{
				if (_buckets.Count == 0 || (_curIdx >= _buckets[_curBucket].Index - 1 && _curIdx < _maxBucketElements - 1))
					return false;

				if (_curIdx >= _maxBucketElements - 1 && _curBucket >= _buckets.Count - 1)
					return false;

				return true;
			}
		}

		public int Position => (_curBucket * _maxBucketElements) + _curIdx;

		public T Current => _current;

		object IEnumerator.Current => _current;

		public void Dispose()
		{
		}

		public virtual void Reset()
		{
			_curBucket = 0;
			_curIdx = -1;
		}

		public bool IsPositionEqual(IComparableChunkedEnumerator<T> other)
		{
			var en = other as ChunkedEnumerator<T>;
			if (en == null)
			{
				return false;
			}

			return _curBucket == en._curBucket && _curIdx == en._curIdx;
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
			if (_buckets.Count == 0 || (_curIdx >= _buckets[_curBucket].Index - 1 && _curIdx < _maxBucketElements - 1))
				return false;

			if (_curIdx >= _maxBucketElements - 1)
			{
				if (_curBucket >= _buckets.Count - 1)
					return false;

				_curBucket++;
				_curIdx = -1;
			}

			_curIdx++;
			_current = _buckets[_curBucket][_curIdx];
			return true;
		}

		public bool MovePrev()
		{
			if (_curIdx == -1)
				return false;

			if (_curIdx == 0)
			{
				if (_curBucket == 0)
					return false;

				_curBucket--;
				_curIdx = _maxBucketElements;
			}

			_curIdx--;
			_current = _buckets[_curBucket][_curIdx];
			return true;
		}

		public IComparableChunkedEnumerator<T> AsComparable()
		{
			return this;
		}
	}
}