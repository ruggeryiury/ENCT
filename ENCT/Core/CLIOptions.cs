using System.Collections.Generic;
using CommandLine;

namespace CLI.Options
{
  /// <summary>
  /// CLI Options for encrypt method.
  /// </summary>
  [Verb("encrypt", HelpText = "Encrypt a text or JSON file.")]
  public class EnctEncryptOptions
  {
    /// <summary>
    /// The source file to be encrypted.
    /// </summary>
    [Option('i', "input", Required = true, HelpText = "The source file to be encrypted.")]
    public string Input { get; set; }
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
    /// The ENCT file to be decrypted.
    /// </summary>
    [Option('i', "input", Required = true, HelpText = "The ENCT file to be decrypted.")]
    public string Input { get; set; }
    /// <summary>
    /// The user key to decrypt the file.
    /// </summary>
    [Option('k', "key", Required = true, HelpText = "A 16 characters decryption key.")]
    public string Key { get; set; }

  }

  /// <summary>
  /// CLI Options for header method.
  /// </summary>
  [Verb("header", HelpText = "Parses a ENCT file header")]
  public class EnctHeaderOptions
  {
    /// <summary>
    /// The ENCT file to parse its header.
    /// </summary>
    [Option('i', "input", Required = true, HelpText = "The ENCT file to parse its header.")]
    public string Input { get; set; }
  }
}