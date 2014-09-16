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

		public static T Sync<T>(this Task<T> task)
		{
			task.Wait();
			return task.Result;
		}
	}
}