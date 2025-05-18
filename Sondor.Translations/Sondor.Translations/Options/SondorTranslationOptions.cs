using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Options;

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

    /// <summary>
    /// Determines weather to use the translation key as the final default value. When set to false,
    /// will throw <see cref="SondorTranslationNotFoundException"/> when a translation is not found.
    /// </summary>
    public bool UseKeyAsDefaultValue { get; init; } = true;
}
