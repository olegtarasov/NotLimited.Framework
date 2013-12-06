namespace NotLimited.Framework.Collections.Chunked
{
	internal class OffsetChunkedEnumerator<T> : LinqChunkedEnumeratorBase<T>
	{
		private readonly int start;
		
		private int count = 0;

		public OffsetChunkedEnumerator(IChunkedEnumerator<T> source, int idx) : base(source)
		{
			start = idx;
			source.FromIndex(start);
		}

		private OffsetChunkedEnumerator(OffsetChunkedEnumerator<T> src) : base(src.source.Clone())
		{
			start = src.start;
			count = src.count;
		}

		public override IChunkedEnumerator<T> Clone()
		{
			return new OffsetChunkedEnumerator<T>(this);
		}

		public override bool FromIndex(int idx)
		{
			if (idx < 0)
				return false;

			bool res = source.FromIndex(start + idx);

			if (res)
				count = idx;

			return res;
		}

		public override int Count
		{
			get { return source.Count - start; }
		}

		public override void Reset()
		{
			source.Reset();
			source.FromIndex(start);
			count = 0;
		}

		public override bool MoveNext()
		{
			bool res = source.MoveNext();

			if (res)
				count++;

			return res;
		}

		public override bool MovePrev()
		{
			if (count <= 0)
				return false;

			count--;
			return source.MovePrev();
		}

		public override void FromFirst()
		{
			source.FromIndex(start);
			count = 0;
		}

		public override void FromLast()
		{
			source.FromLast();
			count = source.Count - start;
		}
	}
}