using CommandLine;
using CLI.Options;
using ENCT.Core;

namespace ENCT
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Parser.Default.ParseArguments<EnctEncryptOptions, EnctDecryptOptions, EnctHeaderOptions>(args)
      .MapResult(
        (EnctEncryptOptions options) => EnctCore.Encryptor(options),
        (EnctDecryptOptions options) => EnctCore.Decryptor(options),
        (EnctHeaderOptions options) => EnctCore.Header(options),
        errors => 1
      );
    }
  }
}