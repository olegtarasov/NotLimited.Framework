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

		public static async void HandleException(this Task task, Action<Exception> handler)
		{
			try
			{
				await task.ConfigureAwait(false);
			}
			catch (Exception e)
			{
				handler(e);
			}
		}
	}
}