using System.Web.Hosting;

namespace NotLimited.Framework.Server.Services
{
	public class DefaultHostingService : IHostingService
	{
		public string MapPath(string path)
		{
			return HostingEnvironment.MapPath(path);
		}
	}
}