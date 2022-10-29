namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Status scope that uses plain console to write each status from a new line.
/// </summary>
public class ConsoleStatusScope : IStatusScope
{
    /// <inheritdoc />
    public void SetStatus(string message)
    {
        Console.WriteLine(message);
    }
}