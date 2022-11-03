namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Reports progress to System.Console using <see cref="ConsoleProgressReporter"/>.
/// </summary>
public class ConsoleProgressReporter : IProgressReporter
{
    /// <inheritdoc />
    public void CreateProgressScope(int maxTicks, string? message, Action<IProgressScope> action)
    {
        CreateProgressScope(maxTicks, message, progress =>
                                               {
                                                   action(progress);
                                                   return 0;
                                               });
    }

    /// <inheritdoc />
    public void CreateProgressScope(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Action<IProgressScope> action)
    {
        CreateProgressScope(maxTicks, message, reportAtPercent, progress =>
                                                                {
                                                                    action(progress);
                                                                    return 0;
                                                                });
    }

    /// <inheritdoc />
    public T CreateProgressScope<T>(int maxTicks, string? message, Func<IProgressScope, T> action)
    {
        var scope = new ConsoleProgressScope(maxTicks, message);
        return action(scope);
    }

    /// <inheritdoc />
    public T CreateProgressScope<T>(int maxTicks, string? message, int reportAtPercent, Func<IProgressScope, T> action)
    {
        var scope = new ConsoleProgressScope(maxTicks, message, reportAtPercent);
        return action(scope);
    }

    /// <inheritdoc />
    public async Task CreateProgressScopeAsync(int maxTicks, string? message, Func<IProgressScope, Task> action)
    {
        await CreateProgressScopeAsync(maxTicks, message, async progress =>
                                                          {
                                                              await action(progress);
                                                              return 0;
                                                          });
    }

    /// <inheritdoc />
    public async Task CreateProgressScopeAsync(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Func<IProgressScope, Task> action)
    {
        await CreateProgressScopeAsync(maxTicks, message, reportAtPercent, async progress =>
                                                                           {
                                                                               await action(progress);
                                                                               return 0;
                                                                           });
    }

    /// <inheritdoc />
    public async Task<T> CreateProgressScopeAsync<T>(
        int maxTicks,
        string? message,
        Func<IProgressScope, Task<T>> action)
    {
        var scope = new ConsoleProgressScope(maxTicks, message);
        return await action(scope);
    }

    /// <inheritdoc />
    public async Task<T> CreateProgressScopeAsync<T>(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Func<IProgressScope, Task<T>> action)
    {
        var scope = new ConsoleProgressScope(maxTicks, message, reportAtPercent);
        return await action(scope);
    }
}