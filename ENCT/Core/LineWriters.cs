using System;

namespace ENCT.LineWriters
{
  public class EnctLineWriters
  {
    public static void e(string srcPath, string key)
    {
      Console.WriteLine($"Src: {srcPath}\nKey: {key}");
    }
  }
}