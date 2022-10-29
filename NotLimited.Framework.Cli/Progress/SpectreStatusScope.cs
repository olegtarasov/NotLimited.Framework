using System.Reflection;
using NotLimited.Framework.Common.Progress;
using Spectre.Console;

namespace NotLimited.Framework.Cli.Progress;

/// <summary>
/// Status scope that uses Spectre.Console.
/// </summary>
public class SpectreStatusScope : IStatusScope
{
    private static readonly Dictionary<string, PropertyInfo> Spinners;

    static SpectreStatusScope()
    {
        Spinners = new(StringComparer.OrdinalIgnoreCase);
        var props = typeof(Spinner.Known).GetProperties(BindingFlags.Public | BindingFlags.Static);
        foreach (var prop in props)
        {
            Spinners[prop.Name] = prop;
        }
    }

    private readonly Spinner _spinner;
    private readonly StatusContext _context;

    /// <summary>
    /// Ctor.
    /// </summary>
    public SpectreStatusScope(StatusContext context, string spinner)
    {
        _context = context;
        _spinner = GetSpinner(spinner);
    }

    /// <inheritdoc />
    public void SetStatus(string message)
    {
        _context.Status = message;
    }

    private static Spinner GetSpinner(string name)
    {
        if (!Spinners.TryGetValue(name, out var prop))
            throw new InvalidOperationException($"Spinner {name} not found");

        return (Spinner)(prop.GetValue(null)
                         ?? throw new InvalidOperationException($"Failed to get an instance for spinner {name}"));
    }
}