using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Sondor.Translations.Exceptions;
using System.Text.Json;

namespace Sondor.Translations.Providers;

/// <summary>
/// JSON file translation provider.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="JsonFileTranslationProvider"/>.
/// </remarks>
/// <param name="translationFile">The translation file.</param>
/// <param name="localizationOptions">The localization options.</param>
public sealed class JsonFileTranslationProvider(FileInfo translationFile,
    IOptions<RequestLocalizationOptions> localizationOptions) :
    FileTranslationProvider(translationFile, localizationOptions)
{
    /// <summary>
    /// Reads the translation file.
    /// </summary>
    /// <param name="key">The translation key.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the translation.</returns>
    public override async Task<string> ReadAsync(string key,
        CancellationToken cancellationToken = default)
    {
        var json =
            await File.ReadAllTextAsync(TranslationsFile.FullName, cancellationToken);

        if (string.IsNullOrWhiteSpace(json))
        {
            throw new SondorEmptyTranslationFileException(TranslationsFile);
        }

        await using var translationFileStream = TranslationsFile.OpenRead();

        var translations = await JsonSerializer.DeserializeAsync<Dictionary<string, KeyValuePair<string, string>[]>>(translationFileStream, options: null, cancellationToken);

        if (translations is null || translations.Count == 0)
        {
            throw new SondorNoTranslationCulturesException(TranslationsFile);
        }

        if (!translations.ContainsKey(LocalizationOptions.DefaultRequestCulture.Culture.Name))
        {
            throw new SondorTranslationFileMissingDefaultCultureException(LocalizationOptions.DefaultRequestCulture.Culture,
                TranslationsFile);
        }

        var culture = CultureInfo.CurrentCulture.Name;

        if (!translations.TryGetValue(culture, out var cultureTranslations))
        {
            culture = LocalizationOptions.DefaultRequestCulture.Culture.Name;

            cultureTranslations = translations[culture];
        }

        foreach (var translation in cultureTranslations)
        {
            if (!translation.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            return translation.Value;
        }

        return string.Empty;
    }
}
