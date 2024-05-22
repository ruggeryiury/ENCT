using System;
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
      var swapped = new byte[iv.Length];

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
      var extractedBytes = new byte[length];
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
      var builder = new StringBuilder();
      for (int i = 0; i < bytes.Length; i++)
      {
        builder.Append(bytes[i].ToString("x2"));
      }
      return builder.ToString();
    }

    /// <summary>
    /// Returns a UInt16 representation of a source file extension.
    /// </summary>
    /// <param name="srcExt">The extension of the source file.</param>
    /// <returns>A UInt16 representation of a source file extension.</returns>
    public static ushort GetSourceFileTypeByExt(string srcExt)
    {
      ushort type = srcExt switch
      {
        ".json" => 1,
        _ => 0
      };

      return type;
    }

    /// <summary>
    /// Returns a string representation of a source file extension based on the ENCT source file type.
    /// </summary>
    /// <param name="srcFileType">A UInt16 value taken from the ENCT file header, representing the original file extension.</param>
    /// <returns>A string representation of a source file extension based on the ENCT source file type.</returns>
    public static string GetSourceFileTypeByInt(ushort srcFileType)
    {
      string type = srcFileType switch
      {
        1 => ".json",
        _ => ".txt"
      };

      return type;
    }
  }

}