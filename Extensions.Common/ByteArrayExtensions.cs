using System.IO.Compression;

namespace EpiqExtensions.Common;
public static class ByteArrayExtensions
{
    /// <summary>
    /// Saves to a new file.
    /// </summary>
    /// <param name="byteArray">The data to save.</param>
    /// <param name="filePath">The path where the template should be saved.</param>
    /// <param name="ct">An optional cancellation token.</param>
    /// <returns>A <see cref="FileInfo"/> for the new file.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="System.Security.SecurityException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static FileInfo SaveToFile(this byte[] byteArray, string filePath, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentNullException(nameof(filePath));

        using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);

        fs.WriteAsync(byteArray, 0, byteArray.Length, ct);
        return new FileInfo(filePath);
    }

    public static byte[] GZip(this byte[] bytes)
    {
        using var msi = new MemoryStream(bytes);
        using var mso = new MemoryStream();
        using (var gs = new GZipStream(mso, CompressionLevel.Optimal))
        {
            msi.CopyTo(gs);
        }
        return mso.ToArray();
    }

    public static byte[] GUnzip(this byte[] bytes)
    {
        using var msi = new MemoryStream(bytes);
        using var mso = new MemoryStream();
        using (var gs = new GZipStream(msi, CompressionMode.Decompress))
        {
            gs.CopyTo(mso);
            gs.Flush();
            mso.Position = 0;
        }
        return mso.ToArray();
    }
}