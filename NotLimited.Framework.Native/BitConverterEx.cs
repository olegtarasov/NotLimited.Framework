using System;

namespace NotLimited.Framework.Native
{
	public static class BitConverterEx
	{
		public static unsafe void WriteBytes(this int value, byte[] buff, int index)
		{
			if (buff.Length < index + 4)
				throw new InvalidOperationException("Buffer is too small!");

			fixed (byte* ptr = &buff[index])
				*((int*)ptr) = value;
		}

		public static unsafe int ReadInt32(this byte[] buff, int index)
		{
			if (buff.Length < index + 4)
				throw new InvalidOperationException("Buffer is too small!");

			int result;

			fixed (byte* ptr = &buff[index])
				result = *((int*)ptr);

			return result;
		}

		public static unsafe void WriteBytes(this long value, byte[] buff, int index)
		{
			if (buff.Length < index + 8)
				throw new InvalidOperationException("Buffer is too small!");

			fixed (byte* ptr = &buff[index])
				*((long*)ptr) = value;
		}

		public static unsafe long ReadInt64(this byte[] buff, int index)
		{
			if (buff.Length < index + 8)
				throw new InvalidOperationException("Buffer is too small!");

			long result;

			fixed (byte* ptr = &buff[index])
				result = *((long*)ptr);

			return result;
		}
	}
}
