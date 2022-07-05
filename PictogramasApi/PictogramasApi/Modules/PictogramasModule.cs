using Carter;
using Carter.Request;
using Carter.Response;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.NoSql;
using PictogramasApi.Mgmt.Sql.Interface;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PictogramasApi.Modules
{
    public class PictogramasModule : CarterModule
    {
        private readonly INeo4JMgmt _neo4JMgmt;
        private readonly ICategoriaMgmt _categoriaMgmt;
        private readonly IPictogramaMgmt _pictogramaMgmt;
        private readonly IHttpClientFactory _httpClientFactory;
        private IOptions<WebServicesConfig> WebServices { get; set; }

        public PictogramasModule(INeo4JMgmt neo4JMgmt, ICategoriaMgmt categoriaMgmt,
            IPictogramaMgmt pictogramaMgmt, IHttpClientFactory httpClientFactory, IOptions<WebServicesConfig> webServices)
        {
            _neo4JMgmt = neo4JMgmt;
            _categoriaMgmt = categoriaMgmt;
            _pictogramaMgmt = pictogramaMgmt;
            _httpClientFactory = httpClientFactory;
            WebServices = webServices;

            GetRelaciones();
            GetCategorias();
            GetPictogramaPorId();
            GetPictogramasDeArasaacYGuardarlos();
            GetPictogramaPorIdYGuardarlo();
            GetPictogramaPorNombre();
        }

        private void GetRelaciones()
        {
            Get("/", async (ctx) =>
            {
                var pictograma = await _neo4JMgmt.ObtenerPictograma(1);
                await ctx.Response.Negotiate("Funciona");
            });
        }

        private void GetCategorias()
        {
            Get("/categorias", async (ctx) =>
            {
                var categorias = await _categoriaMgmt.GetCategorias();
                await ctx.Response.Negotiate(categorias);
            });
        }

        private void GetPictogramasDeArasaacYGuardarlos()
        {
            Get("/pictogramas/guardar", async (ctx) =>
            {
                var pictogramas = await ObtenerPictogramasDeArasaac();

                if (pictogramas != null)
                {
                    List<Stream> pictogramasAsStreams = new List<Stream>();
                    foreach(var pictograma in pictogramas)
                    {
                        var pictogramaAsStream = await ObtenerPictogramaDeArasaac(pictograma._id);
                        pictogramasAsStreams.Add(pictogramaAsStream);
                    }
                    await ctx.Response.Negotiate(pictogramas);
                }
                else
                    await ctx.Response.Negotiate("Error obteniendo el pictograma");
            });
        }

        private void GetPictogramaPorId()
        {
            Get("/pictogramas/{id:int}", async (ctx) =>
            {
                var id = ctx.Request.RouteValues.As<int>("id");

                var pictograma = await ObtenerPictogramaDeArasaac(id);

                if (pictograma != null)
                {

                    await ctx.Response.FromStream(pictograma, $"image/png",
                        new ContentDisposition($"attachment;filename=pictograma.png"));
                }
                else
                    await ctx.Response.Negotiate("Error obteniendo el pictograma");
            });
        }

        private void GetPictogramaPorIdYGuardarlo()
        {
            Get("/pictogramas/{id:int}/guardar", async (ctx) =>
            {
                var id = ctx.Request.RouteValues.As<int>("id");

                var pictograma = await ObtenerPictogramaDeArasaac(id);

                if (pictograma != null)
                {

                    await ctx.Response.FromStream(pictograma, $"image/png",
                        new ContentDisposition($"attachment;filename=pictograma.png"));
                }
                else
                    await ctx.Response.Negotiate("Error obteniendo el pictograma");
            });
        }

        private void GetPictogramaPorNombre()
        {
            Get("/pictogramas/nombre/{palabra:minlength(1)}", async (ctx) =>
            {
                var palabra = ctx.Request.RouteValues.As<string>("palabra");

                var pictograma = await _pictogramaMgmt.ObtenerPictogramaPorPalabra(palabra);

                var pictogramaArasaac = await ObtenerPictogramaDeArasaac(pictograma.IdArasaac);

                if (pictogramaArasaac != null)
                {

                    await ctx.Response.FromStream(pictogramaArasaac, $"image/png",
                        new ContentDisposition($"attachment;filename=pictograma.png"));
                }
                else
                    await ctx.Response.Negotiate("Error obteniendo el pictograma");
            });
        }

        public async Task<List<Model.Responses.Pictograma>> ObtenerPictogramasDeArasaac()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{WebServices.Value.ArasaacUri}/api/pictograms/all/es")
                {
                    Headers = {}
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
                    Headers = {}
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
