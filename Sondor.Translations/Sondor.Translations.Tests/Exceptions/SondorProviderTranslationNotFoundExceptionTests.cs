using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="SondorProviderTranslationNotFoundException"/>.
/// </summary>
[TestFixture]
public class SondorProviderTranslationNotFoundExceptionTests
{
    /// <summary>
    /// Ensures the <see cref="SondorProviderTranslationNotFoundException"/> construct sets properties as expected.
    /// </summary>
    [Test]
    public void ConstructorProviders()
    {
        // arrange
        const string key = "key";
        const string providers = "provider-1,provider-2";
        var expected = string.Format(ExceptionConstants.ProviderTranslationNotFoundErrorFormat,
            key,
            providers);

        // act
        var exception = new SondorProviderTranslationNotFoundException(key, providers);

        // assert
        Assert.That(exception.Message, Is.EqualTo(expected));
    }
}
