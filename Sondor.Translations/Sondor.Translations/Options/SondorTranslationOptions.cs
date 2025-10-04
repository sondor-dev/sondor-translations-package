using Sondor.Errors.Exceptions;
using Sondor.Translations.Args;
using Sondor.Translations.Constants;
using Sondor.Translations.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sondor.Translations.Options;

/// <summary>
/// Sondor translation options.
/// </summary>
public class SondorTranslationOptions
{
    /// <summary>
    /// The supported cultures backing field.
    /// </summary>
    private readonly string[] _supportedCultures = OptionsConstants.DefaultSupportedCultures;

    /// <summary>
    /// The default culture.
    /// </summary>
    public string DefaultCulture { get; init; } = OptionsConstants.DefaultCulture;

    /// <summary>
    /// The supported cultures.
    /// </summary>
    public string[] SupportedCultures
    {
        get => _supportedCultures;
        init => _supportedCultures = value.Length == 0 || value.All(string.IsNullOrWhiteSpace) || value.All(_supportedCultures.Contains)
            ? OptionsConstants.DefaultSupportedCultures
            : value;
    }

    /// <summary>
    /// Determines weather to use the translation key as the final default value. When set to false,
    /// will throw <see cref="SondorTranslationNotFoundException"/> when a translation is not found.
    /// </summary>
    public bool UseKeyAsDefaultValue { get; init; } = true;

    /// <summary>
    /// Asserts that the provided <paramref name="options"/> match the provided <paramref name="expected"/>.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="expected">The expected.</param>
    [ExcludeFromCodeCoverage]
    public static void Assert(SondorTranslationOptions? options, SondorTranslationOptions? expected)
    {
        if (options is null && expected is not null)
        {
            throw new SondorAssertionException("Received null options but expected an instance.");
        }

        if (options is not null && expected is null)
        {
            throw new SondorAssertionException("Received none null options, but expected null.");
        }

        if (!options!.DefaultCulture.Equals(expected!.DefaultCulture))
        {
            throw new SondorAssertionException($"Unfortunately, the received '{nameof(expected.DefaultCulture)}' does not match the expected {nameof(expected.DefaultCulture)}.");
        }

        if (!options.SupportedCultures.All(current => expected.SupportedCultures.Contains(current)))
        {
            throw new SondorAssertionException($"Unfortunately, the received '{nameof(expected.SupportedCultures)}' does not match the expected '{nameof(expected.SupportedCultures)}'.");
        }

        if (!options.UseKeyAsDefaultValue.Equals(expected.UseKeyAsDefaultValue))
        {
            throw new SondorAssertionException($"Unfortunately, the received '{nameof(expected.UseKeyAsDefaultValue)}' does not match the expected '{nameof(expected.UseKeyAsDefaultValue)}'.");
        }
    }
}
