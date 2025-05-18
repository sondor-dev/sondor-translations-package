using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// No translation providers exception.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorTranslationNoProvidersException"/>.
/// </remarks>
public class SondorTranslationNoProvidersException() :
    Exception(ExceptionConstants.TranslationNoProvidersError);
