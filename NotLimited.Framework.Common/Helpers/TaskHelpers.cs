namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Helpers to work with tasks.
/// </summary>
public static class TaskHelpers
{
    /// <summary>
    /// Attach a continuation to a task that will inspect task exceptions, flatten them and throw
    /// using current SynchronizationContext if it's available.
    /// </summary>
    /// <param name="task">Task to attach to.</param>
    public static void HandleExceptions(this Task task)
    {
        var action = new Action<Task>(t =>
                                      {
                                          if (t.Exception != null)
                                              throw t.Exception.Flatten();
                                      });

        if (SynchronizationContext.Current != null)
        {
            task.ContinueWith(action, default, TaskContinuationOptions.OnlyOnFaulted,
                              TaskScheduler.FromCurrentSynchronizationContext());
        }
        else
        {
            task.ContinueWith(action, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}