using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Mgmt.Sql.Interface;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PictogramasApi.Jobs
{
    public class ActualizacionStorageJob : IJob
    {
        private readonly IPictogramaMgmt _pictogramaMgmt;
        private readonly IStorageMgmt _storageMgmt;

        private readonly IHttpClientFactory _httpClientFactory;
        private IOptions<WebServicesConfig> WebServices { get; set; }

        public ActualizacionStorageJob(IPictogramaMgmt pictogramaMgmt, IHttpClientFactory httpClientFactory,
            IStorageMgmt storageMgmt, IOptions<WebServicesConfig> webServices)
        {
            _pictogramaMgmt = pictogramaMgmt;
            _storageMgmt = storageMgmt;

            _httpClientFactory = httpClientFactory;
            WebServices = webServices;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
            });
        }

        internal async void ActualizarPictogramas()
        {
            throw new NotImplementedException();

            var pictogramas = await ObtenerPictogramasDeArasaac();

            //TODO: Armar Bulk Inserts

            List<Stream> pictogramasAsStreams = new List<Stream>();
            foreach (var pictograma in pictogramas)
            {
                var pictogramaAsStream = await ObtenerPictogramaDeArasaac(pictograma._id);
                pictogramasAsStreams.Add(pictogramaAsStream);
            }
        }

        public async Task<List<Model.Responses.Pictograma>> ObtenerPictogramasDeArasaac()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{WebServices.Value.ArasaacUri}/api/pictograms/all/es")
            {
                Headers = { }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            string responseAsString;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                responseAsString = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseJson = JArray.Parse(responseAsString);
                var listaDePictogramas = responseJson.ToObject<List<Model.Responses.Pictograma>>();
                return listaDePictogramas;
            }
            else
                return null;
        }

        public async Task<Stream> ObtenerPictogramaDeArasaac(int id)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{WebServices.Value.ArasaacUri}/api/pictograms/{id}")
            {
                Headers = { }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            Stream contentStream;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                Stream copyStream = new MemoryStream();
                contentStream.CopyTo(copyStream);
                copyStream.Seek(0, SeekOrigin.Begin);
                return copyStream;
            }
            else
                return null;
        }
    }
}
