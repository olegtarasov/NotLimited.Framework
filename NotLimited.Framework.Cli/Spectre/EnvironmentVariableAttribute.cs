using Spectre.Console.Cli;

namespace NotLimited.Framework.Cli.Spectre;

/// <summary>
/// Loads parameter value from a specified environment variable.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class EnvironmentValueAttribute : ParameterValueProviderAttribute
{
    private readonly string _variableName;

    /// <summary>
    /// Ctor.
    /// </summary>
    public EnvironmentValueAttribute(string variableName)
    {
        if (string.IsNullOrEmpty(variableName))
            throw new ArgumentNullException(nameof(variableName));

        _variableName = variableName;
    }

    /// <inheritdoc />
    public override bool TryGetValue(CommandParameterContext context, out object? result)
    {
        result = null;

        // Environment values have the lowes priority.
        if (!string.IsNullOrEmpty(context.Value as string))
            return false;

        string? value = Environment.GetEnvironmentVariable(_variableName);
        if (!string.IsNullOrEmpty(value))
        {
            result = value;
            return true;
        }

        return false;
    }
}