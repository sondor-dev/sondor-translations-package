using System.Globalization;
using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Translation not found exception.
/// </summary>
public sealed class SondorTranslationNotFoundException :
    ResourceNotFoundException
{
    /// <summary>
    /// The translation key.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// The translation location.
    /// </summary>
    public string Location { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="SondorTranslationNotFoundException"/>.
    /// </summary>
    /// <param name="key">The translation key.</param>
    /// <param name="resource">The translation resource.</param>
    /// <param name="location">The translation location.</param>
    public SondorTranslationNotFoundException(string key,
        string resource,
        string location) :
        base(string.Format(ExceptionConstants.TranslationNotFoundErrorFormat,
            key,
            location,
            resource,
            CultureInfo.CurrentUICulture.Name))
    {
        Key = key;
        Location = location;
        Resource = resource;
    }
}