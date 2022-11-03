namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Adds an ability to create progress reporting scopes.
/// </summary>
public interface IProgressReporter
{
    /// <summary>
    /// Create a progress reporting scope. 
    /// </summary>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="action">An action to perform on <see cref="IProgressScope"/>.</param>
    void CreateProgressScope(int maxTicks, string? message, Action<IProgressScope> action);

    /// <summary>
    /// Create a progress reporting scope. 
    /// </summary>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="reportAtPercent">Report only at discrete percent intervals, not every tracked event.</param>
    /// <param name="action">An action to perform on <see cref="IProgressScope"/>.</param>
    void CreateProgressScope(int maxTicks, string? message, int reportAtPercent, Action<IProgressScope> action);

    /// <summary>
    /// Create a progress reporting scope. 
    /// </summary>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="action">An action to perform on <see cref="IProgressScope"/>.</param>
    T CreateProgressScope<T>(int maxTicks, string? message, Func<IProgressScope, T> action);

    /// <summary>
    /// Create a progress reporting scope. 
    /// </summary>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="reportAtPercent">Report only at discrete percent intervals, not every tracked event.</param>
    /// <param name="action">An action to perform on <see cref="IProgressScope"/>.</param>
    T CreateProgressScope<T>(int maxTicks, string? message, int reportAtPercent, Func<IProgressScope, T> action);

    /// <summary>
    /// Create a progress reporting scope. 
    /// </summary>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="action">An action to perform on <see cref="IProgressScope"/>.</param>
    Task CreateProgressScopeAsync(int maxTicks, string? message, Func<IProgressScope, Task> action);

    /// <summary>
    /// Create a progress reporting scope. 
    /// </summary>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="reportAtPercent">Report only at discrete percent intervals, not every tracked event.</param>
    /// <param name="action">An action to perform on <see cref="IProgressScope"/>.</param>
    Task CreateProgressScopeAsync(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Func<IProgressScope, Task> action);

    /// <summary>
    /// Create a progress reporting scope. 
    /// </summary>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="action">An action to perform on <see cref="IProgressScope"/>.</param>
    Task<T> CreateProgressScopeAsync<T>(int maxTicks, string? message, Func<IProgressScope, Task<T>> action);

    /// <summary>
    /// Create a progress reporting scope. 
    /// </summary>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="reportAtPercent">Report only at discrete percent intervals, not every tracked event.</param>
    /// <param name="action">An action to perform on <see cref="IProgressScope"/>.</param>
    Task<T> CreateProgressScopeAsync<T>(
        int maxTicks,
        string? message,
        int reportAtPercent,
        Func<IProgressScope, Task<T>> action);
}