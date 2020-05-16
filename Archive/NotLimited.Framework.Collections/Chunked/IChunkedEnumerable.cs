using System.Collections.Generic;

namespace NotLimited.Framework.Collections.Chunked
{
	public interface IChunkedEnumerable<T> : IEnumerable<T>
	{
		new IChunkedEnumerator<T> GetEnumerator();
	}
}