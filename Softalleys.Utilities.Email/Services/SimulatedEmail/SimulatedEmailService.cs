using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Softalleys.Utilities.Email.Models;
using Softalleys.Utilities.Email.Services.SmtpEmail;

namespace Softalleys.Utilities.Email.Services.SimulatedEmail;

public class SimulatedEmailService(ILogger<SimulatedEmailService> logger, IOptions<SmtpEmailOptions> options) : IEmailService
{
    public async Task SendEmailAsync(EmailMessageRequested requested, CancellationToken cancellationToken = default)
    {
        // Log the request and options objects
        logger.LogInformation("Email request: {@Requested}", requested);
        logger.LogInformation("Email options: {@Options}", options.Value);

        // Print the email body to the console
        Console.WriteLine(@"Simulated email body:\n\n");
        Console.WriteLine(requested.BodyTemplate);
        
        Console.WriteLine("\n\n");

        await Task.CompletedTask;
    }
}