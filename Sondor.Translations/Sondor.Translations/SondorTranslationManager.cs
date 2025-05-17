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
/// <param name="providers">The provider.</param>
public class SondorTranslationManager(IStringLocalizerFactory localizerFactory,
    IEnumerable<ISondorTranslationProvider>? providers = null) :
    ISondorTranslationManager
{
    /// <summary>
    /// The localizer factory.
    /// </summary>
    private readonly IStringLocalizerFactory _localizerFactory =
        localizerFactory;

    /// <summary>
    /// The translation providers.
    /// </summary>
    private readonly IList<ISondorTranslationProvider> _providers =
            providers?.ToList() ?? [];

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

    /// <inheritdoc />
    public async Task<string> TranslateAsync(string key,
        string? defaultValue = null,
        CancellationToken cancellationToken = default,
        params object[] parameters)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key, nameof(key));

        if (!_providers.Any())
        {
            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                return defaultValue;
            }

            throw new SondorProviderTranslationNotFoundException(key);
        }

        foreach (var provider in _providers)
        {
            var translation = await provider.TranslateAsync(key,
                defaultValue,
                cancellationToken,
                parameters);
            
            if (string.IsNullOrWhiteSpace(translation))
            {
                continue;
            }

            return translation;
        }

        var providers = _providers
            .Select(current => current.GetType().Name);

        throw new SondorProviderTranslationNotFoundException(key, string.Join(',', providers));
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
