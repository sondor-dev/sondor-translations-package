using System.Text.Json;
using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;
using Sondor.Translations.Extensions;
using Sondor.Translations.Options;
using Sondor.Translations.Providers;

namespace Sondor.Translations.Tests.Providers;

/// <summary>
/// Tests for <see cref="JsonFileTranslationProvider"/>."/>
/// </summary>
[TestFixture]
public class JsonFileTranslationProviderTests
{
    /// <summary>
    /// Ensures the <see cref="JsonFileTranslationProvider.ReadAsync"/> throws <see cref="SondorTranslationFileMissingDefaultCultureException"/> when missing translations for the default culture.
    /// </summary>
    [Test]
    public void TranslateAsyncMissingDefaultCulture()
    {
        // arrange
        const string key = "key-1";
        var translations = DefaultConstants.DefaultTranslations;
        translations.Add("fr-FR", DefaultConstants.DefaultTranslations[OptionsConstants.DefaultCulture]);
        translations.Remove(OptionsConstants.DefaultCulture);
        var json = JsonSerializer.Serialize(translations);

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

        // act && assert
        Assert.ThrowsAsync<SondorTranslationFileMissingDefaultCultureException>(() =>
            translationManager.TranslateAsync(key));
    }

    /// <summary>
    /// Ensures the <see cref="JsonFileTranslationProvider.ReadAsync"/> returns the correct value.
    /// </summary>
    [Test]
    public void TranslateAsyncNoCultures()
    {
        // arrange
        const string key = "key-1";

        var translations = DefaultConstants.DefaultTranslations;
        translations.Remove(OptionsConstants.DefaultCulture);

        var json = JsonSerializer.Serialize(translations);
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

        // act && assert
        Assert.ThrowsAsync<SondorNoTranslationCulturesException>(() =>
            translationManager.TranslateAsync(key));
    }

    /// <summary>
    /// Ensures the <see cref="JsonFileTranslationProvider.ReadAsync"/> returns the correct value.
    /// </summary>
    [Test]
    public void TranslateAsyncWhitespace()
    {
        // arrange
        const string key = "key-1";

        var tmpJsonFile = SondorTranslationTests.CreateTmpJsonTranslationFile("           ");
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

        // act && assert
        Assert.ThrowsAsync<SondorEmptyTranslationFileException>(() =>
            translationManager.TranslateAsync(key));
    }
}