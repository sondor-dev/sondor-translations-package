using FluentValidation;

namespace Sondor.Translations.Options.Validators;

/// <summary>
/// Validator for <see cref="SondorTranslationOptions"/>.
/// </summary>
public class SondorTranslationOptionsValidator :
    AbstractValidator<SondorTranslationOptions>
{
    /// <summary>
    /// Creates a new instance of <see cref="SondorTranslationOptionsValidator"/>.
    /// </summary>
    public SondorTranslationOptionsValidator()
    {
        RuleFor(prop => prop.DefaultCulture)
            .NotEmpty()
            .NotNull();

        RuleFor(prop => prop.SupportedCultures)
            .NotEmpty()
            .NotNull()
            .Must(cultures => cultures.Length > 0)
            .WithMessage("At least one supported culture must be provided.")
            .Must(cultures => cultures.Length == cultures.Distinct().Count())
            .WithMessage("Duplicate cultures are not allowed.");
    }
}