//namespace NotLimited.Framework.Collections.Chunked
//{
//	internal class LimitingChunkedEnumerator<T> : LinqChunkedEnumeratorBase<T>
//	{
//		private readonly int limit;
//		private int cnt = 0;

//		public LimitingChunkedEnumerator(IChunkedEnumerator<T> source, int limit) : base(source)
//		{
//			this.limit = limit;
//		}

//		private LimitingChunkedEnumerator(LimitingChunkedEnumerator<T> src) : base(src.source.Clone())
//		{
//			limit = src.limit;
//		}

//		public override int Count
//		{
//			get { return limit; }
//		}

//		public override IChunkedEnumerator<T> Clone()
//		{
//			return new LimitingChunkedEnumerator<T>(this);
//		}

//		public override bool HasMore
//		{
//			get { return base.HasMore && cnt < limit; }
//		}

//		public override bool MoveNext()
//		{
//			cnt++;

//			if (cnt > limit)
//				return false;

//			return base.MoveNext();
//		}

//		public override bool MovePrev()
//		{
//			if (cnt == 0)
//				return false;
//			cnt--;
//			return base.MovePrev();
//		}

//		public override void Reset()
//		{
//			base.Reset();
//			cnt = 0;
//		}

//		public override bool FromIndex(int idx)
//		{
//			if (idx > limit)
//				return false;

//			cnt = idx;

//			return base.FromIndex(idx);
//		}

//		public override void FromLast()
//		{
//			base.FromIndex(limit + 1);
//			cnt = limit;
//		}

//		public override void FromFirst()
//		{
//			base.FromFirst();
//			cnt = 0;
//		}
//	}
//}