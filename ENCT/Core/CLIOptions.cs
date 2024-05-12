using System.Collections.Generic;
using CommandLine;

namespace CLI.Options
{
  /// <summary>
  /// CLI Options for encrypt method.
  /// </summary>
  [Verb("encrypt", HelpText = "Encrypt a text file.")]
  public class EnctEncryptOptions
  {
    /// <summary>
    /// The source file to be encrypted.
    /// </summary>
    [Value(0, MetaName = "src",HelpText = "The file to be encrypted.", Required = true, Max = 1)]
    public IEnumerable<string> Src { get; set; }
    /// <summary>
    /// The user key to encrypt the file.
    /// </summary>
    [Option('k', "key", Required = true, HelpText = "A 16 characters encryption key.")]
    public string Key { get; set; }
  }

  /// <summary>
  /// CLI Options for decrypt method.
  /// </summary>
  [Verb("decrypt", HelpText = "Decrypt a ENCT file.")]
  public class EnctDecryptOptions
  {
    /// <summary>
    /// The source file to be decrypted.
    /// </summary>
    [Value(0, MetaName = "src",HelpText = "The file to be decrypted.", Required = true, Max = 1)]
    public IEnumerable<string> Src { get; set; }
    /// <summary>
    /// The user key to decrypt the file.
    /// </summary>
    [Option('k', "key", Required = true, HelpText = "A 16 characters decryption key.")]
    public string Key { get; set; }

  }
}