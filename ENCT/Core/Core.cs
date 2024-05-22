using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using CLI.Options;
using ENCT.Structures;
using ENCT.Utilities;
using ENCT.V1;

namespace ENCT.Core
{
  public class EnctCore
  {
    /// <summary>
    /// Method to encrypt a ENCT file.
    /// </summary>
    /// <param name="options">Options from the argument parser.</param>
    /// <returns></returns>
    public static int Encryptor(EnctEncryptOptions options)
    {
      var (srcPath, encKey) = options;

      // Throw ArgumentException if the provided file doesn't exist
      if (!Path.Exists(srcPath))
      {
        throw new ArgumentException($"Provided path \"{srcPath}\" doesn't exist.");
      }
      string srcFilename = Path.GetFileNameWithoutExtension(srcPath);
      string srcExt = Path.GetExtension(srcPath);
      string destPath = $"{srcFilename}.enct";
      if (encKey.Length != 16)
      {
        throw new ArgumentException($"Provided key must have 16 characters");
      }

      // From this moment, ENCT only fully supports text and JSON files
      if (srcExt != ".txt" && srcExt != ".json")
      {
        throw new NotImplementedException();
      }

      // Get source file contents and compute hash
      string srcContents = File.ReadAllText(srcPath, Encoding.UTF8);
      byte[] srcHash = EnctUtilities.CreateSHA256Hash(Encoding.UTF8.GetBytes(srcContents));

      // Start AES Algorithm
      Aes aesAlg = Aes.Create();
      aesAlg.Key = Encoding.UTF8.GetBytes(encKey);
      aesAlg.GenerateIV();

      byte[] swappedIV = EnctUtilities.SwapIVBytes(aesAlg.IV);
      byte[] encContents = EnctV1.EncryptV1(JsonConvert.SerializeObject(srcContents, Formatting.None), aesAlg);
      byte[] fileHeader = EnctV1.GenerateHeader(srcContents, swappedIV, srcExt, srcHash);

      using var fs = new FileStream(destPath, FileMode.Create);
      fs.Write(fileHeader);
      fs.Write(encContents);
      return 0;
    }

    public static int Decryptor(EnctDecryptOptions options)
    {
      var (srcPath, decKey) = options;

      if (!Path.Exists(srcPath))
      {
        throw new ArgumentException($"Provided path \"{srcPath}\" doesn't exist.");
      }

      byte[] enct = File.ReadAllBytes(srcPath);

      var header = EnctV1.ParseHeader(enct);
      byte[] encContents = EnctUtilities.ReadBytesAsSequence(enct, 0x50, enct.Length - 0x50);
      byte[] decryptedContents = EnctV1.DecryptV1(encContents, Encoding.ASCII.GetBytes(decKey), header.IV);
      string destFilename = Path.GetFileNameWithoutExtension(srcPath);
      string destExt = header.SourceFileType;
      string newContents = JsonConvert.DeserializeObject<string>(Encoding.UTF8.GetString(decryptedContents));
      using (var sw = new StreamWriter($"{destFilename}{destExt}"))
      {
        sw.Write(newContents);
      };

      string srcFileHash = EnctUtilities.ByteArrayToHexString(header.SourceFileHash);
      string decContentHashHex = EnctUtilities.ByteArrayToHexString(EnctUtilities.CreateSHA256Hash(Encoding.UTF8.GetBytes(newContents)));
      Console.WriteLine("ENCT file hash: {0}", srcFileHash);
      Console.WriteLine("Decrypted hash (from content): {0}", decContentHashHex);

      return 0;
    }

    public static int Header(EnctHeaderOptions options)
    {
      var (srcPath, asJson) = options;

      if (!Path.Exists(srcPath))
      {
        throw new ArgumentException($"Provided path \"{srcPath}\" doesn't exist.");
      }

      byte[] enct = new byte[0x50];
      using (var fs = new FileStream(srcPath, FileMode.Open, FileAccess.Read))
      {
        fs.Read(enct, 0, enct.Length);
      }
      ENCTParsedHeaderStruct header = EnctV1.ParseHeader(enct);

      if (asJson)
      {
        Console.WriteLine(JsonConvert.SerializeObject(header, Formatting.None));
        return 0;
      }

      Console.WriteLine($"Header from file \"{srcPath}\":\n");
      Console.WriteLine($"FileVersion: {header.FileVersion}");
      Console.WriteLine($"CreationDate: {header.CreationDate}");
      Console.WriteLine($"CreationTime: {header.CreationTime}");
      Console.WriteLine($"SourceFileContentSize: {header.SourceFileContentSize} bytes");
      Console.WriteLine($"SourceFileType: {header.SourceFileType}");
      Console.WriteLine($"IV: {BitConverter.ToString(EnctUtilities.SwapIVBytes(header.IV)).Replace("-", "").ToLower()}");
      Console.WriteLine($"SourceFileHash: sha256-{BitConverter.ToString(header.SourceFileHash).Replace("-", "").ToLower()}");

      return 0;
    }
  }
}