using System;

namespace ENCT.Structures
{
  /// <summary>
  /// A header structure for a ENCT file.
  /// </summary>
  public struct ENCTHeaderStruct
  {
    /// <summary>
    /// The identifier of the ENCT file as an ASCII-encoded string.
    /// </summary>
    public string Magic;

    /// <summary>
    /// The file version of the ENCT file.
    /// </summary>
    public UInt16 FileVersion;
    public string CreationDate;
    public string CreationTime;
    public UInt32 SourceFileContentSize;
    public UInt16 SourceFileType;
    public byte[] IV;
    public byte[] SourceFileHash;
  }

  public struct ENCTParsedHeaderStruct {
    /// <summary>
    /// The identifier of the ENCT file as an ASCII-encoded string.
    /// </summary>
    public string Magic;

    /// <summary>
    /// The file version of the ENCT file.
    /// </summary>
    public ushort FileVersion;
    public string CreationDate;
    public string CreationTime;
    public uint SourceFileContentSize;
    public ushort SourceFileType;
    public byte[] IV;
    public byte[] SourceFileHash;

  }
}