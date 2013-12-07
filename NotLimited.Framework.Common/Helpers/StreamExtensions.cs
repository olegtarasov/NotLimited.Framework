using System.IO;

namespace NotLimited.Framework.Common.Helpers
{
	public static class StreamExtensions
	{
		public static byte[] ReadBytes(this Stream stream, int count)
		{
			var result = new byte[count];
			int totalRead = 0;
			int read;

			while (totalRead < count)
			{
				if ((read = stream.Read(result, totalRead, count - totalRead)) == -1)
					throw new EndOfStreamException();
				totalRead += read;
			}

			return result;
		}

		public static void WriteBuff(this Stream stream, byte[] buff)
		{
			stream.Write(buff, 0, buff.Length);
		}
	}
}