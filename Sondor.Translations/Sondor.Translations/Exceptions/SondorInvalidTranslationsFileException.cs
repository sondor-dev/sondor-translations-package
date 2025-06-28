using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Invalid translations file exception.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorInvalidTranslationsFileException"/>.
/// </remarks>
/// <param name="translationsFile">The translations file.</param>
public class SondorInvalidTranslationsFileException(FileInfo translationsFile) :
    ResourceInvalidException(string.Format(ExceptionConstants.InvalidTranslationsFileErrorFormat, translationsFile.FullName))
{
    /// <summary>
    /// The translations file.
    /// </summary>
    public FileInfo TranslationsFile { get; } = translationsFile;
}
