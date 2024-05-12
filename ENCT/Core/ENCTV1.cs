using System;
using System.IO;
using System.Linq;
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
      MemoryStream mStream = new MemoryStream();
      BinaryWriter bw = new BinaryWriter(mStream);
      DateTime now = DateTime.Now;
      UInt16 srcFile;

      if (srcExt == ".txt") srcFile = (UInt16)0;
      else srcFile = (UInt16)1;

      ENCTHeaderStruct header = new ENCTHeaderStruct
      {
        Magic = "ENCT",
        FileVersion = (UInt16)fileVersion,
        CreationDate = now.ToString("yyyy-MM-dd"),
        CreationTime = now.ToString("HH:mm:ss"),
        SourceFileContentSize = (UInt32)srcContents.Length,
        SourceFileType = srcFile,
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

    public static ENCTParsedHeaderStruct ParseHeader(byte[] enct)
    {
      ENCTParsedHeaderStruct header = new ENCTParsedHeaderStruct
      {
        Magic = Encoding.ASCII.GetString(EnctUtilities.ReadBytesAsSequence(enct, 0x0, 0x4)),
        FileVersion = BitConverter.ToUInt16(EnctUtilities.ReadBytesAsSequence(enct, 0x4, 0x2)),
        CreationDate = Encoding.ASCII.GetString(EnctUtilities.ReadBytesAsSequence(enct, 0x6, 0xA)),
        CreationTime = Encoding.ASCII.GetString(EnctUtilities.ReadBytesAsSequence(enct, 0x10, 0x8)),
        SourceFileContentSize = BitConverter.ToUInt32(EnctUtilities.ReadBytesAsSequence(enct, 0x18, 0x4)),
        SourceFileType = BitConverter.ToUInt16(EnctUtilities.ReadBytesAsSequence(enct, 0x1C, 0x2)),
        IV = EnctUtilities.SwapIVBytes(EnctUtilities.ReadBytesAsSequence(enct, 0x20, 0x10)),
        SourceFileHash = EnctUtilities.ReadBytesAsSequence(enct, 0x30, 0x20),
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

      ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

      using (MemoryStream msEncrypt = new MemoryStream())
      {
        using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
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
      using (Aes aesAlg = Aes.Create())
      {
        aesAlg.Key = key;
        aesAlg.IV = iv;

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream ms = new MemoryStream(encContents);
        using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using StreamReader sr = new StreamReader(cs);
        decContentBytes = Encoding.UTF8.GetBytes(sr.ReadToEnd());
      }

      return decContentBytes;
    }
  }
}