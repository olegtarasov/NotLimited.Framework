//using System;
//using System.Collections.Generic;

//namespace NotLimited.Framework.Collections.Chunked
//{
//	internal class TransformingChunkedEnumerator<T, E> : LinqChunkedEnumeratorBase<T>, IChunkedEnumerator<E>, IChunkedEnumerable<E>
//	{
//		private readonly Func<T, E> transform;

//		public TransformingChunkedEnumerator(IChunkedEnumerator<T> source, Func<T, E> transform) : base(source)
//		{
//			this.transform = transform;
//		}

//		private TransformingChunkedEnumerator(TransformingChunkedEnumerator<T, E> src) : base(src.source.Clone())
//		{
//			transform = src.transform;
//		}

//		public new E Current
//		{
//			get { return transform(source.Current); }
//		}

//		public override IChunkedEnumerator<T> Clone()
//		{
//			throw new NotSupportedException();
//		}

//		IChunkedEnumerator<E> IChunkedEnumerator<E>.Clone()
//		{
//			return new TransformingChunkedEnumerator<T, E>(this);
//		}

//		IChunkedEnumerator<E> IChunkedEnumerable<E>.GetEnumerator()
//		{
//			return (IChunkedEnumerator<E>)GetEnumerator();
//		}

//		public new IEnumerator<E> GetEnumerator()
//		{
//			return this;
//		}
//	}
//}