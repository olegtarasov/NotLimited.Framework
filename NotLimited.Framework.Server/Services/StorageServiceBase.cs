using System;
using System.IO;

namespace NotLimited.Framework.Server.Services
{
    public abstract class StorageServiceBase : IStorageService
    {
        public string GetRandomFileName(string path, string ext = "")
        {
            string fileName;
            GetRandomFileNameWithPath(path, ext, out fileName);
            return fileName;
        }

        public string GetRandomFileNameWithPath(string path, string ext = "")
        {
            string fileName;
            return GetRandomFileNameWithPath(path, ext, out fileName);
        }

        public string GetRandomFileNameWithPath(string path, string ext, out string fileName)
        {
            string fullPath;

            do
            {
                fileName = Guid.NewGuid().ToString("N") + ext;
                fullPath = CombineServerPath(path, fileName);
            } while (FileExists(fullPath));

            return fullPath;
        }

        public abstract string GetCombinedUrl(params string[] paths);
        public abstract string CombineServerPath(params string[] paths);
        public abstract void StoreFile(Stream stream, string path);
        public abstract bool RemoveFile(string path);

        protected abstract bool FileExists(string path);
    }
}