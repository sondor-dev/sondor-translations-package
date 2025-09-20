using System.Collections;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Args;

/// <summary>
/// The supported language test arguments.
/// </summary>
public class LanguageArgs : IEnumerable
{
    /// <inheritdoc />
    public IEnumerator GetEnumerator()
    {
        return TranslationConstants.SupportedCultures.GetEnumerator();
    }
}