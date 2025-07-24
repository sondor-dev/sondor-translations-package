using Sondor.Translations.Attributes;

namespace Sondor.Translations.Tests.Attributes;

/// <summary>
/// Tests for the <see cref="TranslationKeyAttribute"/> class.
/// </summary>
[TestFixture]
public class TranslationKeyAttributeTests
{
    /// <summary>
    /// Ensures that the constructor of <see cref="TranslationKeyAttribute"/> works as expected.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        const string key = "TestKey";

        // act
        var attribute = new TranslationKeyAttribute(key);

        // assert
        Assert.That(attribute.Key, Is.EqualTo(key));
    }
}
