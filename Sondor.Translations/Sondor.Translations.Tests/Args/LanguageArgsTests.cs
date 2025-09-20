using Sondor.Translations.Args;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Tests.Args;

/// <summary>
/// Tests for <see cref="LanguageArgs"/>.
/// </summary>
[TestFixture]
public class LanguageArgsTests
{
    /// <summary>
    /// Ensures that all supported languages are included in the args.
    /// </summary>
    /// <param name="language">The language.</param>
    [TestCaseSource(typeof(LanguageArgs))]
    public void LanguageArgs(string language)
    {
        // arrange
        const bool expected = true;

        // act
        var actual = TranslationConstants.SupportedCultures.Contains(language);

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}