namespace Sondor.Translations;

/// <summary>
/// The default translation provider.
/// </summary>
internal class DefaultTranslationProvider :
    ISondorTranslationProvider
{
    /// <inheritdoc />
    public Task<string> TranslateAsync(string key,
        string? defaultValue = null,
        CancellationToken cancellationToken = default,
        params object[] parameters)
    {
        return Task.FromResult(string.Empty);
    }
}
