using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Softalleys.Utilities.Email.Models;

namespace Softalleys.Utilities.Email.Services.SmtpEmail;

public class SmtpEmailService(
    ILogger<SmtpEmailService> logger,
    IOptions<SmtpEmailOptions> options) : IEmailService
{
    public async Task SendEmailAsync(EmailMessageRequested requested, CancellationToken cancellationToken = default)
    {
        try
        {
            var emailOpts = options.Value;

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(emailOpts.FromName, emailOpts.FromAddress));

            message.To.AddRange(requested.Recipients.Select(r =>
                new MailboxAddress(r.Name, r.Mail)));

            message.Subject = requested.Subject;

            var html = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = requested.BodyTemplate
            };

            var files = new List<MimeEntity>();

            if (requested.Attachments != null)
            {
                foreach (var attachment in requested.Attachments)
                {
                    var contentTypeSplit = attachment.ContentType.Split("/");
                    var contentType = contentTypeSplit.Length != 2
                        ? new ContentType("application", attachment.ContentType)
                        : new ContentType(contentTypeSplit[0], contentTypeSplit[1]);

                    var attachmentPart = new MimePart(contentType)
                    {
                        Content = new MimeContent(new MemoryStream(attachment.Data)),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = attachment.FileName
                    };

                    files.Add(attachmentPart);
                }
            }

            if (files.Count > 0)
            {
                var multipart = new Multipart("mixed");
                multipart.Add(html);
                foreach (var file in files)
                    multipart.Add(file);
                message.Body = multipart;
            }
            else
            {
                message.Body = html;
            }

            using var client = new MailKit.Net.Smtp.SmtpClient();

            // Accept all certificates (for testing purposes only)
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await client.ConnectAsync(
                emailOpts.SmtpServer,
                emailOpts.Port,
                emailOpts.EnableSsl,
                cancellationToken);

            await client.AuthenticateAsync(emailOpts.Username, emailOpts.Password, cancellationToken);

            await client.SendAsync(message, cancellationToken);

            await client.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error sending email.");
            logger.LogWarning("Error with options: {0}", options.Value);
        }
    }
}