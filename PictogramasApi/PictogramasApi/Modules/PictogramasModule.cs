using Carter;
using Carter.Request;
using Carter.Response;
using PictogramasApi.Jobs;
using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Mgmt.NoSql;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Services;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;

namespace PictogramasApi.Modules
{
    public class PictogramasModule : CarterModule
    {
        private readonly INeo4JMgmt _neo4JMgmt;
        private readonly ICategoriaMgmt _categoriaMgmt;
        private readonly IPictogramaMgmt _pictogramaMgmt;
        private readonly IStorageMgmt _storageMgmt;

        private readonly ActualizacionStorageJob _actualizacionStorageJob;
        private readonly ArasaacService _arasaacService;

        public PictogramasModule(INeo4JMgmt neo4JMgmt, ICategoriaMgmt categoriaMgmt, ArasaacService arasaacService,
            IPictogramaMgmt pictogramaMgmt, IStorageMgmt storageMgmt,ActualizacionStorageJob actualizacionStorageJob)
        {
            _neo4JMgmt = neo4JMgmt;
            _categoriaMgmt = categoriaMgmt;
            _pictogramaMgmt = pictogramaMgmt;
            _storageMgmt = storageMgmt;
            _actualizacionStorageJob = actualizacionStorageJob;
            _arasaacService = arasaacService;

            GetRelaciones();
            GetCategorias();
            GetPictogramaPorId();
            GetPictogramaPorNombre();

            GetPictogramaPorIdYGuardarlo();
            GetPictogramasDeArasaacYGuardarlos();

            GetPictogramaDelStorage();            

            DeletePictogramaDelStorage();
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
                //TODO: Cuando esto se implemente, el resto desaparece
                _actualizacionStorageJob.ActualizarPictogramas();

                var pictogramas = await _arasaacService.ObtenerPictogramasDeArasaac();

                if (pictogramas != null)
                {
                    List<Stream> pictogramasAsStreams = new List<Stream>();
                    foreach(var pictograma in pictogramas)
                    {
                        var pictogramaAsStream = await _arasaacService.ObtenerPictogramaDeArasaac(pictograma._id);
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

                var pictograma = await _arasaacService.ObtenerPictogramaDeArasaac(id);

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

                var pictograma = await _arasaacService.ObtenerPictogramaDeArasaac(id);

                if (pictograma != null)
                {
                    _storageMgmt.Guardar(pictograma, "pictograma");
                    await ctx.Response.FromStream(pictograma, $"image/png",
                        new ContentDisposition($"attachment;filename=pictograma.png"));
                }
                else
                    await ctx.Response.Negotiate("Error obteniendo el pictograma");
            });
        }

        private void GetPictogramaDelStorage()
        {
            Get("/pictogramas/{filename:minlength(1)}/obtener", async (ctx) =>
            {
                var filename = ctx.Request.RouteValues.As<string>("filename");

                var pictograma = _storageMgmt.Obtener(filename);

                if (pictograma != null)
                {                    
                    await ctx.Response.FromStream(pictograma, $"image/png",
                        new ContentDisposition($"attachment;filename=pictograma.png"));
                }
                else
                    await ctx.Response.Negotiate("Error obteniendo el pictograma");
            });
        }

        private void DeletePictogramaDelStorage()
        {
            Delete("/pictogramas/{filename:minlength(1)}", async (ctx) =>
            {
                var filename = ctx.Request.RouteValues.As<string>("filename");

                _storageMgmt.Borrar(filename);
                await ctx.Response.Negotiate("Pictograma eliminado");
            });
        }

        private void GetPictogramaPorNombre()
        {
            Get("/pictogramas/nombre/{palabra:minlength(1)}", async (ctx) =>
            {
                var palabra = ctx.Request.RouteValues.As<string>("palabra");

                var pictograma = await _pictogramaMgmt.ObtenerPictogramaPorPalabra(palabra);

                var pictogramaArasaac = await _arasaacService.ObtenerPictogramaDeArasaac(pictograma.IdArasaac);

                if (pictogramaArasaac != null)
                {

                    await ctx.Response.FromStream(pictogramaArasaac, $"image/png",
                        new ContentDisposition($"attachment;filename=pictograma.png"));
                }
                else
                    await ctx.Response.Negotiate("Error obteniendo el pictograma");
            });
        }
    }
}
