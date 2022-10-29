using System.Reflection;
using NotLimited.Framework.Common.Progress;
using Spectre.Console;

namespace NotLimited.Framework.Cli.Progress;

/// <summary>
/// Status reporter that uses Spectre.Console.
/// </summary>
public class SpectreStatusReporter : IStatusReporter
{
    /// <inheritdoc />
    public T CreateStatusScope<T>(string message, string spinner, Func<IStatusScope, T> action)
    {
        return AnsiConsole.Status().Start(message, context =>
                                                   {
                                                       var scope = new SpectreStatusScope(context, spinner);
                                                       return action(scope);
                                                   });
    }

    /// <inheritdoc />
    public async Task<T> CreateStatusScopeAsync<T>(string message, string spinner, Func<IStatusScope, Task<T>> action)
    {
        return await AnsiConsole.Status().StartAsync(message, async context =>
                                                              {
                                                                  var scope = new SpectreStatusScope(context, spinner);
                                                                  return await action(scope);
                                                              });
    }

    /// <inheritdoc />
    public void CreateStatusScope(string message, Action<IStatusScope> action)
    {
        CreateStatusScope(message, "dots", action);
    }

    /// <inheritdoc />
    public void CreateStatusScope(string message, string spinner, Action<IStatusScope> action)
    {
        CreateStatusScope(message, spinner, scope =>
                                            {
                                                action(scope);
                                                return 0;
                                            });
    }

    /// <inheritdoc />
    public T CreateStatusScope<T>(string message, Func<IStatusScope, T> action)
    {
        return CreateStatusScope(message, "dots", action);
    }

    /// <inheritdoc />
    public Task CreateStatusScopeAsync(string message, Func<IStatusScope, Task> action)
    {
        return CreateStatusScopeAsync(message, "dots", action);
    }

    /// <inheritdoc />
    public Task CreateStatusScopeAsync(string message, string spinner, Func<IStatusScope, Task> action)
    {
        return CreateStatusScopeAsync(message, spinner, scope =>
                                                        {
                                                            action(scope);
                                                            return Task.FromResult(0);
                                                        });
    }

    /// <inheritdoc />
    public Task<T> CreateStatusScopeAsync<T>(string message, Func<IStatusScope, Task<T>> action)
    {
        return CreateStatusScopeAsync(message, "dots", action);
    }
}