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
    public ushort FileVersion {get; set;}

    /// <summary>
    /// The ENCT file creation date, formatted as YYYY-MM-DD.
    /// </summary>
    public string CreationDate {get; set;}

    /// <summary>
    /// The ENCT file creation time, formatted as HH:MM:SS.
    /// </summary>
    public string CreationTime {get; set;}

    /// <summary>
    /// The source file contents size as bytes.
    /// </summary>
    public uint SourceFileContentSize {get; set;}

    /// <summary>
    /// The source file type.
    /// </summary>
    public ushort SourceFileType {get; set;}

    /// <summary>
    /// The initialization vector of the encryption used to encrypt the file.
    /// </summary>
    public byte[] IV {get; set;}

    /// <summary>
    /// The source file hash.
    /// </summary>
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

    /// <summary>
    /// The ENCT file creation date, formatted as YYYY-MM-DD.
    /// </summary>
    public string CreationDate {get; set;}

    /// <summary>
    /// The ENCT file creation time, formatted as HH:MM:SS.
    /// </summary>
    public string CreationTime {get; set;}

    /// <summary>
    /// The source file contents size as bytes.
    /// </summary>
    public uint SourceFileContentSize {get; set;}

    /// <summary>
    /// The source file type.
    /// </summary>
    public ushort SourceFileType {get; set;}

    /// <summary>
    /// The initialization vector of the encryption used to encrypt the file.
    /// </summary>
    public byte[] IV {get; set;}

    /// <summary>
    /// The source file hash.
    /// </summary>
    public byte[] SourceFileHash {get; set;}
  }
}