using System.Net;

namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Helpers for IP addresses
/// </summary>
public static class IPHelpers
{
    /// <summary>
    /// Gets an external IP address using https://ipify.org.
    /// </summary>
    public static IPAddress? GetExternalIp()
    {
        var httpClient = new HttpClient();

        try
        {
            string? ip = AsyncHelpers.RunSync(() => httpClient.GetStringAsync("https://api.ipify.org"));
            if (string.IsNullOrEmpty(ip))
                return null;

            return IPAddress.Parse(ip);
        }
        catch (Exception)
        {
            return null;
        }
    }
}