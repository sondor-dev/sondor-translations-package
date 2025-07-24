namespace Sondor.Translations.Attributes;

/// <summary>
/// Translation default attribute.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="TranslationDefaultAttribute"/>.
/// </remarks>
/// <param name="defaultValue">The default value/</param>
[AttributeUsage(AttributeTargets.Field)]
public class TranslationDefaultAttribute(string defaultValue) :
    Attribute
{
    /// <summary>
    /// The default value.
    /// </summary>
    public string DefaultValue { get; } = defaultValue;
}
