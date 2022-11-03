using NotLimited.Framework.Common.Progress;
using Spectre.Console;

namespace NotLimited.Framework.Cli.Progress;

/// <summary>
/// Progress reporter utilizing Spectre.Console.
/// </summary>
public class SpectreProgressReporter : IProgressReporter
{
    private readonly bool _externalRefresh;

    /// <summary>
    /// Ctor.
    /// </summary>
    public SpectreProgressReporter(bool externalRefresh)
    {
        _externalRefresh = externalRefresh;
    }

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
        return AnsiConsole.Progress().HideCompleted(true).AutoClear(true).AutoRefresh(!_externalRefresh).Start(
            context =>
            {
                var scope = new SpectreProgressScope(context, maxTicks, message, externalRefresh: _externalRefresh);
                return action(scope);
            });
    }

    /// <inheritdoc />
    public T CreateProgressScope<T>(int maxTicks, string? message, int reportAtPercent, Func<IProgressScope, T> action)
    {
        return AnsiConsole.Progress().HideCompleted(true).AutoClear(true).AutoRefresh(!_externalRefresh).Start(
            context =>
            {
                var scope = new SpectreProgressScope(context, maxTicks, message, reportAtPercent, _externalRefresh);
                return action(scope);
            });
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
        return await AnsiConsole.Progress().HideCompleted(true).AutoClear(true).AutoRefresh(!_externalRefresh)
                                .StartAsync(
                                    async context =>
                                    {
                                        var scope = new SpectreProgressScope(
                                            context, maxTicks, message, externalRefresh: _externalRefresh);
                                        return await action(scope);
                                    });
    }

    /// <inheritdoc />
    public async Task<T> CreateProgressScopeAsync<T>(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Func<IProgressScope, Task<T>> action)
    {
        return await AnsiConsole.Progress().HideCompleted(true).AutoClear(true).AutoRefresh(!_externalRefresh)
                                .StartAsync(
                                    async context =>
                                    {
                                        var scope = new SpectreProgressScope(
                                            context, maxTicks, message, reportAtPercent, _externalRefresh);
                                        return await action(scope);
                                    });
    }
}