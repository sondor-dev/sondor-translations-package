using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="SondorNoTranslationCulturesException"/>.
/// </summary>
[TestFixture]
public class SondorNoTranslationCulturesExceptionTests
{
    [Test]
    public void Constructor()
    {
        // arrange
        var fileName = Path.GetTempFileName();
        var tempFile = new FileInfo(fileName);
        var expected = string.Format(ExceptionConstants.NoTranslationCulturesErrorFormat, tempFile.FullName);

        // act
        var exception = new SondorNoTranslationCulturesException(tempFile);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.Message, Is.EqualTo(expected));
            Assert.That(exception.TranslationsFile, Is.EqualTo(tempFile));
        });

        File.Delete(fileName);
    }
}
