namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Bit converter extensions to read and write values using unsafe pointers.
/// </summary>
public static class BitConverterExtensions
{
	/// <summary>
	/// Writes an <see cref="Int32"/> value to specified index in an array using unsafe pointers.
	/// </summary>
	/// <param name="value">Integer value.</param>
	/// <param name="buff">Byte buffer to write to.</param>
	/// <param name="index">Index to write at.</param>
	public static unsafe void WriteBytes(this int value, byte[] buff, int index)
	{
		if (buff.Length < index + 4)
			throw new InvalidOperationException("Buffer is too small!");

		fixed (byte* ptr = &buff[index])
			*((int*)ptr) = value;
	}

	/// <summary>
	/// Reads an <see cref="Int32"/> value from specified index in an array using unsafe pointers.
	/// </summary>
	/// <param name="buff">Byte buffer to read from.</param>
	/// <param name="index">Index to read at.</param>
	public static unsafe int ReadInt32(this byte[] buff, int index)
	{
		if (buff.Length < index + 4)
			throw new InvalidOperationException("Buffer is too small!");

		int result;

		fixed (byte* ptr = &buff[index])
			result = *((int*)ptr);

		return result;
	}

	/// <summary>
	/// Writes an <see cref="Int64"/> value to specified index in an array using unsafe pointers.
	/// </summary>
	/// <param name="value">Integer value.</param>
	/// <param name="buff">Byte buffer to write to.</param>
	/// <param name="index">Index to write at.</param>
	public static unsafe void WriteBytes(this long value, byte[] buff, int index)
	{
		if (buff.Length < index + 8)
			throw new InvalidOperationException("Buffer is too small!");

		fixed (byte* ptr = &buff[index])
			*((long*)ptr) = value;
	}

	/// <summary>
	/// Reads an <see cref="Int64"/> value from specified index in an array using unsafe pointers.
	/// </summary>
	/// <param name="buff">Byte buffer to read from.</param>
	/// <param name="index">Index to read at.</param>
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