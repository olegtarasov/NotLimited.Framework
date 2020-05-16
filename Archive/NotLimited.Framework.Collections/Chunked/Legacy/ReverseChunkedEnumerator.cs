//namespace NotLimited.Framework.Collections.Chunked
//{
//	internal class ReverseChunkedEnumerator<T> : LinqChunkedEnumeratorBase<T>
//	{
//		public ReverseChunkedEnumerator(IChunkedEnumerator<T> source) : base(source)
//		{
//			source.FromLast();
//		}

//		private ReverseChunkedEnumerator(ReverseChunkedEnumerator<T> src) : base(src.source.Clone())
//		{
//		}

//		public override bool MoveNext()
//		{
//			return source.MovePrev();
//		}

//		public override void Reset()
//		{
//			base.Reset();
//			source.FromLast();
//		}

//		public override bool MovePrev()
//		{
//			return source.MoveNext();
//		}

//		public override void FromFirst()
//		{
//			source.FromLast();
//		}

//		public override void FromLast()
//		{
//			source.FromFirst();
//		}

//		public override bool FromIndex(int idx)
//		{
//			return source.FromIndex(source.Count - idx + 1);
//		}

//		public override IChunkedEnumerator<T> Clone()
//		{
//			return new ReverseChunkedEnumerator<T>(this);
//		}

//		public override bool HasMore
//		{
//			get { return Clone().MoveNext(); }
//		}
//	}
//}