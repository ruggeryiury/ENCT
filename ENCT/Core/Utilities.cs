using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ENCT.Utilities
{
  public class EnctUtilities
  {
    /// <summary>
    /// Compute the SHA256 hash from a file contents.
    /// </summary>
    /// <param name="srcContents">The file contents as a byte array.</param>
    /// <returns>A computed SHA256 hash from a file as a byte array.</returns>
    public static byte[] CreateSHA256Hash(byte[] srcContents)
    {
      return SHA256.HashData(srcContents);
    }

    /// <summary>
    /// Returns the initialization vector, swapping every 2-bytes.
    /// </summary>
    /// <param name="iv">A generated initialization vector.</param>
    /// <returns>The swapped initialization vector as a byte array.</returns>
    public static byte[] SwapIVBytes(byte[] iv)
    {
      byte[] swapped = new byte[iv.Length];

      for (int i = 0; i < iv.Length; i += 2)
      {
        swapped[i] = iv[i + 1];
        swapped[i + 1] = iv[i];
      }

      return swapped;
    }

    /// <summary>
    /// Reads from the provided buffer the bytes from the provided offset,
    /// with the provided length and returns these bytes as a byte array.
    /// </summary>
    /// <param name="buffer">The buffer you want to read bytes from.</param>
    /// <param name="offset">The start offset of the byte reading.</param>
    /// <param name="length">The length of the byte reading.</param>
    /// <returns>A byte array of the bytes from the provided offset start with the provided length.</returns>
    public static byte[] ReadBytesAsSequence(byte[] buffer, int offset, int length)
    {
      byte[] extractedBytes = new byte[length];
      Array.Copy(buffer, offset, extractedBytes, 0, length);
      return extractedBytes;
    }

    /// <summary>
    /// Builds a string with hexadecimal values from a byte array.
    /// </summary>
    /// <param name="bytes">The byte array to convert.</param>
    /// <returns>A string with with hexadecimal values converted from a byte array.</returns>
    public static string ByteArrayToHexString(byte[] bytes)
    {
      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < bytes.Length; i++)
      {
        builder.Append(bytes[i].ToString("x2"));
      }
      return builder.ToString();
    }
  }
}