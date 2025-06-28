using System.Globalization;
using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Translation file missing default culture exception.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorTranslationFileMissingDefaultCultureException"/>.
/// </remarks>
/// <param name="defaultCulture">The default culture.</param>
/// <param name="translationsFile">The translations file.</param>
public class SondorTranslationFileMissingDefaultCultureException(CultureInfo defaultCulture,
    FileInfo translationsFile) :
    ResourceInvalidException(string.Format(ExceptionConstants.TranslationFileMissingDefaultCulture,
        defaultCulture.Name,
        translationsFile.FullName))
{
    /// <summary>
    /// The translation file.
    /// </summary>
    public FileInfo TranslationsFile { get; } = translationsFile;

    /// <summary>
    /// The default culture.
    /// </summary>
    public CultureInfo DefaultCulture { get; } = defaultCulture;
}
