using System.Globalization;
using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Translation not found exception.
/// </summary>
/// <remarks>
/// Creates a new instance of the <see cref="SondorTranslationNotFoundException"/>.
/// </remarks>
/// <param name="key">The translation key.</param>
/// <param name="resource">The translation resource.</param>
/// <param name="location">The translation location.</param>
public sealed class SondorTranslationNotFoundException(string key,
    string resource,
    string location) :
    EntityNotFoundException(string.Format(ExceptionConstants.TranslationNotFoundErrorFormat,
        key,
        location,
        resource,
        CultureInfo.CurrentUICulture.Name))
{
    /// <summary>
    /// The translation key.
    /// </summary>
    public string Key { get; } = key;

    /// <summary>
    /// The translation resource.
    /// </summary>
    public string Resource { get; } = resource;

    /// <summary>
    /// The translation location.
    /// </summary>
    public string Location { get; } = location;
}