namespace NotLimited.Framework.Collections.Chunked
{
	public interface IComparableChunkedEnumerator<T> : IChunkedEnumerator<T>
	{
		bool IsPositionEqual(IComparableChunkedEnumerator<T> other);
	}
}