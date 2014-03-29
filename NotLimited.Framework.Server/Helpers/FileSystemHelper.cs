using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace NotLimited.Framework.Server.Helpers
{
	public sealed class FileSystemHelper
	{
		private readonly string _serverRoot;

		public FileSystemHelper()
		{
			_serverRoot = HostingEnvironment.MapPath("~/");
			if (string.IsNullOrEmpty(_serverRoot))
				throw new InvalidOperationException("Can't get server root!");
		}

		public string CombineServerPath(params string[] paths)
		{
			string[] p = new string[paths.Length + 1];

			p[0] = _serverRoot;
			paths.CopyTo(p, 1);

			return Path.Combine(p);
		}

		public string GetRandomFileName(string path, string ext = "")
		{
			string fileName;

			do
			{
				fileName = Guid.NewGuid().ToString("N") + ext;
			} while (File.Exists(CombineServerPath(path, fileName)));

			return fileName;
		}

		public string StoreFile(Stream stream, string path, string originalName)
		{
			string fileName = GetRandomFileName(path, originalName.Substring(originalName.LastIndexOf('.')));

			using (var fs = new FileStream(CombineServerPath(path, fileName), FileMode.Create, FileAccess.Write))
			{
				stream.CopyTo(fs);
			}

			return fileName;
		}
	}

}