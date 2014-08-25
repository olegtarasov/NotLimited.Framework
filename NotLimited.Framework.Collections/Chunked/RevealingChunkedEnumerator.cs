namespace NotLimited.Framework.Collections.Chunked
{
	internal class RevealingChunkedEnumerator<T> : LinqChunkedEnumeratorBase<T>
	{
		private readonly RevelationToken token;

		private int revealed = 0;

		public RevealingChunkedEnumerator(IChunkedEnumerator<T> source, RevelationToken token) : base(source)
		{
			this.token = token;
		}

		private RevealingChunkedEnumerator(RevealingChunkedEnumerator<T> src) : base(src.source.Clone())
		{
			token = src.token;
			revealed = src.revealed;
		}

		public override IChunkedEnumerator<T> Clone()
		{
			return new RevealingChunkedEnumerator<T>(this);
		}

		public override bool MoveNext()
		{
			if (revealed >= token.Revealed)
				return false;

			bool result = source.MoveNext();

			if (result)
				revealed++;

			return result;
		}

		public override bool MovePrev()
		{
			bool result = source.MovePrev();

			if (result)
				revealed--;

			return result;
		}

		public override bool FromIndex(int idx)
		{
			if (idx > token.Revealed)
				return false;

			revealed = idx;

			return base.FromIndex(idx);
		}

		public override void FromLast()
		{
			revealed = token.Revealed;
			base.FromIndex(revealed + 1);
		}

		public override void FromFirst()
		{
			revealed = 0;
			base.FromFirst();
		}

		public override int Count
		{
			get { return token.Revealed; }
		}

		public override void Reset()
		{
			base.Reset();
			revealed = 0;
		}

		public override bool HasMore
		{
			get { return revealed < token.Revealed && base.HasMore; }
		}
	}
}