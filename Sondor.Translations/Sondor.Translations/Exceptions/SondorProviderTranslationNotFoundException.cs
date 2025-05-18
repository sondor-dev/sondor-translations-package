using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Sondor provider translation not found exception.
/// </summary>
/// <remarks>
/// Sondor provider translation not found exception.
/// </remarks>
/// <param name="key">The key.</param>
/// <param name="providers">The providers.</param>
public class SondorProviderTranslationNotFoundException(string key, string providers) :
    EntityNotFoundException(string.Format(ExceptionConstants.ProviderTranslationNotFoundErrorFormat, key, providers));
