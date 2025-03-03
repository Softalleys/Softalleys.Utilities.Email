namespace Softalleys.Utilities.Email.Models;

/// <summary>
/// Represents an email address with an associated name.
/// </summary>
/// <param name="Mail">The email address.</param>
/// <param name="Name">The name associated with the email address.</param>
public record EmailAddress(string Mail, string Name);