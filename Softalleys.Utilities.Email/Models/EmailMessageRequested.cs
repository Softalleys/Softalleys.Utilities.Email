namespace Softalleys.Utilities.Email.Models;

/// <summary>
/// Represents a request to send an email message.
/// </summary>
/// <param name="Subject">The subject of the email.</param>
/// <param name="BodyTemplate">The body template of the email.</param>
/// <param name="Recipients">The recipients of the email.</param>
/// <param name="CarbonCopyRecipients">The carbon copy recipients of the email. Optional.</param>
/// <param name="BlindCarbonCopyRecipients">The blind carbon copy recipients of the email. Optional.</param>
/// <param name="Attachments">The attachments of the email. Optional.</param>
public record EmailMessageRequested(
    string Subject,
    string BodyTemplate, 
    EmailAddress[] Recipients, 
    EmailAddress[]? CarbonCopyRecipients = null,
    EmailAddress[]? BlindCarbonCopyRecipients = null,
    EmailAttachmentFile[]? Attachments = null);