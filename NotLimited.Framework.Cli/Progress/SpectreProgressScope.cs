using NotLimited.Framework.Common.Progress;
using Spectre.Console;

namespace NotLimited.Framework.Cli.Progress;

/// <summary>
/// Progress reporter for Spectre.Console.
/// </summary>
public class SpectreProgressScope : IProgressScope
{
    private readonly ProgressContext _context;
    private readonly bool _externalRefresh;
    private readonly PercentProgressTracker? _tracker;
    private readonly ProgressTask _task;

    private int _curTicks = 0;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="context">Spectre progress context.</param>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="reportAtPercent">If specified, reports only at discrete percent intervals, not every tracked event.</param>
    /// <param name="externalRefresh">Specifies whether progress bar should only be updated externally.</param>
    public SpectreProgressScope(
        ProgressContext context,
        int maxTicks,
        string? message,
        int? reportAtPercent = null,
        bool externalRefresh = false)
    {
        _context = context;
        _externalRefresh = externalRefresh;
        _tracker = reportAtPercent != null ? new PercentProgressTracker(maxTicks, reportAtPercent.Value) : null;
        _task = _context.AddTask(message ?? string.Empty, maxValue: maxTicks);

        MaxTicks = maxTicks;
        Message = message;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _task.StopTask();
    }

    /// <inheritdoc />
    public void Report(string? message = null)
    {
        Report(_curTicks + 1, message);
    }

    /// <inheritdoc />
    public void Report(int ticks, string? message = null)
    {
        _curTicks = ticks;
        if (_tracker != null)
        {
            if (_tracker.ShouldReport(_curTicks))
            {
                _task.Value = _curTicks;
                if (message != null && message != _task.Description)
                {
                    _task.Description = message;
                }

                if (_externalRefresh)
                {
                    _context.Refresh();
                }
            }
        }
        else
        {
            _task.Value = _curTicks;
            if (message != null && message != _task.Description)
            {
                _task.Description = message;
            }

            if (_externalRefresh)
            {
                _context.Refresh();
            }
        }
    }

    /// <inheritdoc />
    public int MaxTicks
    {
        get => (int)_task.MaxValue;
        set => _task.MaxValue = value;
    }

    /// <inheritdoc />
    public string? Message
    {
        get => _task.Description;
        set => _task.Description = value ?? string.Empty;
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
        var scope = new SpectreProgressScope(_context, maxTicks, message, externalRefresh: _externalRefresh);
        return action(scope);
    }

    /// <inheritdoc />
    public T CreateProgressScope<T>(int maxTicks, string? message, int reportAtPercent, Func<IProgressScope, T> action)
    {
        var scope = new SpectreProgressScope(_context, maxTicks, message, reportAtPercent, _externalRefresh);
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
        var scope = new SpectreProgressScope(_context, maxTicks, message, externalRefresh: _externalRefresh);
        return await action(scope);
    }

    /// <inheritdoc />
    public async Task<T> CreateProgressScopeAsync<T>(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Func<IProgressScope, Task<T>> action)
    {
        var scope = new SpectreProgressScope(_context, maxTicks, message, reportAtPercent, _externalRefresh);
        return await action(scope);
    }
}