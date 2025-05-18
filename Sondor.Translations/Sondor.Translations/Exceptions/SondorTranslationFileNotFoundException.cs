using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Translation file not found exception.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorTranslationFileNotFoundException"/>.
/// </remarks>
/// <param name="translationsFile">The translations file.</param>
public sealed class SondorTranslationFileNotFoundException(FileInfo translationsFile) :
    InvalidStateException(string.Format(ExceptionConstants.TranslationFileNotFoundErrorFormat, translationsFile.FullName))
{
    /// <summary>
    /// The translations file.
    /// </summary>
    public FileInfo TranslationsFile { get; } = translationsFile;
}