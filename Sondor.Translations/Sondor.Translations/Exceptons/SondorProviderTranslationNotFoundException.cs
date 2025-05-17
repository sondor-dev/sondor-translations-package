using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptons;

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
    public SondorProviderTranslationNotFoundException(string key) :
        base(string.Format(TranslationConstants.NoProvidersTranslationNotFoundErrorFormat, key))
    {
    }

    /// <summary>
    /// Sondor provider translation not found exception.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="providers">The providers.</param>
    public SondorProviderTranslationNotFoundException(string key, string providers) : 
        base(string.Format(TranslationConstants.ProviderTranslationNotFoundErrorFormat, key, providers))
    {
    }
}
