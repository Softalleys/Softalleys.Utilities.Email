using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Softalleys.Utilities.Email.Services.FluidRender;
using Softalleys.Utilities.Email.Services.SimulatedEmail;
using Softalleys.Utilities.Email.Services.SmtpEmail;

namespace Softalleys.Utilities.Email.Dependencies;

/// <summary>
/// Provides extension methods to register email-related services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the <see cref="SmtpEmailService"/> and configures <see cref="SmtpEmailOptions"/> from the specified configuration.
    /// </summary>
    /// <param name="services">The service collection where services are registered.</param>
    /// <param name="configuration">The configuration object that provides settings.</param>
    /// <param name="sectionName">The name of the configuration section to bind to <see cref="SmtpEmailOptions"/>. Defaults to "SmtpEmailOptions".</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddSmtpEmail(this IServiceCollection services, IConfiguration configuration,
        string sectionName = "SmtpEmailOptions")
    {
        services.AddSingleton<IEmailService, SmtpEmailService>();
        services.Configure<SmtpEmailOptions>(configuration.GetSection(sectionName));
        return services;
    }

    /// <summary>
    /// Registers the <see cref="SmtpEmailService"/> and configures <see cref="SmtpEmailOptions"/> from the specified configuration.
    /// Also allows additional custom configuration via <paramref name="configureOptions"/>.
    /// </summary>
    /// <param name="services">The service collection where services are registered.</param>
    /// <param name="configuration">The configuration object that provides settings.</param>
    /// <param name="configureOptions">An action used to post-configure <see cref="SmtpEmailOptions"/>.</param>
    /// <param name="sectionName">The name of the configuration section to bind to <see cref="SmtpEmailOptions"/>. Defaults to "SmtpEmailOptions".</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddSmtpEmail(this IServiceCollection services, IConfiguration configuration,
        Action<SmtpEmailOptions> configureOptions, string sectionName = "SmtpEmailOptions")
    {
        services.AddSingleton<IEmailService, SmtpEmailService>();
        services.Configure<SmtpEmailOptions>(configuration.GetSection(sectionName));

        services.PostConfigure(configureOptions);

        return services;
    }

    /// <summary>
    /// Registers the <see cref="SmtpEmailService"/> and configures <see cref="SmtpEmailOptions"/> either from the supplied
    /// <paramref name="configuration"/> or from the provided <paramref name="configureOptions"/> if no configuration is given.
    /// </summary>
    /// <param name="services">The service collection where services are registered.</param>
    /// <param name="configureOptions">An action used to configure <see cref="SmtpEmailOptions"/>.</param>
    /// <param name="configuration">An optional configuration object that provides settings.</param>
    /// <param name="sectionName">The name of the configuration section to bind to <see cref="SmtpEmailOptions"/>. Defaults to "SmtpEmailOptions".</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddSmtpEmail(this IServiceCollection services,
        Action<SmtpEmailOptions> configureOptions, IConfiguration? configuration = null,
        string sectionName = "SmtpEmailOptions")
    {
        services.AddSingleton<IEmailService, SmtpEmailService>();

        if (configuration != null)
        {
            services.Configure<SmtpEmailOptions>(configuration.GetSection(sectionName));

            services.PostConfigure(configureOptions);
        }
        else
            services.Configure(configureOptions);

        return services;
    }

    /// <summary>
    /// Registers the <see cref="SimulatedEmailService"/> for email simulation.
    /// </summary>
    /// <param name="services">The service collection where services are registered.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddSimulatedEmail(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, SimulatedEmailService>();
        services.AddOptions<SmtpEmailOptions>();
        return services;
    }

    /// <summary>
    /// Registers the <see cref="FluidRenderService"/> for HTML rendering using the Fluid language.
    /// Fluid is a .NET template engine that is used to render HTML templates with data.
    /// For more information, visit the Fluid documentation at https://github.com/sebastienros/fluid.
    /// </summary>
    /// <param name="services">The service collection where services are registered.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddFluidHtmlViewRender(this IServiceCollection services)
    {
        services.AddSingleton<IHtmlViewRenderService, FluidRenderService>();
        return services;
    }
}