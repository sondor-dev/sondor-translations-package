namespace Sondor.Translations.Tests;

/// <summary>
/// Tests for <see cref="DefaultTranslationProvider"/>.
/// </summary>
[TestFixture]
public class DefaultTranslationProviderTests
{
    /// <summary>
    /// Ensures the <see cref="DefaultTranslationProvider.TranslateAsync"/> returns the expected value.
    /// </summary>
    [Test]
    public async Task TranslateAsync()
    {
        // arrange
        const string key = "key";
        const string defaultValue = "defaultValue";
        var expected = string.Empty;

        var provider = new DefaultTranslationProvider();

        // act
        var value = await provider.TranslateAsync(key, defaultValue);

        // assert
        Assert.That(value, Is.EqualTo(expected));
    }
}
