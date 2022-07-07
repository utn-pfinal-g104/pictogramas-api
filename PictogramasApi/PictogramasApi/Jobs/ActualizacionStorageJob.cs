using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PictogramasApi.Jobs
{
    public class ActualizacionStorageJob : IJob
    {
        private readonly IPictogramaMgmt _pictogramaMgmt;
        private readonly IStorageMgmt _storageMgmt;
        private readonly ICategoriaMgmt _categoriaMgmt;
        private readonly IPalabraClaveMgmt _palabraClaveMgmt;
        private readonly ITagMgmt _tagMgmt;
        private readonly IPictogramaPorTagMgmt _pictogramaPorTagMgmt;
        private readonly IPictogramaPorCategoriaMgmt _pictogramaPorCategoriaMgmt;
        private readonly IUsuarioMgmt _usuarioMgmt;

        private readonly IHttpClientFactory _httpClientFactory;
        private IOptions<WebServicesConfig> WebServices { get; set; }

        public ActualizacionStorageJob(IPictogramaMgmt pictogramaMgmt, ICategoriaMgmt categoriaMgmt,
            IPalabraClaveMgmt palabraClaveMgmt, ITagMgmt tagMgmt, IPictogramaPorTagMgmt pictogramaPorTagMgmt,
            IPictogramaPorCategoriaMgmt pictogramaPorCategoriaMgmt, IHttpClientFactory httpClientFactory,
            IUsuarioMgmt usuarioMgmt, IStorageMgmt storageMgmt, IOptions<WebServicesConfig> webServices)
        {
            _pictogramaMgmt = pictogramaMgmt;
            _storageMgmt = storageMgmt;
            _categoriaMgmt = categoriaMgmt;
            _palabraClaveMgmt = palabraClaveMgmt;
            _tagMgmt = tagMgmt;
            _pictogramaPorTagMgmt = pictogramaPorTagMgmt;
            _pictogramaPorCategoriaMgmt = pictogramaPorCategoriaMgmt;
            _usuarioMgmt = usuarioMgmt;

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

            List<Model.Responses.Pictograma> pictogramasArasaac = await ObtenerPictogramasDeArasaac();

            List<Pictograma> pictogramas = MapearPictogramas(pictogramasArasaac);
            HashSet<string> categorias = ObtenerCategorias(pictogramasArasaac);
            HashSet<string> tags = ObtenerTags(pictogramasArasaac);
            List<PalabraClave> palabrasClaves = ObtenerPalabrasClaves(pictogramasArasaac);

            //TODO: Armar Bulk Inserts

            List<Stream> pictogramasAsStreams = new List<Stream>();
            foreach (var pictograma in pictogramasArasaac)
            {
                var pictogramaAsStream = await ObtenerPictogramaDeArasaac(pictograma._id);
                pictogramasAsStreams.Add(pictogramaAsStream);
            }
        }

        private List<Pictograma> MapearPictogramas(List<Model.Responses.Pictograma> pictogramasArasaac)
        {
            List<Pictograma> pictogramas = new List<Pictograma>();

            foreach(var pictograma in pictogramasArasaac){
                pictogramas.Add(new Pictograma
                {
                    Aac = pictograma.aac,
                    AacColor = pictograma.aacColor,
                    Hair = pictograma.hair,
                    IdArasaac = pictograma._id,
                    Schematic = pictograma.schematic,
                    Sex = pictograma.sex,
                    Skin = pictograma.skin,
                    Violence = pictograma.violence
                });
            }

            return pictogramas;
        }

        private List<PalabraClave> ObtenerPalabrasClaves(List<Model.Responses.Pictograma> pictogramas)
        {
            List<PalabraClave> palabrasClaves = new List<PalabraClave>();
            foreach (var pictograma in pictogramas)
            {
                foreach (var palabraClave in pictograma.keywords)
                    palabrasClaves.Add(new PalabraClave
                    { 
                        HasLocution = palabraClave.hasLocution,
                        IdPictograma = pictograma._id,
                        Keyword = palabraClave.keyword,
                        Meaning = palabraClave.meaning,
                        Plural = palabraClave.plural,
                        Tipo = palabraClave.type
                    });
            }
            return palabrasClaves;
        }

        private HashSet<string> ObtenerTags(List<Model.Responses.Pictograma> pictogramas)
        {
            HashSet<string> tags = new HashSet<string>();
            foreach (var pictograma in pictogramas)
            {
                foreach (var tag in pictograma.tags)
                    tags.Add(tag);
            }
            return tags;
        }

        private static HashSet<string> ObtenerCategorias(List<Model.Responses.Pictograma> pictogramas)
        {
            HashSet<string> categorias = new HashSet<string>();
            foreach (var pictograma in pictogramas)
            {
                foreach (var categoria in pictograma.categories)
                    categorias.Add(categoria);
            }
            return categorias;
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
