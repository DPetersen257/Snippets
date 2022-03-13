using System.Net;
using System.Security;

namespace EpiqExtensions.Common;
public static class StringExtensions
{
    /// <summary>
    /// Truncates the specified string.
    /// </summary>
    /// <param name="data">The string to truncate.</param>
    /// <param name="maxlen">The length of the desired output string.</param>
    /// <returns>The original string, truncated to the specified length</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string Truncate(this string data, int maxlen)
        => data.Length > maxlen ? data.Remove(maxlen, data.Length - maxlen) : data;

    /// <summary>
    /// Converts a <see cref="string"/> to a <see cref="SecureString"/>.
    /// </summary>
    /// <param name="insecureString">The regular string.</param>
    /// <returns>A ReadOnly <see cref="SecureString"/>.</returns>
    public static SecureString ConvertToSecureString(this string insecureString)
    {
        var knox = new SecureString();
        foreach (char c in insecureString)
        {
            knox.AppendChar(c);
        }
        knox.MakeReadOnly();
        return knox;
    }

    /// <summary>
    /// Converts a <see cref="SecureString"/> into a <see cref="string"/>.
    /// </summary>
    /// <param name="ss">The <see cref="SecureString"/>.</param>
    /// <returns>A standard version of the string.</returns>
    public static string ConvertToString(this SecureString ss)
        => new NetworkCredential(string.Empty, ss).Password;

    /// <summary>
    /// Validation for URLs to ensure they are formatted correctly.
    /// </summary>
    /// <param name="url">a standard string.</param>
    /// <returns>True if URL is valid.</returns>
    public static bool IsValidUrl(this string url)
    {
        return !string.IsNullOrWhiteSpace(url)
            && Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
               && (uriResult?.Scheme == Uri.UriSchemeHttp || uriResult?.Scheme == Uri.UriSchemeHttps);
    }
}
