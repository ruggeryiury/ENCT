using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ENCT.Structures;
using ENCT.Utilities;

namespace ENCT.V1
{
  public class EnctV1
  {
    /// <summary>
    /// Generates a ENCT file header and returns as a byte array.
    /// </summary>
    /// <param name="srcContents">The content of the source file.</param>
    /// <param name="swappedIV">A generated initialization vector.</param>
    /// <param name="srcExt">The extension of the source file.</param>
    /// <param name="fileVersion">The version of the ENCT file.</param>
    /// <returns>The file header as a byte array.</returns>
    public static byte[] GenerateHeader(string srcContents, byte[] swappedIV, string srcExt, byte[] srcHash, int fileVersion = 1)
    {
      var mStream = new MemoryStream();
      var bw = new BinaryWriter(mStream);
      var now = DateTime.Now;
      ushort srcFileType = EnctUtilities.GetSourceFileTypeByExt(srcExt);

      ENCTHeaderStruct header = new ENCTHeaderStruct
      {
        Magic = "ENCT",
        FileVersion = (ushort)fileVersion,
        CreationDate = now.ToString("yyyy-MM-dd"),
        CreationTime = now.ToString("HH:mm:ss"),
        SourceFileContentSize = (UInt32)srcContents.Length,
        SourceFileType = srcFileType,
        IV = swappedIV,
        SourceFileHash = srcHash
      };

      bw.Write(Encoding.ASCII.GetBytes(header.Magic));
      bw.Write(BitConverter.GetBytes(header.FileVersion));
      bw.Write(Encoding.ASCII.GetBytes(header.CreationDate));
      bw.Write(Encoding.ASCII.GetBytes(header.CreationTime));
      bw.Write(BitConverter.GetBytes(header.SourceFileContentSize));
      bw.Write(BitConverter.GetBytes(header.SourceFileType));
      bw.Write(new byte[2]);
      bw.Write(header.IV);
      bw.Write(header.SourceFileHash);
      return mStream.ToArray();
    }

    /// <summary>
    /// Returns a structure with all header values formatted.
    /// </summary>
    /// <param name="enct">The first 80 bytes of a ENCT file.</param>
    /// <returns></returns>
    public static ENCTParsedHeaderStruct ParseHeader(byte[] enct)
    {
      var header = new ENCTParsedHeaderStruct()
      {
        Magic = Encoding.ASCII.GetString(enct, 0x00, 0x04),
        FileVersion = BitConverter.ToUInt16(enct, 0x04),
        CreationDate = Encoding.ASCII.GetString(enct, 0x06, 0x0A),
        CreationTime = Encoding.ASCII.GetString(enct, 0x10, 0x08),
        SourceFileContentSize = BitConverter.ToUInt32(enct, 0x18),
        SourceFileType = EnctUtilities.GetSourceFileTypeByInt(BitConverter.ToUInt16(enct, 0x1C)),
        IV = EnctUtilities.SwapIVBytes(EnctUtilities.ReadBytesAsSequence(enct, 0x20, 0x10)),
        SourceFileHash = EnctUtilities.ReadBytesAsSequence(enct, 0x30, 0x20)
      };

      return header;
    }
    /// <summary>
    /// Encrypts the file contents and returns as a byte array.
    /// </summary>
    /// <param name="srcContents">The source file contents.</param>
    /// <param name="aes">Cryptographic class that is used to perform the symmetric algorithm.</param>
    /// <returns>The encrypted file contents as a byte array.</returns>
    public static byte[] EncryptV1(string srcContents, Aes aes)
    {
      byte[] encContents;

      var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

      using (var msEncrypt = new MemoryStream())
      {
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
          // Write all data to the stream
          swEncrypt.Write(srcContents);
        }

        encContents = msEncrypt.ToArray();
      }

      return encContents;
    }

    public static byte[] DecryptV1(byte[] encContents, byte[] key, byte[] iv)
    {
      byte[] decContentBytes = null;
      using (var aesAlg = Aes.Create())
      {
        aesAlg.Key = key;
        aesAlg.IV = iv;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var ms = new MemoryStream(encContents);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        decContentBytes = Encoding.UTF8.GetBytes(sr.ReadToEnd());
      }

      return decContentBytes;
    }
  }
}