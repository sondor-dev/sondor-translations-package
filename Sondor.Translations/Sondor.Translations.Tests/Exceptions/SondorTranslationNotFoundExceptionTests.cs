using Sondor.Translations.Exceptons;

namespace Sondor.Translations.Tests.Exceptions;

/// <summary>
/// Tests for <see cref="SondorTranslationNotFoundException"/>.
/// </summary>
[TestFixture]
public class SondorTranslationNotFoundExceptionTests
{
    /// <summary>
    /// Ensures the constructor works as expected.
    /// </summary>
    [Test]
    public void Constructor()
    {
        // arrange
        const string key = "key";
        const string resource = "resource";
        const string location = "location";

        // act
        var exception = new SondorTranslationNotFoundException(key,
            resource,
            location);

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.Key, Is.EqualTo(key));
            Assert.That(exception.Resource, Is.EqualTo(resource));
            Assert.That(exception.Location, Is.EqualTo(location));
        });
    }
}