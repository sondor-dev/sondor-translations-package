using System.Globalization;
using System.Text.Json;
using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;
using Sondor.Translations.Extensions;
using Sondor.Translations.Options;
using Sondor.Translations.Tests.Args;

namespace Sondor.Translations.Tests;

/// <summary>
/// Tests for <see cref="SondorTranslationManager"/>.
/// </summary>
[TestFixture]
public class SondorTranslationManagerTests
{
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

        var translationManager = SondorTranslationTests.CreateTranslationManager();

        // act
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        // assert
        if (value is null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                translationManager.Translate(value!, location, resource));

            return;
        }

        Assert.Throws<ArgumentException>(() =>
            translationManager.Translate(value, location, resource));
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

        var translationManager = SondorTranslationTests.CreateTranslationManager();

        // act
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        // assert
        if (value is null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                translationManager.Translate(key, value!, resource));

            return;
        }

        Assert.Throws<ArgumentException>(() =>
            translationManager.Translate(key, value, resource));
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
        
        var translationManager = SondorTranslationTests.CreateTranslationManager();

        // act
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        // assert
        if (value is null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                translationManager.Translate(key, location, value!));

            return;
        }

        Assert.Throws<ArgumentException>(() =>
            translationManager.Translate(key, location, value));
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

        var translationOptions = new SondorTranslationOptions()
        {
            DefaultCulture  = OptionsConstants.DefaultCulture,
            SupportedCultures = OptionsConstants.DefaultSupportedCultures,
            UseKeyAsDefaultValue = false
        };
        var translationManager = SondorTranslationTests.CreateTranslationManager(translationOptions);

        // act && assert
        Assert.Throws<SondorTranslationNotFoundException>(() =>
            translationManager.Translate(key, location, resource));
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
        const string resource = "Resources.Tests";
        const string expected = "test-key";

        var translationManager = SondorTranslationTests.CreateTranslationManager();

        // act
        var value = translationManager.Translate(key, location, resource);

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

        var translationManager = SondorTranslationTests.CreateTranslationManager();

        // act
        var value = translationManager.Translate(key, location, resource, defaultValue);

        // assert
        Assert.That(value, Is.EqualTo(defaultValue));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> returns the expected translation when the translation is not found and a default value is provided.
    /// </summary>
    [Test]
    public void TranslateUseKeyAsDefault()
    {
        // arrange
        const string key = "missing-key";
        const string location = "Sondor.Translations.Tests";
        const string resource = "Resources";

        var translationOptions = new SondorTranslationOptions()
        {
            DefaultCulture = OptionsConstants.DefaultCulture,
            SupportedCultures = OptionsConstants.DefaultSupportedCultures,
            UseKeyAsDefaultValue = OptionsConstants.DefaultUseKeyAsDefaultValue
        };
        var translationManager = SondorTranslationTests.CreateTranslationManager(translationOptions);

        // act
        var value = translationManager.Translate(key, location, resource);

        // assert
        Assert.That(value, Is.EqualTo(key));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> formats the value with the provided parameters.
    /// </summary>
    [Test]
    public void TranslateNotFoundParameters()
    {
        // arrange
        const string key = "key-invalid";
        const string replace = "value";
        const string location = "Sondor.Translations.Tests";
        const string resource = "Resources.en";
        const string defaultValue = "value-is-{0}";

        var expected = string.Format(defaultValue, replace);
        var translationManager = SondorTranslationTests.CreateTranslationManager();

        // act
        var value = translationManager.Translate(key, location, resource, defaultValue, replace);

        // assert
        Assert.That(value, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.Translate"/> formats the value with the provided parameters.
    /// </summary>
    [Test]
    public void TranslateParameters()
    {
        // arrange
        const string key = "key-replace";
        const string replace = "value";
        const string location = "Sondor.Translations.Tests";
        const string resource = "Resources.Tests";
        const string defaultValue = "value-is-{0}";

        var expected = string.Format(defaultValue, replace);
        var translationManager = SondorTranslationTests.CreateTranslationManager();

        // act
        var value = translationManager.Translate(key, location, resource, defaultValue, replace);

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
        // arrange
        var translationManager = SondorTranslationTests.CreateTranslationManager();

        // act
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        // assert
        if (value is null)
        {
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                translationManager.TranslateAsync(value!));

            return;
        }

        Assert.ThrowsAsync<ArgumentException>(() =>
            translationManager.TranslateAsync(value));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.TranslateAsync"/> throws <see cref="SondorTranslationNoProvidersException"/> when no providers are registered.
    /// </summary>
    [Test]
    public void TranslateAsyncNoProviders()
    {
        // arrange
        const string key = "key";

        var translationManager = SondorTranslationTests.CreateTranslationManager();

        // act && assert
        Assert.ThrowsAsync<SondorTranslationNoProvidersException>(() =>
            translationManager.TranslateAsync(key));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.TranslateAsync"/> throws <see cref="SondorProviderTranslationNotFoundException"/> when no providers are registered.
    /// </summary>
    [Test]
    public void TranslateAsyncNotFoundExceptions()
    {
        // arrange
        const string key = "key-invalid";

        var json = JsonSerializer.Serialize(DefaultConstants.DefaultTranslations);
        var tmpJsonFile = SondorTranslationTests.CreateTmpJsonTranslationFile(json);
        var translationOptions = new SondorTranslationOptions()
        {
            DefaultCulture = OptionsConstants.DefaultCulture,
            SupportedCultures = OptionsConstants.DefaultSupportedCultures,
            UseKeyAsDefaultValue = false
        };

        var services = SondorTranslationTests.CreateTranslationServices(translationOptions);
        var jsonFileProvider = services.LoadJsonFileTranslationProvider(tmpJsonFile);

        var translationManager =
            SondorTranslationTests.CreateTranslationManager(translationOptions, providers: [jsonFileProvider]);

        // act && assert
        Assert.ThrowsAsync<SondorProviderTranslationNotFoundException>(() =>
            translationManager.TranslateAsync(key));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.TranslateAsync"/> returns the default value when the translation is not found and a default value is provided.
    /// </summary>
    [Test]
    public async Task TranslateAsyncDefaultValue()
    {
        // arrange
        const string key = "key-invalid";
        const string defaultValue = "default-value";

        var json = JsonSerializer.Serialize(DefaultConstants.DefaultTranslations);
        var tmpJsonFile = SondorTranslationTests.CreateTmpJsonTranslationFile(json);
        var translationOptions = new SondorTranslationOptions()
        {
            DefaultCulture = OptionsConstants.DefaultCulture,
            SupportedCultures = OptionsConstants.DefaultSupportedCultures,
            UseKeyAsDefaultValue = false
        };

        var services = SondorTranslationTests.CreateTranslationServices(translationOptions);
        var jsonFileProvider = services.LoadJsonFileTranslationProvider(tmpJsonFile);

        var translationManager =
            SondorTranslationTests.CreateTranslationManager(translationOptions, providers: [jsonFileProvider]);

        // act
        var translation = await translationManager.TranslateAsync(key, defaultValue);

        // assert
        Assert.That(translation, Is.EqualTo(defaultValue));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.TranslateAsync"/> returns the key when the translation is not found and the key is used as the default value.
    /// </summary>
    [Test]
    public async Task TranslateAsyncKey()
    {
        // arrange
        const string key = "key-invalid";

        var json = JsonSerializer.Serialize(DefaultConstants.DefaultTranslations);
        var tmpJsonFile = SondorTranslationTests.CreateTmpJsonTranslationFile(json);
        var translationOptions = new SondorTranslationOptions()
        {
            DefaultCulture = OptionsConstants.DefaultCulture,
            SupportedCultures = OptionsConstants.DefaultSupportedCultures,
            UseKeyAsDefaultValue = true
        };

        var services = SondorTranslationTests.CreateTranslationServices(translationOptions);
        var jsonFileProvider = services.LoadJsonFileTranslationProvider(tmpJsonFile);

        var translationManager =
            SondorTranslationTests.CreateTranslationManager(translationOptions, providers: [jsonFileProvider]);

        // act
        var translation = await translationManager.TranslateAsync(key);

        // assert
        Assert.That(translation, Is.EqualTo(key));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.TranslateAsync"/> returns the correct value.
    /// </summary>
    [Test]
    public async Task TranslateAsync()
    {
        // arrange
        const string key = "key-1";
        const string expected = "value-1";

        var json = JsonSerializer.Serialize(DefaultConstants.DefaultTranslations);
        var tmpJsonFile = SondorTranslationTests.CreateTmpJsonTranslationFile(json);
        var translationOptions = new SondorTranslationOptions()
        {
            DefaultCulture = OptionsConstants.DefaultCulture,
            SupportedCultures = OptionsConstants.DefaultSupportedCultures,
            UseKeyAsDefaultValue = true
        };

        var services = SondorTranslationTests.CreateTranslationServices(translationOptions);
        var jsonFileProvider = services.LoadJsonFileTranslationProvider(tmpJsonFile);

        var translationManager =
            SondorTranslationTests.CreateTranslationManager(translationOptions, providers: [jsonFileProvider]);

        // act
        var translation = await translationManager.TranslateAsync(key);

        // assert
        Assert.That(translation, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures the <see cref="SondorTranslationManager.TranslateAsync"/> returns the correct value.
    /// </summary>
    [Test]
    public async Task TranslateAsyncFallbackToDefaultCulture()
    {
        // arrange
        const string key = "key-1";
        const string expected = "value-1";

        var frenchCulture = new CultureInfo("fr-FR");

        var json = JsonSerializer.Serialize(DefaultConstants.DefaultTranslations);
        var tmpJsonFile = SondorTranslationTests.CreateTmpJsonTranslationFile(json);
        var translationOptions = new SondorTranslationOptions
        {
            DefaultCulture = OptionsConstants.DefaultCulture,
            SupportedCultures = OptionsConstants.DefaultSupportedCultures,
            UseKeyAsDefaultValue = true
        };

        var services = SondorTranslationTests.CreateTranslationServices(translationOptions);
        var jsonFileProvider = services.LoadJsonFileTranslationProvider(tmpJsonFile);

        var translationManager =
            SondorTranslationTests.CreateTranslationManager(translationOptions, providers: [jsonFileProvider]);

        CultureInfo.CurrentCulture = frenchCulture;
        CultureInfo.CurrentUICulture = frenchCulture;

        // act
        var translation = await translationManager.TranslateAsync(key);

        // assert
        Assert.That(translation, Is.EqualTo(expected));
    }
}