using System.Security.Cryptography;

namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Helpers that compute string hashes from binary data.
/// </summary>
public static class HashHelper
{
    /// <summary>
    /// Computes string hash from a stream.
    /// </summary>
    public static string ComputeHash(Stream stream)
    {
        var sha = SHA256.Create();
        byte[] checksum = sha.ComputeHash(stream);
        return BitConverter.ToString(checksum).Replace("-", string.Empty);
    }

    /// <summary>
    /// Computes hash from a file name
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string ComputeHash(string fileName)
    {
        using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite,
                                          16 * 1024 * 1024);
        return ComputeHash(stream);
    }
}