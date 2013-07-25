using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureTests
{
    public class ContainerManager
    {
        private readonly CloudStorageAccount _storageAccount = null;
        private readonly CloudBlobClient _blobStorageType = null;
        
        public ContainerManager()
        {
            _storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            _blobStorageType = _storageAccount.CreateCloudBlobClient();
        }

        public void ListContainers()
        {
            IEnumerable<CloudBlobContainer> containers = _blobStorageType.ListContainers();
            foreach (var container in containers)
            {
                Console.WriteLine(string.Format("name:{0} \n uri:{1}", container.Name, container.Uri));
            }
        }

        public void CreateNewContainer(string novoContainer)
        {
            if (string.IsNullOrEmpty(novoContainer) == false)
            {
                var container = _blobStorageType.GetContainerReference(novoContainer);
                container.CreateIfNotExists();

                Console.WriteLine("Container criado!");
                Console.WriteLine(container.Uri);
            }
        }

        public void DeleteContainer(string novoContainer)
        {
            _blobStorageType.GetContainerReference(novoContainer).DeleteIfExists();

            Console.WriteLine("Container Excluído!");
        }
    }
}