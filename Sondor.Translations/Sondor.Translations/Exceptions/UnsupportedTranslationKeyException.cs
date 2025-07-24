using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Unsupported translation key exception.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="UnsupportedTranslationKeyException"/>.
/// </remarks>
/// <param name="type">The unsupported type.</param>
public class UnsupportedTranslationKeyException(Type type) :
    UnsupportedException(string.Format(ExceptionConstants.UnsupportedTranslationKeyType, type.Name));