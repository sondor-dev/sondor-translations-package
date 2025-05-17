namespace Sondor.Translations;

/// <summary>
/// Sondor translation options.
/// </summary>
public class SondorTranslationOptions
{
    /// <summary>
    /// The default culture.
    /// </summary>
    public string DefaultCulture { get; init; } = string.Empty;

    /// <summary>
    /// The supported cultures.
    /// </summary>
    public string[] SupportedCultures { get; init; } = [];
}
