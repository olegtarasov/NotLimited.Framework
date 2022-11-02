namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Various helpers to work with text.
/// </summary>
public static class TextHelpers
{
    /// <summary>
    /// Tries to guess if specified buffer contains text data by checking for \0 bytes.
    /// </summary>
    public static bool IsTextData(byte[] buff)
    {
        for (int i = 0; i < buff.Length; i++)
        {
            if (buff[i] == 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Ensures a string has correct platform-specific line endings.
    /// </summary>
    public static string EnsurePlatformLineEndings(string input)
    {
        if (input.IndexOf('\n') == -1)
            return input; // There are no line endings at all, skip

        if (OperatingSystem.IsWindows())
        {
            if (input.IndexOf("\r\n", StringComparison.Ordinal) > -1)
                return input; // There is at least one correct line ending, so we assume content is correct.

            return input.Replace("\n", "\r\n");
        }

        if (input.IndexOf("\r\n", StringComparison.Ordinal) == -1)
            return input;

        return input.Replace("\r\n", "\n");
    }
}