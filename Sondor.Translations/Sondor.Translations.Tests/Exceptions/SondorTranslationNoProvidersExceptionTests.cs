using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="SondorEmptyTranslationFileException"/>.
/// </summary>
[TestFixture]
public class SondorTranslationNoProvidersExceptionTests
{
    /// <summary>
    /// Ensures the constructor works as expected.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        const string expected = ExceptionConstants.TranslationNoProvidersError;

        // act
        var exception = new SondorTranslationNoProvidersException();

        // assert
        Assert.That(exception.Message, Is.EqualTo(expected));
    }
}
