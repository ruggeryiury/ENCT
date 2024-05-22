using CommandLine;

namespace CLI.Options
{
  /// <summary>
  /// CLI Options for encrypt method.
  /// </summary>
  [Verb("encrypt", HelpText = "Encrypts a .txt/.json file to ENCT.")]
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

    /// <summary>
    /// Deconstruct all encrypt options.
    /// </summary>
    /// <param name="srcPath">The source file path.</param>
    /// <param name="encKey">A 16 characters encryption key.</param>
    public void Deconstruct(out string srcPath, out string encKey)
    {
      srcPath = Input;
      encKey = Key;
    }
  }

  /// <summary>
  /// CLI Options for decrypt method.
  /// </summary>
  [Verb("decrypt", HelpText = "Decrypts a ENCT file.")]
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
    [Option('k', "key", Required = true, HelpText = "The 16 characters decryption key.")]
    public string Key { get; set; }

    /// <summary>
    /// Deconstruct all decrypt options.
    /// </summary>
    /// <param name="srcPath">The source file path.</param>
    /// <param name="decKey">A 16 characters decryption key.</param>
    public void Deconstruct(out string srcPath, out string decKey)
    {
      srcPath = Input;
      decKey = Key;
    }

  }

  /// <summary>
  /// CLI Options for header method.
  /// </summary>
  [Verb("header", HelpText = "Parses a ENCT file header and prints the header contents.")]
  public class EnctHeaderOptions
  {
    /// <summary>
    /// The ENCT file path to parse its header.
    /// </summary>
    [Option('i', "input", Required = true, HelpText = "The ENCT file path to parse its header.")]
    public string Input { get; set; }

    /// <summary>
    /// Set output as a JSON object.
    /// </summary>
    [Option('j', "json", Required = false, HelpText = "Set output as a JSON object", Default = false)]
    public bool AsJSON { get; set; }

    /// <summary>
    /// Deconstruct all header options.
    /// </summary>
    /// <param name="srcPath">The source file path.</param>
    /// <param name="asJson">A boolean value that tells if the output of the CLI must be formatted as JSON.</param>
    public void Deconstruct(out string srcPath, out bool asJson)
    {
      srcPath = Input;
      asJson = AsJSON;
    }
  }
}