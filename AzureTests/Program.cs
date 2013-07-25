using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace AzureTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerManager = new ContainerManager();

            containerManager.ListContainers();

            Console.WriteLine(" ");
            Console.WriteLine("Digite o nome do novo container");

            var novoContainer = Console.ReadLine();

            containerManager.CreateNewContainer(novoContainer);
        
            var blobManager = new BlobManager();

            blobManager.ListBlobs(novoContainer);

            var path = @"C:\Users\Public\Pictures\Sample Pictures\Penguins.jpg";
            var nomeArquivo = Path.GetFileName(path);
            
            using (Stream s = new FileStream(path, FileMode.Open))
            {
                var fileToBlob = new FileToBlob {FileName = nomeArquivo, Content = s};
                blobManager.AddBlob(novoContainer, fileToBlob);
            }

            blobManager.DeleteBlob(novoContainer, nomeArquivo);

            containerManager.DeleteContainer(novoContainer);

            Console.Read();
        }
    }
}
