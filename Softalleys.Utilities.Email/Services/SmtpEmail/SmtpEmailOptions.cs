namespace Softalleys.Utilities.Email.Services.SmtpEmail;

/// <summary>
/// Represents the configuration options for email settings.
/// </summary>
public record SmtpEmailOptions
{
    /// <summary>
    /// The address of the SMTP server used to send emails.
    /// </summary>
    public string SmtpServer { get; set; } = "smtp.example.com";
    
    /// <summary>
    /// The port number on the SMTP server to connect to.
    /// </summary>
    public int Port { get; set; } = 465;

    /// <summary>
    /// The username for authenticating with the SMTP server.
    /// </summary>
    public string Username { get; set; } = "username";

    /// <summary>
    /// The password for authenticating with the SMTP server.
    /// </summary>
    public string Password { get; set; } = "password";

    /// <summary>
    /// Indicates whether SSL should be used when connecting to the SMTP server.
    /// </summary>
    public bool EnableSsl { get; set; } = true;

    /// <summary>
    /// The email address that will appear in the 'From' field of the email.
    /// </summary>
    public string FromAddress { get; set; } = "dont-reply@example.com";
    
    /// <summary>
    /// The name that will appear in the 'From' field of the email.
    /// </summary>
    public string FromName { get; set; } = "Email Service";
}