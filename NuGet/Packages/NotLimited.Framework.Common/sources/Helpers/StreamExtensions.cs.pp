//////////////////////////////////////////////////////////////////////////
// This file is a part of NotLimited.Framework.Common NuGet package.
// You are strongly discouraged from fiddling with it.
// If you do, all hell will break loose and living will envy the dead.
//////////////////////////////////////////////////////////////////////////
using System.IO;

namespace $rootnamespace$.Helpers
{
	internal static class StreamExtensions
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