# Softalleys.Utilities.Email

A comprehensive email sending utility for .NET 8 and .NET 9 applications. This package provides a simple interface for sending emails via SMTP, rendering HTML templates using the Fluid templating engine, and simulating email sending for testing purposes.

## Installation

Install the package via NuGet:

```shell
dotnet add package Softalleys.Utilities.Email
```

## Configuration

### Setting Up Dependency Injection

Register the required services in your `Program.cs` or startup class:

```csharp
// For SMTP email sending
builder.Services.AddSmtpEmail(builder.Configuration);

// For HTML template rendering with Fluid
builder.Services.AddFluidHtmlViewRender();

// For email simulation (useful for testing)
// builder.Services.AddSimulatedEmail(); // Uncomment to use simulated email instead of real SMTP
```

### Configuration Options

You can configure the SMTP settings in your `appsettings.json`:

```json
{
  "SmtpEmailOptions": {
    "SmtpServer": "smtp.example.com",
    "Port": 465,
    "Username": "your-username",
    "Password": "your-password",
    "EnableSsl": true,
    "FromAddress": "noreply@yourdomain.com",
    "FromName": "Your Application Name"
  }
}
```

#### Alternative Configuration Methods

**Environment Variables:**

```
SmtpEmailOptions__SmtpServer=smtp.example.com
SmtpEmailOptions__Port=465
SmtpEmailOptions__Username=your-username
SmtpEmailOptions__Password=your-password
SmtpEmailOptions__EnableSsl=true
SmtpEmailOptions__FromAddress=noreply@yourdomain.com
SmtpEmailOptions__FromName=Your Application Name
```

**Direct Configuration in Code:**

```csharp
builder.Services.AddSmtpEmail(options =>
{
    options.SmtpServer = "smtp.example.com";
    options.Port = 465;
    options.Username = "your-username";
    options.Password = "your-password";
    options.EnableSsl = true;
    options.FromAddress = "noreply@yourdomain.com";
    options.FromName = "Your Application Name";
});
```

## Usage

### Injecting Services

Inject the services into your classes:

```csharp
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IHtmlViewRenderService _htmlRenderer;

    public EmailController(IEmailService emailService, IHtmlViewRenderService htmlRenderer)
    {
        _emailService = emailService;
        _htmlRenderer = htmlRenderer;
    }
    
    // Controller actions...
}
```

### Rendering HTML Templates with Fluid

```csharp
// Define your view model
var viewModel = new 
{
    UserName = "John Doe",
    ActivationLink = "https://example.com/activate?token=abc123"
};

// HTML template using Fluid syntax
string htmlTemplate = @"
<!DOCTYPE html>
<html>
<head>
    <title>Welcome to Our Service</title>
</head>
<body>
    <h1>Hello {{ UserName }}!</h1>
    <p>Thank you for registering. Please click the link below to activate your account:</p>
    <a href='{{ ActivationLink }}'>Activate Account</a>
</body>
</html>
";

// Render the HTML
string renderedHtml = await _htmlRenderer.RenderHtmlAsync(htmlTemplate, viewModel);
```

For more information about Fluid templating syntax, visit [the Fluid documentation](https://github.com/sebastienros/fluid).

### Sending Emails

```csharp
public async Task<IActionResult> SendWelcomeEmail(string email, string name)
{
    // Create view model
    var viewModel = new 
    {
        UserName = name,
        CurrentYear = DateTime.Now.Year
    };
    
    // Render email template
    string htmlTemplate = @"
        <h1>Welcome {{ UserName }}!</h1>
        <p>Thank you for joining our platform.</p>
        <footer>&copy; {{ CurrentYear }} Our Company</footer>
    ";
    
    string renderedHtml = await _htmlRenderer.RenderHtmlAsync(htmlTemplate, viewModel);
    
    // Create email message
    var emailMessage = new EmailMessageRequested(
        Subject: "Welcome to Our Platform",
        BodyTemplate: renderedHtml,
        Recipients: new[] { new EmailAddress(email, name) }
    );
    
    // Send the email
    await _emailService.SendEmailAsync(emailMessage);
    
    return Ok("Email sent successfully");
}
```

### Using the Simulated Email Service for Testing

For testing environments, use the `SimulatedEmailService` to avoid sending real emails:

```csharp
builder.Services.AddSimulatedEmail();
```

The simulated service will log the email details and content to the console instead of sending actual emails, which is useful for development and testing environments.

## SMTP Email Options

| Property | Description | Default |
|----------|-------------|---------|
| SmtpServer | SMTP server address | smtp.example.com |
| Port | SMTP server port | 465 |
| Username | Authentication username | username |
| Password | Authentication password | password |
| EnableSsl | Whether to use SSL | true |
| FromAddress | Default sender email address | dont-reply@example.com |
| FromName | Default sender name | Email Service |

## Email Message Properties

| Property | Description | Required |
|----------|-------------|----------|
| Subject | Email subject line | Yes |
| BodyTemplate | HTML content of the email | Yes |
| Recipients | Array of email recipients | Yes |
| CarbonCopyRecipients | Array of CC recipients | No |
| BlindCarbonCopyRecipients | Array of BCC recipients | No |
| Attachments | Array of email attachments | No |