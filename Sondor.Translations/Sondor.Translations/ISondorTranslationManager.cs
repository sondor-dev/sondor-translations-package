using Sondor.Translations.Exceptons;

namespace Sondor.Translations;

/// <summary>
/// Sondor translation manager.
/// </summary>
public interface ISondorTranslationManager
{
    /// <summary>
    /// Gets the translation for the specified key.
    /// </summary>
    /// <param name="key">The translation key.</param>
    /// <param name="resource">The translation resource.</param>
    /// <param name="location">The translation location.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="parameters">The parameters to dynamically inject.</param>
    /// <returns>The translation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when an invalid argument is provided.</exception>
    /// <exception cref="ArgumentNullException">This exception is thrown when an invalid argument is provided.</exception>
    /// <exception cref="SondorTranslationNotFoundException">This exception is thrown when a translation could not be found.</exception>
    string Translate(string key,
        string location,
        string resource,
        string? defaultValue = null,
        params object[] parameters);
}
