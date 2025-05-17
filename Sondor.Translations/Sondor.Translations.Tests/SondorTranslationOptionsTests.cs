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

        // act
        var options = new SondorTranslationOptions
        {
            DefaultCulture = defaultCulture,
            SupportedCultures = supportedCultures
        };

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(options.DefaultCulture, Is.EqualTo(defaultCulture));
            Assert.That(options.SupportedCultures, Is.EqualTo(supportedCultures));
        });
    }
}
