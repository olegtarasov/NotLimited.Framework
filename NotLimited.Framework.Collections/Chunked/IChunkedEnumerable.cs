using System.Collections.Generic;

namespace NotLimited.Framework.Collections.Chunked
{
	public interface IChunkedEnumerable<out T> : IEnumerable<T>
	{
		new IChunkedEnumerator<T> GetEnumerator();
	}
}