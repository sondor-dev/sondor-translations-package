using Microsoft.Extensions.Localization;
using Sondor.Translations.Exceptons;

namespace Sondor.Translations;

/// <summary>
/// Sondor translation manager.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorTranslationManager"/>.
/// </remarks>
/// <param name="localizerFactory">The localizer factory.</param>
public class SondorTranslationManager(IStringLocalizerFactory localizerFactory) :
    ISondorTranslationManager
{
    /// <summary>
    /// The localizer factory.
    /// </summary>
    private readonly IStringLocalizerFactory _localizerFactory =
        localizerFactory;

    /// <inheritdoc />
    public string Translate(string key,
        string location,
        string resource,
        string? defaultValue = null,
        params object[] parameters)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key, nameof(key));
        ArgumentException.ThrowIfNullOrWhiteSpace(location, nameof(location));
        ArgumentException.ThrowIfNullOrWhiteSpace(resource, nameof(resource));

        var localizer = CreateLocalizer(location, resource);
        var translation = localizer[key];

        if (translation.ResourceNotFound)
        {
            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                return string.Format(defaultValue, parameters);
            }

            throw new SondorTranslationNotFoundException(key, resource, location);
        }

        var formattedTranslation = string.Format(translation, parameters);

        return formattedTranslation;
    }

    /// <summary>
    /// Creates a localizer for the specified location and resource.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="resource">The resource.</param>
    /// <returns>Returns the localizer.</returns>
    private IStringLocalizer CreateLocalizer(string location,
        string resource)
    {
        return _localizerFactory.Create(resource, location);
    }
}
