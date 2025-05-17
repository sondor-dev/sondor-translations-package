using System.Globalization;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sondor.Options.Extensions;
using Sondor.Translations.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace Sondor.Translations.Extensions;

/// <summary>
/// Collection of <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Sondor translations to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="resource">The resource.</param>
    /// <param name="settings">The settings section.</param>
    /// <returns>Returns the service collection.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when an invalid argument is provided.</exception>
    /// <exception cref="ArgumentNullException">This exception is thrown when an invalid argument is provided.</exception>
    /// <exception cref="ValidationException">This exception is thrown when the configured options fail validation.</exception>
    public static IServiceCollection AddSondorTranslations(this IServiceCollection services,
        string resource = TranslationConstants.DefaultResourceName,
        string settings = nameof(SondorTranslationOptions))
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resource, nameof(resource));
        ArgumentException.ThrowIfNullOrWhiteSpace(settings, nameof(settings));

        services.AddSondorOptions<SondorTranslationOptions>(settings);

        var provider = services.BuildServiceProvider();
        var translationOptions = provider.GetRequiredService<IOptions<SondorTranslationOptions>>().Value;

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(translationOptions.DefaultCulture);
            options.SupportedCultures = translationOptions.SupportedCultures
                .Select(culture => new CultureInfo(culture))
                .ToList();
        });

        services.AddLocalization(options =>
        {
            options.ResourcesPath = resource;
        });

        services.AddScoped<ISondorTranslationManager, SondorTranslationManager>(serviceProvider =>
        {
            var localizerFactory = serviceProvider.GetRequiredService<IStringLocalizerFactory>();

            return new SondorTranslationManager(localizerFactory);
        });

        return services;
    }
}