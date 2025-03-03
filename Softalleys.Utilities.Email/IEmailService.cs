using Softalleys.Utilities.Email.Models;

namespace Softalleys.Utilities.Email;

/// <summary>
/// Interface for email service.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="requested">The email message request containing subject, body, recipients, and optional attachments.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    Task SendEmailAsync(EmailMessageRequested requested, CancellationToken cancellationToken = default);
}