namespace Sondor.Translations.Constants;

/// <summary>
/// Collection of translation constants.
/// </summary>
internal class TranslationConstants
{
    /// <summary>
    /// The default resource name.
    /// </summary>
    internal const string DefaultResourceName = "Resources";

    /// <summary>
    /// The translation not found error message format.
    /// </summary>
    internal const string TranslationNotFoundErrorFormat =
        "Unfortunately, no translation could by find with a key of '{0}'. Searched in '{1}.{2}' - {3}";

    /// <summary>
    /// The translation provider not found error message format.
    /// </summary>
    internal const string ProviderTranslationNotFoundErrorFormat =
        "Unfortunately, no translation could by find with a key of '{0}'. Searched in '{1}' providers.";

    /// <summary>
    /// The provider translation not found when no providers are provided error message format.
    /// </summary>
    internal const string NoProvidersTranslationNotFoundErrorFormat =
        "Unfortunately, no translation could by find with a key of '{0}'. No providers were provided.";
}