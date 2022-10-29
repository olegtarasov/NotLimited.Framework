namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Reports progress to System.Console using <see cref="ConsoleProgressReporter"/>.
/// </summary>
public class ConsoleProgressReporter : IProgressReporter
{
    /// <inheritdoc />
    public void CreateProgressScope(int maxTicks, string? message, Action<IProgressScope> action)
    {
        var scope = new ConsoleProgressScope(maxTicks, message);
        action(scope);
    }

    /// <inheritdoc />
    public void CreateProgressScope(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Action<IProgressScope> action)
    {
        var scope = new ConsoleProgressScope(maxTicks, message, reportAtPercent);
        action(scope);
    }

    /// <inheritdoc />
    public async Task CreateProgressScopeAsync(int maxTicks, string? message, Func<IProgressScope, Task> action)
    {
        var scope = new ConsoleProgressScope(maxTicks, message);
        await action(scope);
    }

    /// <inheritdoc />
    public async Task CreateProgressScopeAsync(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Func<IProgressScope, Task> action)
    {
        var scope = new ConsoleProgressScope(maxTicks, message, reportAtPercent);
        await action(scope);
    }
}