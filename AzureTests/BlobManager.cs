using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureTests
{
    public class BlobManager
    {
        private readonly CloudStorageAccount _storageAccount = null;
        private readonly CloudBlobClient _blobStorageType = null;

        public BlobManager()
        {
            _storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            _blobStorageType = _storageAccount.CreateCloudBlobClient();
        }

        //Lista todos os blobs do container
        public void ListBlobs(string containerName)
        {
            var container = _blobStorageType.GetContainerReference(containerName);
            var blobs = container.ListBlobs();

            foreach (var blob in blobs)
            {
                Console.WriteLine(blob.Uri);
            }
        }

        //Adiciona um novo blob ao container
        public void AddBlob(string containerName, FileToBlob file)
        {
            var container = _blobStorageType.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(file.FileName);

            blob.UploadFromStream(file.Content);
        }

        //Exclui blob de um container
        public void DeleteBlob(string containerName, string blobName)
        {
            var container = _blobStorageType.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);
            
            blob.DeleteIfExists();
        }
    }
}
