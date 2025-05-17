using System.Collections;

namespace Sondor.Translations.Tests.Args;

/// <summary>
/// Collection of <see cref="string"/> test arguments.
/// </summary>
public class StringArgs : IEnumerable
{
    /// <inheritdoc />
    public IEnumerator GetEnumerator()
    {
        yield return null;
        yield return string.Empty;
        yield return " ";
        yield return "value";
    }
}