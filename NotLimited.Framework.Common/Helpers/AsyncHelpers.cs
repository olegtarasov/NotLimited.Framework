using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Async helpers.
/// </summary>
public static class AsyncHelpers
{
	/// <summary>
	/// Executes an async Task method which has a void return value synchronously
	/// </summary>
	/// <param name="task">Task method to execute</param>
	public static void RunSync(Func<Task> task)
	{
		var oldContext = SynchronizationContext.Current;
		var synch = new ExclusiveSynchronizationContext();
		SynchronizationContext.SetSynchronizationContext(synch);
		
		// We are catching all exceptions inside the lambda, so we can safely ignore the warning.
		// ReSharper disable once AsyncVoidLambda
		synch.Post(async _ =>
		           {
			           try
			           {
				           await task();
			           }
			           catch (Exception e)
			           {
				           synch.InnerException = e;
				           throw;
			           }
			           finally
			           {
				           synch.EndMessageLoop();
			           }
		           }, null);
		synch.BeginMessageLoop();

		SynchronizationContext.SetSynchronizationContext(oldContext);
	}

	/// <summary>
	/// Executes an async Task method which has a T return type synchronously
	/// </summary>
	/// <typeparam name="T">Return Type</typeparam>
	/// <param name="task">Task method to execute</param>
	/// <returns></returns>
	public static T? RunSync<T>(Func<Task<T>> task)
	{
		var oldContext = SynchronizationContext.Current;
		var synch = new ExclusiveSynchronizationContext();
		SynchronizationContext.SetSynchronizationContext(synch);
		var ret = default(T);

		// We are catching all exceptions inside the lambda, so we can safely ignore the warning.
		// ReSharper disable once AsyncVoidLambda
		synch.Post(async _ =>
		           {
			           try
			           {
				           ret = await task();
			           }
			           catch (Exception e)
			           {
				           synch.InnerException = e;
				           throw;
			           }
			           finally
			           {
				           synch.EndMessageLoop();
			           }
		           }, null);
		synch.BeginMessageLoop();
		SynchronizationContext.SetSynchronizationContext(oldContext);
		return ret;
	}

	private class ExclusiveSynchronizationContext : SynchronizationContext
	{
		private readonly AutoResetEvent _workItemsWaiting = new(false);
		private readonly Queue<(SendOrPostCallback, object?)> _items = new();

		private bool _done;

		public Exception? InnerException { get; set; }

		public override void Send(SendOrPostCallback d, object? state)
		{
			throw new NotSupportedException("We cannot send to our same thread");
		}

		public override void Post(SendOrPostCallback d, object? state)
		{
			lock (_items)
			{
				_items.Enqueue((d, state));
			}
			_workItemsWaiting.Set();
		}

		public void EndMessageLoop()
		{
			Post(_ => _done = true, null);
		}

		public void BeginMessageLoop()
		{
			while (!_done)
			{
				ValueTuple<SendOrPostCallback, object?>? task = null;
				lock (_items)
				{
					if (_items.Count > 0)
					{
						task = _items.Dequeue();
					}
				}
				if (task != null)
				{
					task.Value.Item1(task.Value.Item2);
					if (InnerException != null) // the method threw an exeption
					{
						throw new AggregateException("AsyncHelpers.Run method threw an exception.", InnerException);
					}
				}
				else
				{
					_workItemsWaiting.WaitOne();
				}
			}
		}

		public override SynchronizationContext CreateCopy()
		{
			return this;
		}
	}
}