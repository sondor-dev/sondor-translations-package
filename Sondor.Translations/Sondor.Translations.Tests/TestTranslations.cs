using Sondor.Translations.Attributes;
using Sondor.Translations.Tests.Constants;

namespace Sondor.Translations.Tests;

/// <summary>
/// Collection of test translations.
/// </summary>
internal enum TestTranslations
{
    /// <summary>
    /// The test translation.
    /// </summary>
    [TranslationKey(TestTranslationConstants.TestTranslationKey)]
    [TranslationDefault(TestTranslationConstants.TestTranslationDefault)]
    Test = 1
}
