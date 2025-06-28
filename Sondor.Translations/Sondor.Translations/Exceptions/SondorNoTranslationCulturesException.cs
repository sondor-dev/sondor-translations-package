using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Translation file loaded but contains no translation cultures.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorNoTranslationCulturesException"/>.
/// </remarks>
/// <param name="translationsFile">The translation file.</param>
public class SondorNoTranslationCulturesException(FileInfo translationsFile) :
    ResourceInvalidException(string.Format(ExceptionConstants.NoTranslationCulturesErrorFormat, translationsFile.FullName))
{
    /// <summary>
    /// The translation file.
    /// </summary>
    public FileInfo TranslationsFile { get; } = translationsFile;
}
