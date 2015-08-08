using System;
using System.Collections;
using System.Collections.Generic;

namespace NotLimited.Framework.Collections
{
	public class CircularFifoEnumerator<T> : IEnumerator<T>
	{
		private readonly T[] buff;
		private readonly int startIdx;
		
		private int idx, cnt = 0;
		
		public CircularFifoEnumerator(T[] buff, int startIdx)
		{
			this.buff = buff;
			this.startIdx = idx = startIdx;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public bool MoveNext()
		{
			if (cnt >= buff.Length)
				return false;
			
			cnt++;
			idx--;

			if (idx < 0)
				idx = buff.Length - 1;

			return true;
		}

		public void Reset()
		{
			cnt = 0;
			idx = startIdx;
		}

		public T Current
		{
			get { return buff[idx]; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}
	}
}