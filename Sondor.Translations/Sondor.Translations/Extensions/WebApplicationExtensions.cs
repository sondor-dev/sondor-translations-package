using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sondor.Translations.Extensions;

/// <summary>
/// Collection of <see cref="WebApplication"/> extensions.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Use Sondor translations for request localization.
    /// </summary>
    /// <param name="application">The application.</param>
    /// <returns>Returns the web application.</returns>
    public static WebApplication UseSondorTranslations(this WebApplication application)
    {
        var localizeOptions = application.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();

        application.UseRequestLocalization(localizeOptions.Value);

        return application;
    }
}