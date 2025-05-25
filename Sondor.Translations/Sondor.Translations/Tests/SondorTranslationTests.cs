using Microsoft.Extensions.Configuration;
using Sondor.Translations.Constants;
using Sondor.Translations.Options;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Sondor.Translations.Extensions;

namespace Sondor.Translations.Tests;

/// <summary>
/// Collection of test helpers for testing Sondor Translations.
/// </summary>
public static class SondorTranslationTests
{
    /// <summary>
    /// Creates an instance of <see cref="ISondorTranslationManager"/>.
    /// </summary>
    /// <param name="settings">The settings section.</param>
    /// <param name="providers">The providers.</param>
    /// <param name="services">The services.</param>
    /// <param name="translationOptions">The translation options.</param>
    /// <returns>Returns the translation manager.</returns>
    public static ISondorTranslationManager CreateTranslationManager(
        SondorTranslationOptions? translationOptions = null,
        string settings = nameof(SondorTranslationOptions),
        IServiceCollection? services = null,
        IEnumerable<ISondorTranslationProvider>? providers = null
    )
    {
        services = CreateTranslationServices(translationOptions,
            settings,
            services,
            providers);
        var serviceProvider = services.BuildServiceProvider();
        var translationManager = serviceProvider.GetRequiredService<ISondorTranslationManager>();

        return translationManager;
    }

    /// <summary>
    /// Creates an instance of <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="settings">The settings section.</param>
    /// <param name="providers">The providers.</param>
    /// <param name="services">The services.</param>
    /// <param name="translationOptions">The translation options.</param>
    /// <returns>Returns the translation manager.</returns>
    public static IServiceCollection CreateTranslationServices(
        SondorTranslationOptions? translationOptions = null,
        string settings = nameof(SondorTranslationOptions),
        IServiceCollection? services = null,
        IEnumerable<ISondorTranslationProvider>? providers = null
    )
    {
        translationOptions ??= OptionsConstants.DefaultTranslationOptions;
        var json = JsonSerializer.Serialize(new
        {
            SondorTranslationOptions = translationOptions
        });
        var tmpFilename = Path.GetTempFileName();

        Path.ChangeExtension(tmpFilename, ".json");

        var tempJsonFileName = tmpFilename + ".json";

        File.WriteAllText(tempJsonFileName, json);

        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.Sources.Clear();
        configurationBuilder.AddJsonFile(tempJsonFileName);

        services ??= new ServiceCollection();
        services.AddSingleton<IConfiguration>(configurationBuilder.Build());
        services.AddSerilog();

        services.AddSondorTranslations(settings,
            providers);

        File.Delete(tempJsonFileName);

        return services;
    }

    /// <summary>
    /// Create a temporary JSON translation file.
    /// </summary>
    /// <param name="json">The JSON to write.</param>
    /// <returns>Returns the temporary JSON translation file.</returns>
    public static FileInfo CreateTmpJsonTranslationFile(string? json = null)
    {
        var tempFileName = Path.GetTempFileName();
        var jsonFileName = $"{tempFileName}.json";

        var jsonFile = new FileInfo(jsonFileName);

        Path.ChangeExtension(tempFileName, ".json");

        File.WriteAllText(jsonFileName, json);

        return jsonFile;
    }
}