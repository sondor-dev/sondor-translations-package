using Sondor.Translations.Options;

namespace Sondor.Translations.Constants;

/// <summary>
/// Collection of translation options constants.
/// </summary>
internal static class OptionsConstants
{
    /// <summary>
    /// The default use key as default value option.
    /// </summary>
    internal const bool DefaultUseKeyAsDefaultValue = true;

    /// <summary>
    /// The default culture.
    /// </summary>
    internal const string DefaultCulture = "en";

    /// <summary>
    /// The default supported cultures.
    /// </summary>
    internal static readonly string[] DefaultSupportedCultures = [
        "en",
        "en-GB",
        "en-US"
    ];

    /// <summary>
    /// The default translation options.
    /// </summary>
    internal static readonly SondorTranslationOptions DefaultTranslationOptions =
        new()
        {
            DefaultCulture = DefaultCulture,
            SupportedCultures = DefaultSupportedCultures,
            UseKeyAsDefaultValue = DefaultUseKeyAsDefaultValue
        };
}
