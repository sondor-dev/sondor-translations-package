using Microsoft.AspNetCore.Localization;

namespace Sondor.Translations.Constants;

/// <summary>
/// Collection of default constants for translations.
/// </summary>
internal class DefaultConstants
{
    /// <summary>
    /// The key 1 translation.
    /// </summary>
    internal static readonly KeyValuePair<string, string> Key1 =
        new("key-1", "value-1");

    /// <summary>
    /// The key 2 translation.
    /// </summary>
    internal static readonly KeyValuePair<string, string> Key2 =
        new("key-2", "value-2");

    /// <summary>
    /// The default translations.
    /// </summary>
    internal static Dictionary<string, IEnumerable<KeyValuePair<string, string>>> DefaultTranslations => new()
    {
        {
            OptionsConstants.DefaultCulture, new List<KeyValuePair<string, string>>
            {
                Key1,
                Key2
            }
        }
    };

    /// <summary>
    /// The default request culture providers.
    /// </summary>
    internal static IRequestCultureProvider[] DefaultRequestCultureProviders => new IRequestCultureProvider[]
    {
        new AcceptLanguageHeaderRequestCultureProvider(),
        new CookieRequestCultureProvider()
    };
}
