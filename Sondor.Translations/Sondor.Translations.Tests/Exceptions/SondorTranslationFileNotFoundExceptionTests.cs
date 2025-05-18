using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="SondorTranslationFileNotFoundException"/>.
/// </summary>
[TestFixture]
public class SondorTranslationFileNotFoundExceptionTests
{
    /// <summary>
    /// Ensures the constructor works as expected.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        var fileName = Path.GetTempFileName();
        var tempFile = new FileInfo(fileName);
        var expected = string.Format(ExceptionConstants.TranslationFileNotFoundErrorFormat,
            tempFile.FullName);

        // act
        var exception = new SondorTranslationFileNotFoundException(tempFile);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.Message, Is.EqualTo(expected));
            Assert.That(exception.TranslationsFile, Is.EqualTo(tempFile));
        });

        File.Delete(tempFile.FullName);
    }
}
