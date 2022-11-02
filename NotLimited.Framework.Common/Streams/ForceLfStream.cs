namespace NotLimited.Framework.Common.Streams;

/// <summary>
/// A wrapper stream that converts CRLF sequences to LF on the fly.
/// </summary>
public class ForceLfStream : ForceSpecificEolStreamBase
{
    /// <summary>
    /// Ctor.
    /// </summary>
    public ForceLfStream(Stream innerStream) : base(innerStream)
    {
    }

    /// <summary>
    /// Processes line endings in a span.
    /// </summary>
    protected override int ProcessSpan(Span<byte> span)
    {
        int newRead = span.Length;
        int idxWrite = 0, idxRead = 0;
        while (idxRead < span.Length)
        {
            if (span[idxRead] == '\r')
            {
                int readAhead;
                if (idxRead + 1 == span.Length)
                {
                    readAhead = InnerStream.ReadByte();
                    if (readAhead > -1 && readAhead != '\n')
                        InnerStream.Seek(-1, SeekOrigin.Current);
                }
                else
                {
                    readAhead = span[idxRead + 1];
                }

                if (readAhead == '\n')
                {
                    span[idxWrite] = (byte)'\n';

                    idxRead += 2;
                    idxWrite++;
                    newRead--;
                    continue;
                }
            }

            span[idxWrite] = span[idxRead];
            idxRead++;
            idxWrite++;
        }

        return newRead;
    }
}