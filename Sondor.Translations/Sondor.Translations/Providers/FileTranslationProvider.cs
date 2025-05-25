using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Providers;

/// <summary>
/// File translation provider.
/// </summary>
/// <remarks>
/// Creates a new instance of the <see cref="FileTranslationProvider"/>.
/// </remarks>
/// <param name="translationsFile">The translation file.</param>
/// <param name="localizationOptions">The localization options.</param>
/// <exception cref="SondorTranslationFileNotFoundException">This exception is thrown when the provided translation file does not exist.</exception>
public abstract class FileTranslationProvider(FileInfo translationsFile,
    IOptions<RequestLocalizationOptions> localizationOptions) :
    ISondorTranslationProvider
{
    /// <summary>
    /// The localization options.
    /// </summary>
    protected readonly RequestLocalizationOptions LocalizationOptions = localizationOptions.Value;

    /// <summary>
    /// The translations file.
    /// </summary>
    protected readonly FileInfo TranslationsFile = !translationsFile.Exists ?
            throw new SondorTranslationFileNotFoundException(translationsFile) :
            translationsFile;

    /// <inheritdoc />
    public virtual async Task<string> TranslateAsync(string key,
        string? defaultValue = null,
        CancellationToken cancellationToken = default,
        params object[] parameters)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key, nameof(key));

        var translation = await ReadAsync(key, cancellationToken);

        return parameters.Length > 0 ?
            string.Format(translation) :
            translation;
    }

    /// <summary>
    /// Reads the translation file.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the translation.</returns>
    public abstract Task<string> ReadAsync(string key,
        CancellationToken cancellationToken = default);
}