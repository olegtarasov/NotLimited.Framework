using System.Buffers;

namespace NotLimited.Framework.Common.Streams;

/// <summary>
/// Base class for streams which force a specific EOL scheme.
/// </summary>
public abstract class ForceSpecificEolStreamBase : Stream
{
    /// <summary>
    /// Wrapped stream.
    /// </summary>
    protected readonly Stream InnerStream;

    /// <summary>
    /// Ctor.
    /// </summary>
    public ForceSpecificEolStreamBase(Stream innerStream)
    {
        if (!innerStream.CanSeek || !innerStream.CanRead)
            throw new NotSupportedException("Underlying stream should support at least reading and seeking");

        InnerStream = innerStream;
    }

    /// <inheritdoc />
    public override bool CanRead => true;

    /// <inheritdoc />
    public override bool CanSeek => false;

    /// <inheritdoc />
    public override bool CanWrite => InnerStream.CanWrite;

    /// <inheritdoc />
    public override long Length => throw new NotSupportedException();

    /// <inheritdoc />
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override int ReadByte()
    {
        var buff = ArrayPool<byte>.Shared.Rent(1);
        int read = Read(buff, 0, 1);
        int result = buff[0];

        ArrayPool<byte>.Shared.Return(buff);

        return read == 0 ? -1 : result;
    }

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        int actualRead;
        do
        {
            int read = InnerStream.Read(buffer, offset, count);
            if (read <= 0)
                return read;

            actualRead = ProcessSpan(new Span<byte>(buffer, offset, read));
        } while (actualRead <= 0);

        return actualRead;
    }

    /// <inheritdoc />
    public override int Read(Span<byte> buffer)
    {
        int actualRead;
        do
        {
            int read = InnerStream.Read(buffer);
            if (read <= 0)
                return read;

            actualRead = ProcessSpan(read != buffer.Length ? buffer[..read] : buffer);
        } while (actualRead <= 0);

        return actualRead;
    }

    /// <inheritdoc />
    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        int actualRead;
        do
        {
            int read = await InnerStream.ReadAsync(buffer, offset, count, cancellationToken);
            if (read <= 0)
                return read;

            actualRead = ProcessSpan(new Memory<byte>(buffer, offset, read).Span);
        } while (actualRead <= 0);

        return actualRead;
    }

    /// <inheritdoc />
    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        int actualRead;
        do
        {
            int read = await InnerStream.ReadAsync(buffer, cancellationToken);
            if (read <= 0)
                return read;

            actualRead = ProcessSpan(read != buffer.Length ? buffer.Span.Slice(0, read) : buffer.Span);
        } while (actualRead <= 0);

        return actualRead;
    }

    /// <inheritdoc />
    public override void WriteByte(byte value)
    {
        throw new NotSupportedException(
            "Can't write a single byte, since there is no way to know whether CR will be followed by LF");
    }

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count)
    {
        var copy = new byte[count - offset];
        Array.Copy(buffer, offset, copy, 0, count);

        int actualCount = ProcessSpan(new Span<byte>(copy));
        InnerStream.Write(copy, 0, actualCount);
    }

    /// <inheritdoc />
    public override void Write(ReadOnlySpan<byte> buffer)
    {
        var copy = buffer.ToArray();

        int actualCount = ProcessSpan(new Span<byte>(copy));
        InnerStream.Write(copy, 0, actualCount);
    }

    /// <inheritdoc />
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        var copy = new byte[count - offset];
        Array.Copy(buffer, offset, copy, 0, count);

        int actualCount = ProcessSpan(new Span<byte>(copy));
        return InnerStream.WriteAsync(copy, 0, actualCount, cancellationToken);
    }

    /// <inheritdoc />
    public override async ValueTask WriteAsync(
        ReadOnlyMemory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        var copy = buffer.ToArray();

        int actualCount = ProcessSpan(new Span<byte>(copy));
        await InnerStream.WriteAsync(copy, 0, actualCount, cancellationToken);
    }

    /// <inheritdoc />
    public override void Flush()
    {
        InnerStream.Flush();
    }

    /// <inheritdoc />
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
        return InnerStream.FlushAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override void Close()
    {
        InnerStream.Close();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        InnerStream.Dispose();
    }

    /// <inheritdoc />
    public override ValueTask DisposeAsync()
    {
        return InnerStream.DisposeAsync();
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override void SetLength(long value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Processes line endings in a span.
    /// </summary>
    protected abstract int ProcessSpan(Span<byte> span);
}