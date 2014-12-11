using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace NotLimited.Framework.Server.Services
{
    public class AzureStorageService : StorageServiceBase
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobClient _client;
        private readonly CloudBlobContainer _container;
        private readonly string _containerUri;

        public AzureStorageService()
            : this(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString,
                   ConfigurationManager.AppSettings["AzureContainer"])
        {
        }

        public AzureStorageService(string connectionString, string containerName)
        {
            _storageAccount = CloudStorageAccount.Parse(connectionString);
            _client = _storageAccount.CreateCloudBlobClient();
            _container = _client.GetContainerReference(containerName);
            _container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);
            _containerUri = _container.Uri.AbsoluteUri + "/";
        }

        public override string GetCombinedUrl(params string[] paths)
        {
            return _containerUri + CombineServerPath(paths);
        }

        public override string CombineServerPath(params string[] paths)
        {
            return paths.Aggregate((s, s1) => s + "/" + s1);
        }

        public override void StoreFile(Stream stream, string path)
        {
            var blob = _container.GetBlockBlobReference(path);
            blob.UploadFromStream(stream);
        }

        public override bool RemoveFile(string path)
        {
            var blob = _container.GetBlockBlobReference(path);
            return blob.DeleteIfExists();
        }

        protected override bool FileExists(string path)
        {
            return _container.GetBlockBlobReference(path).Exists();
        }
    }
}