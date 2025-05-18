using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="SondorEmptyTranslationFileException"/>
/// </summary>
[TestFixture]
public class SondorEmptyTranslationFileExceptionTests
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
        var expected = string.Format(ExceptionConstants.TranslationFileEmptyErrorFormat,
            tempFile.FullName);
        
        // act
        var exception = new SondorEmptyTranslationFileException(tempFile);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.Message, Is.EqualTo(expected));
            Assert.That(exception.TranslationsFile, Is.EqualTo(tempFile));
        });

        File.Delete(tempFile.FullName);
    }
}
