namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Various file helpers.
/// </summary>
public static class FileHelpers
{
    /// <summary>
    /// Reads the amount of bytes specified in <paramref name="count"/> or less if file is smaller.
    /// </summary>
    public static byte[] ReadFileBytes(string path, int count)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var buff = new byte[count];
        int read = stream.Read(buff, 0, count);

        if (read < count)
            Array.Resize(ref buff, read);

        return buff;
    }

    /// <summary>
    /// Reads first 8000 bytes from file and tries to guess whether it's text by checking for \0 bytes.
    /// </summary>
    public static bool IsTextFile(string fileName)
    {
        var head = ReadFileBytes(fileName, 8000);
        return TextHelpers.IsTextData(head);
    }
}