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
	}
}