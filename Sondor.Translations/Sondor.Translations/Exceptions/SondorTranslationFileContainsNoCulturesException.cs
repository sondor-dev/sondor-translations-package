using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Translation file contains no cultures exception.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorTranslationFileContainsNoCulturesException"/>.
/// </remarks>
/// <param name="translationsFile">The translations file.</param>
public class SondorTranslationFileContainsNoCulturesException(FileInfo translationsFile) :
    InvalidStateException(string.Format(ExceptionConstants.TranslationFileContainsNoCultures, translationsFile.FullName))
{
    /// <summary>
    /// The translations file.
    /// </summary>
    public FileInfo TranslationsFile { get; } = translationsFile;
}
