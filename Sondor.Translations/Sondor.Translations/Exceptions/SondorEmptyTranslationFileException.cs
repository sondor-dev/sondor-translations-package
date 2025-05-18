using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Empty translation file exception.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorEmptyTranslationFileException"/>.
/// </remarks>
/// <param name="translationsFile">The translation file.</param>
public class SondorEmptyTranslationFileException(FileInfo translationsFile) :
    InvalidStateException(string.Format(ExceptionConstants.TranslationFileEmptyErrorFormat, translationsFile.FullName))
{
    /// <summary>
    /// The translations file.
    /// </summary>
    public FileInfo TranslationsFile { get; } = translationsFile;
}