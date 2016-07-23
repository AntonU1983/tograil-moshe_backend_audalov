using System;
using System.Threading;
using System.Threading.Tasks;
using GenericBackend.Core.Utils;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GenericBackend.Core.Images
{
    public class StorageManager
    {
        readonly CloudStorageAccount _storageAccount = CloudStorageAccount.Parse(AppSettingsHelper.StorageConnectionString);

        public async Task<Uri> UploadFromStreamAsync(string containerName, string blobName, string filePath)
        {
            
            var blobClient = _storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            var blockBlob = container.GetBlockBlobReference(blobName);
            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                await blockBlob.UploadFromStreamAsync(fileStream, CancellationToken.None);
            }
            return blockBlob.Uri;
        }
        public CloudBlobContainer AddOrGetContainer(string name)
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(name);
            if (!container.Exists())
            {
                // Create the container if it doesn't already exist.
                container.CreateIfNotExists();
                var permissions = new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob};
                container.SetPermissions(permissions);
            }
            
            return container;
        }
    }
}