﻿using System.Collections.Generic;

namespace NotLimited.Framework.Collections.Chunked
{
	public interface IChunkedEnumerator<T> : IChunkedEnumerable<T>, IEnumerator<T>
	{
		bool MovePrev();
		void FromFirst();
		void FromLast();
		bool FromIndex(int idx);
		IChunkedEnumerator<T> Clone();

		int Count { get; }
		bool HasMore { get; }
		int Position { get; }
		IComparableChunkedEnumerator<T> AsComparable();
	}
}