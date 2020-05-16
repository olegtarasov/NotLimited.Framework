//using System.Collections;
//using System.Collections.Generic;

//namespace NotLimited.Framework.Collections.Chunked
//{
//	internal abstract class LinqChunkedEnumeratorBase<T> : IChunkedEnumerator<T>
//	{
//		protected IChunkedEnumerator<T> source;

//		protected LinqChunkedEnumeratorBase(IChunkedEnumerator<T> source)
//		{
//			this.source = source;
//		}

//		public virtual T Current
//		{
//			get { return source.Current; }
//		}

//		object IEnumerator.Current
//		{
//			get { return Current; }
//		}

//		public virtual bool FromIndex(int idx)
//		{
//			if (idx < 0)
//				return false;

//			return source.FromIndex(idx);
//		}

//		public abstract IChunkedEnumerator<T> Clone();

//		public virtual int Count
//		{
//			get { return source.Count; }
//		}

//		public virtual bool HasMore
//		{
//			get { return source.HasMore; }
//		}

//		public void Dispose()
//		{
//		}

//		public virtual bool MoveNext()
//		{
//			return source.MoveNext();
//		}

//		public virtual void Reset()
//		{
//			source.Reset();
//		}

//		public virtual bool MovePrev()
//		{
//			return source.MovePrev();
//		}

//		public virtual void FromFirst()
//		{
//			source.FromFirst();
//		}

//		public virtual void FromLast()
//		{
//			source.FromLast();
//		}

//		IChunkedEnumerator<T> IChunkedEnumerable<T>.GetEnumerator()
//		{
//			return (IChunkedEnumerator<T>)GetEnumerator();
//		}

//		public virtual IEnumerator<T> GetEnumerator()
//		{
//			return this;
//		}

//		IEnumerator IEnumerable.GetEnumerator()
//		{
//			return GetEnumerator();
//		}
//	}
//}