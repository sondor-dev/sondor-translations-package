namespace Sondor.Translations;

/// <summary>
/// Sondor translation provider.
/// </summary>
public interface ISondorTranslationProvider
{
    /// <summary>
    /// Gets the translation for the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="parameters">The dynamic parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the found translation.</returns>
    Task<string> TranslateAsync(string key,
        string? defaultValue = null,
        CancellationToken cancellationToken = default,
        params object[] parameters);
}