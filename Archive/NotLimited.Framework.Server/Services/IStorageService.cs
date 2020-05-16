using System.IO;

namespace NotLimited.Framework.Server.Services
{
    public interface IStorageService
    {
        string GetCombinedUrl(params string[] paths);
        string CombineServerPath(params string[] paths);
        string GetRandomFileName(string path, string ext = "");
        string GetRandomFileNameWithPath(string path, string ext = "");
        void StoreFile(Stream stream, string path);
        bool RemoveFile(string path);
        string GetRandomFileNameWithPath(string path, string ext, out string fileName);
        void ClearDirectory(string path);
    }
}