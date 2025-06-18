using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sondor.Translations.Extensions;

/// <summary>
/// Collection of <see cref="IApplicationBuilder"/> extensions.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Use Sondor translations for request localization.
    /// </summary>
    /// <param name="application">The application.</param>
    /// <param name="services">The service collection.</param>
    /// <returns>Returns the web application.</returns>
    public static IApplicationBuilder UseSondorTranslations(this IApplicationBuilder application,
        IServiceProvider services)
    {
        var localizeOptions = services.GetRequiredService<IOptions<RequestLocalizationOptions>>();

        application.UseRequestLocalization(localizeOptions.Value);

        return application;
    }
}