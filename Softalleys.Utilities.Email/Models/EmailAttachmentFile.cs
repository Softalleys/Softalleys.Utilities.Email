namespace Softalleys.Utilities.Email.Models;

/// <summary>
/// Represents an email attachment file with its data, filename, and content type.
/// </summary>
/// <param name="Data">The binary data of the attachment file.</param>
/// <param name="FileName">The name of the attachment file.</param>
/// <param name="ContentType">The MIME type of the attachment file.</param>
public record EmailAttachmentFile(byte[] Data, string FileName, string ContentType);