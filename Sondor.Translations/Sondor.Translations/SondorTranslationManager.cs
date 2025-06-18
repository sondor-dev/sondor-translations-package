using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Sondor.Translations.Exceptions;
using Sondor.Translations.Options;

namespace Sondor.Translations;

/// <summary>
/// Sondor translation manager.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorTranslationManager"/>.
/// </remarks>
/// <param name="localizerFactory">The localizer factory.</param>
/// <param name="providers">The provider.</param>
public class SondorTranslationManager(IOptions<SondorTranslationOptions> translationOptions,
    IStringLocalizerFactory localizerFactory,
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

    /// <summary>
    /// The translation options.
    /// </summary>
    private readonly SondorTranslationOptions _translationOptions =
        translationOptions.Value;

    /// <inheritdoc ./>
    public string Translate(string key,
        string location,
        string resource,
        string? defaultValue = null,
        params object[] parameters)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key), "Key cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));
        }

        if (location is null)
        {
            throw new ArgumentNullException(nameof(location), "Location cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(location))
        {
            throw new ArgumentException("Location cannot be null or whitespace.", nameof(location));
        }

        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource), "Resource cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(resource))
        {
            throw new ArgumentException("Resource cannot be null or whitespace.", nameof(resource));
        }

        var localizer = CreateLocalizer(location, resource);
        var translation = localizer[key];

        if (translation.ResourceNotFound)
        {
            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                return parameters.Length == 0 ?
                    defaultValue :
                    string.Format(defaultValue, parameters);
            }

            if (_translationOptions.UseKeyAsDefaultValue)
            {
                return key;
            }

            throw new SondorTranslationNotFoundException(key, resource, location);
        }

        if (parameters.Length == 0)
        {
            return translation;
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
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key), "Key cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));
        }

        if (!_providers.Any())
        {
            throw new SondorTranslationNoProvidersException();
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

        if (!string.IsNullOrWhiteSpace(defaultValue))
        {
            return defaultValue;
        }

        if (_translationOptions.UseKeyAsDefaultValue)
        {
            return key;
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
