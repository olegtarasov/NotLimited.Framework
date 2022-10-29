namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Status reporter that uses plain system console. Spinners are not supported.
/// </summary>
public class ConsoleStatusReporter : IStatusReporter
{
    /// <inheritdoc />
    public T CreateStatusScope<T>(string message, Func<IStatusScope, T> action)
    {
        var scope = new ConsoleStatusScope();
        return action(scope);
    }

    /// <inheritdoc />
    public async Task<T> CreateStatusScopeAsync<T>(string message, Func<IStatusScope, Task<T>> action)
    {
        var scope = new ConsoleStatusScope();
        return await action(scope);
    }

    /// <inheritdoc />
    public void CreateStatusScope(string message, Action<IStatusScope> action)
    {
        CreateStatusScope(message, scope =>
                                   {
                                       action(scope);
                                       return 0;
                                   });
    }

    /// <inheritdoc />
    public void CreateStatusScope(string message, string spinner, Action<IStatusScope> action)
    {
        CreateStatusScope(message, action);
    }

    /// <inheritdoc />
    public T CreateStatusScope<T>(string message, string spinner, Func<IStatusScope, T> action)
    {
        return CreateStatusScope(message, action);
    }

    /// <inheritdoc />
    public Task CreateStatusScopeAsync(string message, Func<IStatusScope, Task> action)
    {
        return CreateStatusScopeAsync(message, scope =>
                                              {
                                                  action(scope);
                                                  return Task.FromResult(0);
                                              });
    }

    /// <inheritdoc />
    public Task CreateStatusScopeAsync(string message, string spinner, Func<IStatusScope, Task> action)
    {
        return CreateStatusScopeAsync(message, action);
    }

    /// <inheritdoc />
    public Task<T> CreateStatusScopeAsync<T>(string message, string spinner, Func<IStatusScope, Task<T>> action)
    {
        return CreateStatusScopeAsync(message, action);
    }
}