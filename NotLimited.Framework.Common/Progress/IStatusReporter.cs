namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Adds an ability to create status reporting scopes.
/// </summary>
public interface IStatusReporter
{
    /// <summary>
    /// Create a status reporting scope.
    /// </summary>
    /// <param name="message">Status message.</param>
    /// <param name="action">An action to perform on <see cref="IStatusScope"/>.</param>
    void CreateStatusScope(string message, Action<IStatusScope> action);

    /// <summary>
    /// Create a status reporting scope.
    /// </summary>
    /// <param name="message">Status message.</param>
    /// <param name="spinner">Spinner animation</param>
    /// <param name="action">An action to perform on <see cref="IStatusScope"/>.</param>
    void CreateStatusScope(string message, string spinner, Action<IStatusScope> action);

    /// <summary>
    /// Create a status reporting scope.
    /// </summary>
    /// <param name="message">Status message.</param>
    /// <param name="action">An action to perform on <see cref="IStatusScope"/>.</param>
    T CreateStatusScope<T>(string message, Func<IStatusScope, T> action);

    /// <summary>
    /// Create a status reporting scope.
    /// </summary>
    /// <param name="message">Status message.</param>
    /// <param name="spinner">Spinner animation</param>
    /// <param name="action">An action to perform on <see cref="IStatusScope"/>.</param>
    T CreateStatusScope<T>(string message, string spinner, Func<IStatusScope, T> action);

    /// <summary>
    /// Create a status reporting scope.
    /// </summary>
    /// <param name="message">Status message.</param>
    /// <param name="action">An action to perform on <see cref="IStatusScope"/>.</param>
    Task CreateStatusScopeAsync(string message, Func<IStatusScope, Task> action);

    /// <summary>
    /// Create a status reporting scope.
    /// </summary>
    /// <param name="message">Status message.</param>
    /// <param name="spinner">Spinner animation.</param>
    /// <param name="action">An action to perform on <see cref="IStatusScope"/>.</param>
    Task CreateStatusScopeAsync(string message, string spinner, Func<IStatusScope, Task> action);
    

    /// <summary>
    /// Create a status reporting scope.
    /// </summary>
    /// <param name="message">Status message.</param>
    /// <param name="action">An action to perform on <see cref="IStatusScope"/>.</param>
    Task<T> CreateStatusScopeAsync<T>(string message, Func<IStatusScope, Task<T>> action);

    /// <summary>
    /// Create a status reporting scope.
    /// </summary>
    /// <param name="message">Status message.</param>
    /// <param name="spinner">Spinner animation.</param>
    /// <param name="action">An action to perform on <see cref="IStatusScope"/>.</param>
    Task<T> CreateStatusScopeAsync<T>(string message, string spinner, Func<IStatusScope, Task<T>> action);
}