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
    public string Magic {get; set;}

    /// <summary>
    /// The file version of the ENCT file.
    /// </summary>
    public UInt16 FileVersion {get; set;}
    public string CreationDate {get; set;}
    public string CreationTime {get; set;}
    public UInt32 SourceFileContentSize {get; set;}
    public UInt16 SourceFileType {get; set;}
    public byte[] IV {get; set;}
    public byte[] SourceFileHash {get; set;}
  }

  public struct ENCTParsedHeaderStruct {
    /// <summary>
    /// The identifier of the ENCT file as an ASCII-encoded string.
    /// </summary>
    public string Magic {get; set;}

    /// <summary>
    /// The file version of the ENCT file.
    /// </summary>
    public ushort FileVersion {get; set;}
    public string CreationDate {get; set;}
    public string CreationTime {get; set;}
    public uint SourceFileContentSize {get; set;}
    public ushort SourceFileType {get; set;}
    public byte[] IV {get; set;}
    public byte[] SourceFileHash {get; set;}
  }
}