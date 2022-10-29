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
        AnsiConsole.Progress().HideCompleted(true).AutoClear(true).AutoRefresh(!_externalRefresh).Start(
            context =>
            {
                var scope = new SpectreProgressScope(context, maxTicks, message, externalRefresh: _externalRefresh);
                action(scope);
            });
    }

    /// <inheritdoc />
    public void CreateProgressScope(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Action<IProgressScope> action)
    {
        AnsiConsole.Progress().HideCompleted(true).AutoClear(true).AutoRefresh(!_externalRefresh).Start(
            context =>
            {
                var scope = new SpectreProgressScope(context, maxTicks, message, reportAtPercent, _externalRefresh);
                action(scope);
            });
    }

    /// <inheritdoc />
    public async Task CreateProgressScopeAsync(int maxTicks, string? message, Func<IProgressScope, Task> action)
    {
        await AnsiConsole.Progress().HideCompleted(true).AutoClear(true).AutoRefresh(!_externalRefresh).StartAsync(
            async context =>
            {
                var scope = new SpectreProgressScope(context, maxTicks, message, externalRefresh: _externalRefresh);
                await action(scope);
            });
    }

    /// <inheritdoc />
    public async Task CreateProgressScopeAsync(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Func<IProgressScope, Task> action)
    {
        await AnsiConsole.Progress().HideCompleted(true).AutoClear(true).AutoRefresh(!_externalRefresh).StartAsync(
            async context =>
            {
                var scope = new SpectreProgressScope(context, maxTicks, message, reportAtPercent, _externalRefresh);
                await action(scope);
            });
    }
}