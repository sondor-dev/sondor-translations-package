using Microsoft.AspNetCore.Builder;
using Sondor.Translations.Extensions;

namespace Sondor.Translations.Tests.Extensions;

/// <summary>
/// Tests for <see cref="WebApplicationExtensions"/>
/// </summary>
[TestFixture]
public class WebApplicationExtensionsTests
{
    /// <summary>
    /// Ensures that <see cref="WebApplicationExtensions.UseSondorTranslations"/> works as intended.
    /// </summary>
    [Test]
    public void UseSondorTranslations()
    {
        // arrange
        var builder = WebApplication.CreateBuilder();
        SondorTranslationTests.CreateTranslationServices(services: builder.Services);
        var application = builder.Build();

        // act
        application.UseSondorTranslations();

        // assert
        // TODO: Investigate a way to test that middleware is registered
    }
}
