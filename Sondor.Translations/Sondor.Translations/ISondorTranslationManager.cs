using Sondor.Translations.Exceptions;

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

    /// <summary>
    /// Gets the translation for the specified key asynchronously.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>Returns the found translation.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when an invalid argument is provided.</exception>
    /// <exception cref="ArgumentNullException">This exception is thrown when an invalid argument is provided.</exception>
    /// <exception cref="SondorTranslationNotFoundException">This exception is thrown when a translation could not be found.</exception>
    Task<string> TranslateAsync(string key,
        string? defaultValue = null,
        CancellationToken cancellationToken = default,
        params object[] parameters);
}
