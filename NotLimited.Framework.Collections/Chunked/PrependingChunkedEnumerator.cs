namespace NotLimited.Framework.Collections.Chunked
{
	internal class PrependingChunkedEnumerator<T> : LinqChunkedEnumeratorBase<T>
	{
		private readonly IChunkedEnumerable<T> collection;

		private IChunkedEnumerator<T> enumerator;
		private bool additionFinished;

		public PrependingChunkedEnumerator(IChunkedEnumerator<T> source, IChunkedEnumerable<T> collection) : base(source)
		{
			this.collection = collection;
			enumerator = collection.GetEnumerator();
		}

		private PrependingChunkedEnumerator(PrependingChunkedEnumerator<T> src) : base(src.source.Clone())
		{
			additionFinished = src.additionFinished;
			collection = src.collection;
			enumerator = src.enumerator.Clone();
		}

		public override bool MoveNext()
		{
			if (additionFinished)
				return base.MoveNext();

			if (!enumerator.MoveNext())
			{
				additionFinished = true;
				return base.MoveNext();
			}

			return true;
		}

		public override bool MovePrev()
		{
			if (base.MovePrev())
				return true;

			if (additionFinished)
				additionFinished = false;

			return enumerator.MovePrev();
		}

		public override void FromFirst()
		{
			base.FromFirst();
			additionFinished = false;
			enumerator.FromFirst();
		}

		public override bool FromIndex(int idx)
		{
			int cnt = enumerator.Count;
			
			additionFinished = idx > cnt;
			if (additionFinished)
			{
				enumerator.FromLast();
				return base.FromIndex(idx - cnt);
			}

			base.FromFirst();
			return enumerator.FromIndex(idx);
		}

		public override void FromLast()
		{
			enumerator.FromLast();
			base.FromLast();
			additionFinished = true;
		}

		public override int Count
		{
			get { return enumerator.Count + base.Count; }
		}

		public override T Current
		{
			get { return additionFinished ? base.Current : enumerator.Current; }
		}

		public override bool HasMore
		{
			get { return enumerator.HasMore || base.HasMore; }
		}

		public override void Reset()
		{
			base.Reset();
			enumerator = collection.GetEnumerator();
			additionFinished = false;
		}

		public override IChunkedEnumerator<T> Clone()
		{
			return new PrependingChunkedEnumerator<T>(this);
		}
	}
}