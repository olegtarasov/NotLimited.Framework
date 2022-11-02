using System.Buffers;
using System.Collections;
using System.Runtime.InteropServices;

namespace NotLimited.Framework.Common.Streams;

/// <summary>
/// A wrapper stream that converts LF bytes to CRLF on the fly.
/// </summary>
public class ForceCrLfStream : ForceSpecificEolStreamBase
{
    private List<byte>? _tail;


    /// <summary>
    /// Ctor.
    /// </summary>
    public ForceCrLfStream(Stream innerStream) : base(innerStream)
    {
    }

    /// <inheritdoc />
    public override int ReadByte()
    {
        var buff = ArrayPool<byte>.Shared.Rent(1);
        if (ReadTail(new Span<byte>(buff)) >= 0)
        {
            int result = buff[0];
            ArrayPool<byte>.Shared.Return(buff);

            return result;
        }

        return base.ReadByte();
    }

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        var span = new Span<byte>(buffer, offset, count);
        int read = ReadTail(span);
        if (read >= 0)
            return read;

        return base.Read(buffer, offset, count);
    }

    /// <inheritdoc />
    public override int Read(Span<byte> buffer)
    {
        int read = ReadTail(buffer);
        if (read >= 0)
            return read;

        return base.Read(buffer);
    }

    /// <inheritdoc />
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        int read = ReadTail(buffer);
        if (read >= 0)
            return Task.FromResult(read);

        return base.ReadAsync(buffer, offset, count, cancellationToken);
    }

    /// <inheritdoc />
    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        int read = ReadTail(buffer.Span);
        if (read >= 0)
            return ValueTask.FromResult(read);

        return base.ReadAsync(buffer, cancellationToken);
    }

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count)
    {
        base.Write(buffer, offset, count);
        
        WriteTail();
    }

    /// <inheritdoc />
    public override void Write(ReadOnlySpan<byte> buffer)
    {
        base.Write(buffer);

        WriteTail();
    }

    /// <inheritdoc />
    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        await base.WriteAsync(buffer, offset, count, cancellationToken);
        
        WriteTail();
    }

    /// <inheritdoc />
    public override async ValueTask WriteAsync(
        ReadOnlyMemory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        await base.WriteAsync(buffer, cancellationToken);
        
        WriteTail();
    }

    /// <inheritdoc />
    protected override int ProcessSpan(Span<byte> span)
    {
        var list = new List<byte>(span.Length * 2);

        for (int i = 0; i < span.Length; i++)
        {
            if (span[i] == '\n')
            {
                if (i == 0 || span[i - 1] != '\r')
                    list.Add((byte)'\r');
            }
            
            list.Add(span[i]);
        }

        if (list.Count == span.Length)
            return span.Length;

        CollectionsMarshal.AsSpan(list)[..span.Length].CopyTo(span);
        _tail = list.GetRange(span.Length, list.Count - span.Length);

        return span.Length;
    }

    private int ReadTail(Span<byte> span)
    {
        if (_tail == null || _tail.Count == 0)
            return -1;

        int cnt = Math.Min(span.Length, _tail.Count);
        CollectionsMarshal.AsSpan(_tail)[..cnt].CopyTo(span);
        _tail.RemoveRange(0, cnt);

        if (_tail.Count == 0)
            _tail = null;

        return cnt;
    }

    private void WriteTail()
    {
        if (_tail == null || _tail.Count == 0)
            return;

        var span = CollectionsMarshal.AsSpan(_tail);
        InnerStream.Write(span);
        _tail = null;
    }
}