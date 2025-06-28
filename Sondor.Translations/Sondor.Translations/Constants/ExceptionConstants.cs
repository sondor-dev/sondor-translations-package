namespace Sondor.Translations.Constants;

/// <summary>
/// Collection of exception constants.
/// </summary>
internal class ExceptionConstants
{
    /// <summary>
    /// The translation not found error message format.
    /// </summary>
    internal const string TranslationNotFoundErrorFormat =
        "Unfortunately, no translation could by found with a key of '{0}'. Searched in '{1}.{2}' - {3}";

    /// <summary>
    /// The translation provider not found error message format.
    /// </summary>
    internal const string ProviderTranslationNotFoundErrorFormat =
        "Unfortunately, no translation could by found with a key of '{0}'. Searched in '{1}' providers.";

    /// <summary>
    /// The translation file not found error format.
    /// </summary>
    internal const string TranslationFileNotFoundErrorFormat =
        "The translation file '{0}' was not found.";

    /// <summary>
    /// The translation file empty error format.
    /// </summary>
    internal const string TranslationFileEmptyErrorFormat =
        "Unfortunately, the provided translation file has no content. File: '{0}'.";

    /// <summary>
    /// The invalid translation file error format.
    /// </summary>
    internal const string InvalidTranslationsFileErrorFormat =
        "Unfortunately, the provided translation file contents are invalid and could not be processed as a transation source. File: '{0}'.";

    /// <summary>
    /// The translation file contains no cultures error format.
    /// </summary>
    internal const string TranslationFileContainsNoCultures =
        "Unfortunately, the provided translation file does not contain any cultures. File: '{0}'.";

    /// <summary>
    /// The translation file missing default culture error format.
    /// </summary>
    internal const string TranslationFileMissingDefaultCulture =
        "Unfortunately, the provided translation file does not contain the default fulture '{0}'. File: '{1}'.";

    /// <summary>
    /// The no translation providers error message.
    /// </summary>
    internal const string TranslationNoProvidersError =
        "Unfortunately, no translation providers were provided. Please provide at least one translation provider.";

    /// <summary>
    /// The no translation cultures error message format.
    /// </summary>
    internal const string NoTranslationCulturesErrorFormat =
        "Unfortunately, the translation was loaded successful but contains no translations for any cultures. File: '{0}'.";
}
