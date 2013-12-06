#pragma warning disable 642
using System;

namespace NotLimited.Framework.Collections.Chunked
{
	internal class ConditionalChunkedEnumerator<T> : LinqChunkedEnumeratorBase<T>
	{
		private readonly Predicate<T> predicate;

		public ConditionalChunkedEnumerator(IChunkedEnumerator<T> source, Predicate<T> predicate) : base(source)
		{
			this.predicate = predicate;
		}

		private ConditionalChunkedEnumerator(ConditionalChunkedEnumerator<T> src) : base(src.source.Clone())
		{
			predicate = src.predicate;
		}

		public override IChunkedEnumerator<T> Clone()
		{
			return new ConditionalChunkedEnumerator<T>(this);
		}

		public override bool MoveNext()
		{
			bool result;

			while ((result = source.MoveNext()) && !predicate(source.Current))
				;

			return result;
		}

		public override bool MovePrev()
		{
			bool result;

			while ((result = source.MovePrev()) && !predicate(source.Current))
				;

			return result;
		}

		#region Costly motherfuckers

		public override bool FromIndex(int idx)
		{
			var clone = source.Clone();

			clone.FromFirst();
			int cnt = 0;

			while (clone.MoveNext())
			{
				if (predicate(clone.Current))
				{
					cnt++;
					if (cnt == idx)
					{
						source = clone;
						return true;
					}
				}
			}

			return false;
		}

		public override int Count
		{
			get 
			{
				int cnt = 0;
				var clone = Clone();
				
				clone.FromFirst();
				while (clone.MoveNext())
					cnt++;

				return cnt;
			}
		}

		public override bool HasMore
		{
			get { return Clone().MoveNext(); }
		}

		#endregion
	}
}
#pragma warning restore 642