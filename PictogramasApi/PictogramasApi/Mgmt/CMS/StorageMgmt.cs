using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.CMS
{
    public class StorageMgmt : IStorageMgmt
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly BlobContainerClient container;
        private readonly BlobContainerClient categoriasContainer;
        private readonly BlobContainerClient usuariosContainer;
        private readonly ILogger<StorageMgmt> _logger;

        public StorageMgmt(IConfiguration configuration, ILogger<StorageMgmt> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("Storage:ConnectionString");
            container = new BlobContainerClient(_connectionString, _configuration.GetValue<string>("Storage:Container"));
            container.CreateIfNotExists();
            categoriasContainer = new BlobContainerClient(_connectionString, "categorias");
            categoriasContainer.CreateIfNotExists();
            usuariosContainer = new BlobContainerClient(_connectionString, "usuarios");
            usuariosContainer.CreateIfNotExists();
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

        public void GuardarImagenUsuario(Stream file, string fileName)
        {
            BlobClient blob = usuariosContainer.GetBlobClient(fileName);
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

        public Stream ObtenerImagenUsuario(string filename)
        {
            BlobClient blob = usuariosContainer.GetBlobClient(filename);
            var stream = blob.OpenRead();
            return stream;
        }

        public void Borrar(string filename)
        {
            BlobClient blob = container.GetBlobClient(filename);
            blob.DeleteIfExists();
        }

        public async Task<List<string>> ObtenerTotalImagenesPictogramas()
        {
            List<string> archivos = new List<string>();

            try
            {

                var resultSegment = container.GetBlobsAsync()
                    .AsPages(default, 100);

                // Enumerate the blobs returned for each page.
                await foreach (Azure.Page<BlobItem> blobPage in resultSegment)
                {
                    try
                    {
                        foreach (BlobItem blobItem in blobPage.Values)
                        {
                            try
                            {
                                _logger.LogInformation($"Blob name: {blobItem.Name} - {DateTime.Now}");
                                archivos.Add(blobItem.Name);
                            }
                            catch(Exception ex)
                            {
                                _logger.LogError($"Ocurrio un error al obtener las imagenes totales de pictogramas en el Storage - 1:{ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Ocurrio un error al obtener las imagenes totales de pictogramas en el Storage - 2:{ex.Message}");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ocurrio un error al obtener las imagenes totales de pictogramas en el Storage - 3:{ex.Message}");
            }

            return archivos;
        }

        public void BorrarTodasLasImagenesPictogramas(List<string> archivos)
        {
            Parallel.ForEach(archivos, archivo =>
            {
                BlobClient blob = container.GetBlobClient(archivo);
                blob.DeleteIfExists();
            });
        }

        public async Task<List<string>> ObtenerTotalImagenesCategorias()
        {
            List<string> archivos = new List<string>();

            var resultSegment = categoriasContainer.GetBlobsAsync()
                .AsPages(default, 100);

            // Enumerate the blobs returned for each page.
            await foreach (Azure.Page<BlobItem> blobPage in resultSegment)
            {
                foreach (BlobItem blobItem in blobPage.Values)
                {
                    _logger.LogInformation($"Blob name: {blobItem.Name} - {DateTime.Now}");
                    archivos.Add(blobItem.Name);
                }
            }

            return archivos;
        }

        public void BorrarTodasLasImagenesCategorias(List<string> archivos)
        {
            Parallel.ForEach(archivos, archivo =>
            {
                BlobClient blob = categoriasContainer.GetBlobClient(archivo);
                blob.DeleteIfExists();
            });
        }
    }
}
