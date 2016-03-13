using System;
using System.Threading.Tasks;

namespace NotLimited.Framework.Common.Helpers
{
	public static class TaskHelpers
	{
		public static void MuteExceptions(this Task task)
		{
			if (task.Exception != null)
				task.Exception.Handle(x => true);
		}

		public static void HandleException(this Task task, Action<Exception> handler)
		{
			task.ContinueWith(result =>
			{
				if (result.Exception != null)
				{
					result.Exception.Handle(exception =>
					{
						handler(exception);
						return true;
					});
				}
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}
	}
}