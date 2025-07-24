using Sondor.Translations.Extensions;
using Sondor.Translations.Tests.Constants;

namespace Sondor.Translations.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="EnumExtensions"/> class.
/// </summary>
[TestFixture]
public class EnumExtensionsTests
{
    /// <summary>
    /// Ensures that the <see cref="EnumExtensions.GetTranslationKey{TEnum}"/> method works as expected.
    /// </summary>
    [Test]
    public void GetTranslationKey()
    {
        // arrange
        const TestTranslations translation = TestTranslations.Test;
        const string expected = TestTranslationConstants.TestTranslationKey;

        // act
        var key = translation.GetTranslationKey();

        // assert
        Assert.That(key, Is.EqualTo(expected));
    }

    /// <summary>
    /// Ensures that the <see cref="EnumExtensions.GetTranslationDefault{TEnum}"/> method works as expected.
    /// </summary>
    [Test]
    public void GetTranslationDefault()
    {
        // arrange
        const TestTranslations translation = TestTranslations.Test;
        const string expected = TestTranslationConstants.TestTranslationDefault;

        // act
        var key = translation.GetTranslationDefault();

        // assert
        Assert.That(key, Is.EqualTo(expected));
    }
}
