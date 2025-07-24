using Sondor.Translations.Attributes;

namespace Sondor.Translations.Tests.Attributes;

/// <summary>
/// Tests for the <see cref="TranslationDefaultAttribute"/> class.
/// </summary>
[TestFixture]
public class TranslationDefaultAttributeTests
{
    /// <summary>
    /// Ensures that the constructor of <see cref="TranslationDefaultAttribute"/> works as expected.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        const string defaultValue = "Default Value";

        // act
        var attribute = new TranslationDefaultAttribute(defaultValue);

        // assert
        Assert.That(attribute.DefaultValue, Is.EqualTo(defaultValue));
    }
}