using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sondor.Translations.Extensions;
using Sondor.Translations.Tests.Args;

namespace Sondor.Translations.Tests;

/// <summary>
/// Tests for <see cref="SondorTranslationManager"/>.
/// </summary>
[TestFixture]
public class SondorTranslationManagerTests
{
    /// <summary>
    /// The translation manager.
    /// </summary>
    private readonly ISondorTranslationManager _translationManager;

    /// <summary>
    /// Creates a new instance of <see cref="SondorTranslationManagerTests"/>.
    /// </summary>
    public SondorTranslationManagerTests()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.Sources.Clear();
        configurationBuilder.AddInMemoryCollection([
            new KeyValuePair<string, string?>($"{nameof(SondorTranslationOptions)}:{nameof(SondorTranslationOptions.DefaultCulture)}", "en-GB"),
            new KeyValuePair<string, string?>($"{nameof(SondorTranslationOptions)}:{nameof(SondorTranslationOptions.SupportedCultures)}", JsonConvert.SerializeObject(new []
            {
                "en-GB",
                "fr-FR"
            }))
        ]);
        var configuration = configurationBuilder.Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddValidatorsFromAssembly(typeof(SondorTranslationOptions).Assembly);
        services.AddSondorTranslations();

        var serviceProvider = services.BuildServiceProvider();
        _translationManager = serviceProvider.GetRequiredService<ISondorTranslationManager>();
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> throws the expected exceptions when an invalid value is provided to the key parameter.
    /// </summary>
    /// <param name="value">The value.</param>
    [TestCaseSource(typeof(StringArgs))]
    public void TranslateKeyExceptions(string? value)
    {
        // arrange
        const string resource = "resource";
        const string location = "location";

        // act
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (value is null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                _translationManager.Translate(value!, location, resource));

            return;
        }

        Assert.Throws<ArgumentException>(() =>
            _translationManager.Translate(value!, location, resource));
    }
}