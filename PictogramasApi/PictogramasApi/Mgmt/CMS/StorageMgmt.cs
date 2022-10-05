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
        private readonly BlobContainerClient categoriasContainer;

        public StorageMgmt(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("Storage:ConnectionString");
            container = new BlobContainerClient(_connectionString, _configuration.GetValue<string>("Storage:Container"));
            categoriasContainer = new BlobContainerClient(_connectionString, "categorias");
        }

        public void Guardar(Stream file, string fileName)
        {
            BlobClient blob = container.GetBlobClient(fileName);
            blob.Upload(file);
            var fileUrl = blob.Uri.AbsoluteUri;
        }

        public void GuardarImagenCategoria(Stream file, string fileName)
        {
            BlobClient blob = categoriasContainer.GetBlobClient(fileName);
            blob.Upload(file);
            var fileUrl = blob.Uri.AbsoluteUri;
        }

        public Stream Obtener(string filename)
        {
            BlobClient blob = container.GetBlobClient(filename);
            var stream = blob.OpenRead();
            return stream;
        }

        public Stream ObtenerImagenCategoria(string filename)
        {
            BlobClient blob = categoriasContainer.GetBlobClient(filename);
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
