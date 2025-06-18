using System.Globalization;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sondor.Options.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Sondor.Translations.Constants;
using Sondor.Translations.Options;
using Sondor.Translations.Providers;

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
    /// <param name="settings">The settings section.</param>
    /// <param name="providers">The translation providers.</param>
    /// <param name="requestCultureProviders">The request culture providers.</param>
    /// <returns>Returns the service collection.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when an invalid argument is provided.</exception>
    /// <exception cref="ArgumentNullException">This exception is thrown when an invalid argument is provided.</exception>
    /// <exception cref="ValidationException">This exception is thrown when the configured options fail validation.</exception>
    public static IServiceCollection AddSondorTranslations(this IServiceCollection services,
        string settings = nameof(SondorTranslationOptions),
        IEnumerable<ISondorTranslationProvider>? providers = null,
        IList<IRequestCultureProvider>? requestCultureProviders = null)
    {
        if (settings is null)
        {
            throw new ArgumentNullException(nameof(settings), "Settings cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(settings))
        {
            throw new ArgumentException("Settings cannot be null or whitespace.", nameof(settings));
        }

        services.AddSondorOptions<SondorTranslationOptions>(settings);

        var provider = services.BuildServiceProvider();
        var translationOptions = provider.GetRequiredService<IOptions<SondorTranslationOptions>>();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(translationOptions.Value.DefaultCulture);

            options.SupportedUICultures = translationOptions.Value.SupportedCultures
                .Select(culture => new CultureInfo(culture))
                .ToList();

            options.SupportedCultures = translationOptions.Value.SupportedCultures
                .Select(culture => new CultureInfo(culture))
                .ToList();

            options.RequestCultureProviders = requestCultureProviders ?? DefaultConstants.DefaultRequestCultureProviders;
        });

        services.AddLocalization();

        services.AddTransient<ISondorTranslationManager, SondorTranslationManager>(serviceProvider =>
        {
            var localizerFactory = serviceProvider.GetRequiredService<IStringLocalizerFactory>();

            return new SondorTranslationManager(translationOptions,
                localizerFactory,
                providers);
        });

        CultureInfo.CurrentCulture = new CultureInfo(translationOptions.Value.DefaultCulture);
        CultureInfo.CurrentUICulture = new CultureInfo(translationOptions.Value.DefaultCulture);

        return services;
    }

    /// <summary>
    /// Loads a JSON file translation provider.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="translationFile">The translation file.</param>
    /// <returns>Returns the translation provider.</returns>
    /// <exception cref="InvalidOperationException">This exception is thrown when a required service has not been registered to the provided <paramref name="services"/>.</exception>
    public static JsonFileTranslationProvider LoadJsonFileTranslationProvider(this IServiceCollection services,
        FileInfo translationFile)
    {
        var provider = services.BuildServiceProvider();
        var localizationOptions = provider.GetRequiredService<IOptions<RequestLocalizationOptions>>();

        return new JsonFileTranslationProvider(translationFile,
            localizationOptions);
    }
}