using Sondor.Errors.Exceptions;
using Sondor.Translations.Constants;

namespace Sondor.Translations.Exceptions;

/// <summary>
/// Unsupported translation default exception.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="UnsupportedTranslationDefaultException"/>.
/// </remarks>
/// <param name="type">The unsupported type.</param>
public class UnsupportedTranslationDefaultException(Type type) :
    UnsupportedException(string.Format(ExceptionConstants.UnsupportedTranslationDefaultType, type.Name));