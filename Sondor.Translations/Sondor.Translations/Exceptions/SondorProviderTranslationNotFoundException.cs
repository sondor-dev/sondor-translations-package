using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Sondor provider translation not found exception.
/// </summary>
public class SondorProviderTranslationNotFoundException :
    Exception
{
    /// <summary>
    /// Sondor provider translation not found exception.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="providers">The providers.</param>
    public SondorProviderTranslationNotFoundException(string key, string providers) : 
        base(string.Format(ExceptionConstants.ProviderTranslationNotFoundErrorFormat, key, providers))
    {
    }
}
