using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Sondor.Translations.Exceptons;
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
    /// The services.
    /// </summary>
    private readonly IServiceCollection _services;

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
        configurationBuilder.AddJsonFile("appsettings.json");

        var configuration = configurationBuilder.Build();

        _services = new ServiceCollection();
        _services.AddSerilog(config =>
        {
            config
                .MinimumLevel.Debug()
                .WriteTo.Console();
        });
        _services.AddSingleton<IConfiguration>(configuration);
        _services.AddSondorTranslations();

        var serviceProvider = _services.BuildServiceProvider();
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

        // assert
        if (value is null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                _translationManager.Translate(value!, location, resource));

            return;
        }

        Assert.Throws<ArgumentException>(() =>
            _translationManager.Translate(value, location, resource));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> throws the expected exceptions when an invalid value is provided to the location parameter.
    /// </summary>
    /// <param name="value">The value.</param>
    [TestCaseSource(typeof(StringArgs))]
    public void TranslateLocationExceptions(string? value)
    {
        // arrange
        const string key = "key";
        const string resource = "resource";

        // act
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        // assert
        if (value is null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                _translationManager.Translate(key, value!, resource));

            return;
        }

        Assert.Throws<ArgumentException>(() =>
            _translationManager.Translate(key, value, resource));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> throws the expected exceptions when an invalid value is provided to the resource parameter.
    /// </summary>
    /// <param name="value">The value.</param>
    [TestCaseSource(typeof(StringArgs))]
    public void TranslateResourceExceptions(string? value)
    {
        // arrange
        const string key = "key";
        const string location = "location";

        // act
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        // assert
        if (value is null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                _translationManager.Translate(key, location, value!));

            return;
        }

        Assert.Throws<ArgumentException>(() =>
            _translationManager.Translate(key, location, value));
    }

    /// <summary>
    /// Ensures that <see cref="SondorTranslationManager.Translate"/> sets up <see cref="RequestLocalizationOptions"/> correctly.
    /// </summary>
    [Test]
    public void ValidateRequestLocalizationOptions()
    {
        // arrange
        var supported = new [] { "en", "fr" };
        var serviceProvider = _services.BuildServiceProvider();

        // act
        var options = serviceProvider.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
        var supportedCultures = options.SupportedCultures?.Select(current => current.Name) ?? [];
        var supportedUiCultures = options.SupportedUICultures?.Select(current => current.Name) ?? [];

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(options.DefaultRequestCulture.Culture.Name, Is.EqualTo("en"));
            Assert.That(supportedCultures, Is.EqualTo(supported));
            Assert.That(supportedUiCultures, Is.EqualTo(supported));
        });
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> throws <see cref="SondorTranslationNotFoundException"/> when the translation is not found.
    /// </summary>
    [Test]
    public void TranslateNotFoundExceptions()
    {
        // arrange
        const string key = "missing-key";
        const string location = "Sondor.Translations.Tests";
        const string resource = "Resources";

        // act && assert
        Assert.Throws<SondorTranslationNotFoundException>(() =>
            _translationManager.Translate(key, location, resource));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> finds the expected translation when the translation is found.
    /// </summary>
    [Test]
    public void Translate()
    {
        // arrange
        const string key = "key";
        const string location = "Sondor.Translations.Tests";
        const string resource = "Resources.en";
        const string expected = "test-key";

        // act
        var value = _translationManager.Translate(key, location, resource);

        // assert
        Assert.That(value, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> returns the expected translation when the translation is not found and a default value is provided.
    /// </summary>
    [Test]
    public void TranslateDefaultValue()
    {
        // arrange
        const string key = "key-invalid";
        const string defaultValue = "default-value";
        const string location = "Sondor.Translations.Tests";
        const string resource = "Resources.en";

        // act
        var value = _translationManager.Translate(key, location, resource, defaultValue);

        // assert
        Assert.That(value, Is.EqualTo(defaultValue));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> formats the value with the provided parameters.
    /// </summary>
    [Test]
    public void TranslateParameters()
    {
        // arrange
        const string key = "key-invalid";
        const string replace = "value";
        const string location = "Sondor.Translations.Tests";
        const string resource = "Resources.en";
        const string defaultValue = "value-is-{0}";
        var expected = string.Format(defaultValue, replace);

        // act
        var value = _translationManager.Translate(key, location, resource, defaultValue, replace);

        // assert
        Assert.That(value, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> throws the expected exceptions when an invalid value is provided to the key parameter.
    /// </summary>
    /// <param name="value">The value.</param>
    [TestCaseSource(typeof(StringArgs))]
    public void TranslateAsyncKeyExceptions(string? value)
    {
        // act
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        // assert
        if (value is null)
        {
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                _translationManager.TranslateAsync(value!));

            return;
        }

        Assert.ThrowsAsync<ArgumentException>(() =>
            _translationManager.TranslateAsync(value));
    }
}