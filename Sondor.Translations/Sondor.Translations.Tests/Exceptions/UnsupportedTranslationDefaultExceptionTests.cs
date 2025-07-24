using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Tests.Exceptions;

/// <summary>
/// Tests for the <see cref="UnsupportedTranslationDefaultException"/> class.
/// </summary>
[TestFixture]
public class UnsupportedTranslationDefaultExceptionTests
{
    /// <summary>
    /// Ensures that the constructor of <see cref="UnsupportedTranslationDefaultException"/> works as expected.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        var type = typeof(string);
        var expectedMessage = string.Format(ExceptionConstants.UnsupportedTranslationDefaultType, type.Name);

        // act
        var exception = new UnsupportedTranslationDefaultException(type);

        // assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
            Assert.That(exception, Is.InstanceOf<UnsupportedTranslationDefaultException>());
        }
    }
}