using Sondor.Translations.Attributes;
using Sondor.Translations.Exceptions;

namespace Sondor.Translations.Extensions;

/// <summary>
/// Collection of extension methods for enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the translation key for the specified enum instance.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="instance">The instance.</param>
    /// <returns>Returns the translation key.</returns>
    /// <exception cref="UnsupportedTranslationKeyException">This exception is thrown when provided <paramref name="instance"/> does not implement <see cref="TranslationKeyAttribute"/>.</exception>
    public static string GetTranslationKey<TEnum>(this TEnum instance)
        where TEnum : Enum
    {
        var type = instance.GetType();
        var memberInfo = type.GetMember(instance.ToString());

        var attributes = memberInfo[0].GetCustomAttributes(typeof(TranslationKeyAttribute), false);

        return attributes.Length > 0 ?
            ((TranslationKeyAttribute)attributes[0]).Key :
            throw new UnsupportedTranslationKeyException(type);
    }

    /// <summary>
    /// Gets the translation default for the specified enum instance.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="instance">The instance.</param>
    /// <returns>Returns the translation key.</returns>
    /// <exception cref="UnsupportedTranslationKeyException">This exception is thrown when provided <paramref name="instance"/> does not implement <see cref="TranslationKeyAttribute"/>.</exception>
    public static string GetTranslationDefault<TEnum>(this TEnum instance)
        where TEnum : Enum
    {
        var type = instance.GetType();
        var memberInfo = type.GetMember(instance.ToString());

        var attributes = memberInfo[0].GetCustomAttributes(typeof(TranslationDefaultAttribute), false);

        return attributes.Length > 0 ?
            ((TranslationDefaultAttribute)attributes[0]).DefaultValue :
            throw new UnsupportedTranslationDefaultException(type);
    }
}