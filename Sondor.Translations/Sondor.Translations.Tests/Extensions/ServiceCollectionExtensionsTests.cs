using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Sondor.Tests.Args;
using Sondor.Translations.Constants;
using Sondor.Translations.Extensions;

namespace Sondor.Translations.Tests.Extensions;

/// <summary>
/// Tests for <see cref="ServiceCollectionExtensions"/>.
/// </summary>
[TestFixture]
public class ServiceCollectionExtensionsTests
{
    /// <summary>
    /// Ensures that <see cref="ServiceCollectionExtensions.AddSondorTranslations"/> throws an exception when the settings section is null.
    /// </summary>
    [TestCaseSource(typeof(StringArgs))]
    public void AddSondorTranslations_throws_exception(string? value)
    {
        // arrange
        var services = new ServiceCollection();

        // act & assert
        if (!string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (value is null)
        {
            Assert.Throws<ArgumentNullException>(() => services.AddSondorTranslations(value!));

            return;
        }

        Assert.Throws<ArgumentException>(() => services.AddSondorTranslations(value));
    }

    /// <summary>
    /// Ensures that <see cref="ServiceCollectionExtensions.AddSondorTranslations"/> 
    /// adds <see cref="LocalizationOptions"/> to the service collection.
    /// </summary>
    [Test]
    public void AddSondorTranslations_LocalizationOptions()
    {
        // arrange
        var services = SondorTranslationTests.CreateTranslationServices();
        var provider = services.BuildServiceProvider();

        // act
        var localizationOptions = provider.GetRequiredService<IOptions<LocalizationOptions>>();

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(localizationOptions, Is.Not.Null);
            Assert.That(localizationOptions.Value.ResourcesPath, Is.Empty);
        });
    }

    /// <summary>
    /// Ensures that <see cref="ServiceCollectionExtensions.AddSondorTranslations"/> 
    /// adds <see cref="RequestLocalizationOptions"/> to the service collection.
    /// </summary>
    [Test]
    public void AddSondorTranslations_RequestLocalizationOptions()
    {
        // arrange
        var services = SondorTranslationTests.CreateTranslationServices();
        var provider = services.BuildServiceProvider();

        // act
        var requestLocalizationOptions = provider.GetRequiredService<IOptions<RequestLocalizationOptions>>();
        var supportedCultures = requestLocalizationOptions.Value.SupportedCultures?.Select(current => current.Name) ?? [];
        var supportedUiCultures = requestLocalizationOptions.Value.SupportedUICultures?.Select(current => current.Name) ?? [];
        var defaultRequestCultureProviders = requestLocalizationOptions.Value.RequestCultureProviders.Select(current => current.GetType().Name);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(requestLocalizationOptions, Is.Not.Null);
            Assert.That(requestLocalizationOptions.Value.DefaultRequestCulture.Culture.Name, Is.EqualTo(OptionsConstants.DefaultCulture));
            Assert.That(requestLocalizationOptions.Value.DefaultRequestCulture.UICulture.Name, Is.EqualTo(OptionsConstants.DefaultCulture));
            Assert.That(supportedCultures, Is.EqualTo(OptionsConstants.DefaultSupportedCultures));
            Assert.That(supportedUiCultures, Is.EqualTo(OptionsConstants.DefaultSupportedCultures));
            Assert.That(defaultRequestCultureProviders, Is.EqualTo(DefaultConstants.DefaultRequestCultureProviders.Select(current => current.GetType().Name)));
        });
    }

    /// <summary>
    /// Ensures that <see cref="ServiceCollectionExtensions.LoadJsonFileTranslationProvider"/> loads the JSON file translation provider.
    /// </summary>
    [Test]
    public void LoadJsonFileTranslationProvider()
    {
        // arrange
        var tmpJsonFile = SondorTranslationTests.CreateTmpJsonTranslationFile();

        var services = SondorTranslationTests.CreateTranslationServices();

        // act
        Assert.DoesNotThrow(() =>
            services.LoadJsonFileTranslationProvider(tmpJsonFile));

        File.Delete(tmpJsonFile.FullName);
    }
}