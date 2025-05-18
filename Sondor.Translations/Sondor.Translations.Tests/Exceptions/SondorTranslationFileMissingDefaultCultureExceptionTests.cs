using System.Globalization;
using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="SondorTranslationFileMissingDefaultCultureException"/>.
/// </summary>
[TestFixture]
public class SondorTranslationFileMissingDefaultCultureExceptionTests
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
        var defaultCulture = new CultureInfo("en-GB");
        var expected = string.Format(ExceptionConstants.TranslationFileMissingDefaultCulture,
            defaultCulture.Name,
            tempFile.FullName);

        // act
        var exception = new SondorTranslationFileMissingDefaultCultureException(defaultCulture,
            tempFile);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.Message, Is.EqualTo(expected));
            Assert.That(exception.DefaultCulture, Is.EqualTo(defaultCulture));
            Assert.That(exception.TranslationsFile, Is.EqualTo(tempFile));
        });

        File.Delete(tempFile.FullName);
    }
}
