using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PictogramasApi.Mgmt.CMS
{
    public class StorageMgmt : IStorageMgmt
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly BlobContainerClient container;

        public StorageMgmt(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("Storage:ConnectionString");
            container = new BlobContainerClient(_connectionString, _configuration.GetValue<string>("Storage:Container"));
        }

        public void Guardar(Stream file, string fileName)
        {
            BlobClient blob = container.GetBlobClient(fileName);
            blob.Upload(file);
        }

        public Stream Obtener(string filename)
        {
            BlobClient blob = container.GetBlobClient(filename);
            var stream = blob.OpenRead();
            return stream;
        }

        public void Borrar(string filename)
        {
            BlobClient blob = container.GetBlobClient(filename);
            blob.DeleteIfExists();
        }
    }
}
