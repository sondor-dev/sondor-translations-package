using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Tests.Exceptions;

/// <summary>
/// Tests for the <see cref="UnsupportedTranslationKeyException"/> class.
/// </summary>
[TestFixture]
public class UnsupportedTranslationKeyExceptionTests
{
    /// <summary>
    /// Ensures that the constructor of <see cref="UnsupportedTranslationKeyException"/> works as expected.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        var type = typeof(string);
        var expectedMessage = string.Format(ExceptionConstants.UnsupportedTranslationKeyType, type.Name);

        // act
        var exception = new UnsupportedTranslationKeyException(type);

        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
            Assert.That(exception, Is.InstanceOf<UnsupportedTranslationKeyException>());
        }
    }
}