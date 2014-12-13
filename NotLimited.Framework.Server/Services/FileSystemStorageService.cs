using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using Microsoft.WindowsAzure.Storage;

namespace NotLimited.Framework.Server.Services
{
    public class FileSystemStorageService : StorageServiceBase
    {
        private readonly string _serverRoot;

        public FileSystemStorageService()
        {
            _serverRoot = HostingEnvironment.MapPath("~/");
            if (string.IsNullOrEmpty(_serverRoot))
                throw new InvalidOperationException("Can't get server root!");
        }

        public override void ClearDirectory(string path)
        {
            if (!Directory.Exists(path))
                return;

            foreach (var file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                ClearDirectory(directory);
            }
        }

        public override string GetCombinedUrl(params string[] paths)
        {
            return "~/" + paths.Aggregate((s, s1) => s + "/" + s1);
        }

        public override string CombineServerPath(params string[] paths)
        {
            string[] p = new string[paths.Length + 1];

            p[0] = _serverRoot;
            paths.CopyTo(p, 1);

            return Path.Combine(p);
        }

        public override void StoreFile(Stream stream, string path)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fs);
            }
        }

        public override bool RemoveFile(string path)
        {
            bool result = File.Exists(path);
            if (result)
            {
                try
                {
                    File.Delete(path);
                }
                catch
                {
                    return false;
                }
            }

            return result;
        }

        protected override bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}