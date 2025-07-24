namespace Sondor.Translations.Attributes;

/// <summary>
/// Translation key attribute.
/// </summary>
/// <remarks>
/// Create a new instance of <see cref="TranslationKeyAttribute"/>.
/// </remarks>
/// <param name="key">The translation key.</param>
[AttributeUsage(AttributeTargets.Field)]
public class TranslationKeyAttribute(string key) :
    Attribute
{
    /// <summary>
    /// The translation key.
    /// </summary>
    public string Key { get; } = key;
}