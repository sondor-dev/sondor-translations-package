using Sondor.Translations.Constants;
using Sondor.Translations.Options;

namespace Sondor.Translations.Tests;

/// <summary>
/// Tests for <see cref="SondorTranslationOptions"/>.
/// </summary>
[TestFixture]
public class SondorTranslationOptionsTests
{
    /// <summary>
    /// Ensures the constructor works as expected.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        const string defaultCulture = "en-GB";
        var supportedCultures = new[]
        {
            "en-GB",
            "fr-FR"
        };
        const bool useKeyAsDefaultValue = true;

        // act
        var options = new SondorTranslationOptions
        {
            DefaultCulture = defaultCulture,
            SupportedCultures = supportedCultures,
            UseKeyAsDefaultValue = useKeyAsDefaultValue
        };

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(options.DefaultCulture, Is.EqualTo(defaultCulture));
            Assert.That(options.SupportedCultures, Is.EqualTo(supportedCultures));
            Assert.That(options.UseKeyAsDefaultValue, Is.EqualTo(useKeyAsDefaultValue));
        });
    }

    /// <summary>
    /// Ensures the default constructor works as expected.
    /// </summary>
    [Test]
    public void Constructor_default()
    {
        // arrange
        const string defaultCulture = OptionsConstants.DefaultCulture;
        var supportedCultures = OptionsConstants.DefaultSupportedCultures;
        const bool useKeyAsDefaultValue = OptionsConstants.DefaultUseKeyAsDefaultValue;

        // act
        var options = new SondorTranslationOptions();

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(options.DefaultCulture, Is.EqualTo(defaultCulture));
            Assert.That(options.SupportedCultures, Is.EqualTo(supportedCultures));
            Assert.That(options.UseKeyAsDefaultValue, Is.EqualTo(useKeyAsDefaultValue));
        });
    }
}
