﻿using Carter;
using Carter.Request;
using Carter.Response;
using Microsoft.AspNetCore.Http;
using PictogramasApi.Jobs;
using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PictogramasApi.Modules
{
    public class PictogramasModule : CarterModule
    {
        private readonly IPictogramaMgmt _pictogramaMgmt;
        private readonly IStorageMgmt _storageMgmt;
        private readonly IPictogramaPorCategoriaMgmt _pictogramaPorCategoriaMgmt;
        private readonly IPictogramaPorTagMgmt _pictogramaPorTagMgmt;
        private readonly IPalabraClaveMgmt _palabraClaveMgmt;
        private readonly ICategoriaMgmt _categoriaMgmt;
        private readonly ITagMgmt _tagMgmt;

        private readonly ActualizacionStorageJob _actualizacionStorageJob;
        private readonly ArasaacService _arasaacService;

        public PictogramasModule(ArasaacService arasaacService, IPictogramaMgmt pictogramaMgmt, 
            IStorageMgmt storageMgmt, ActualizacionStorageJob actualizacionStorageJob, ICategoriaMgmt categoriaMgmt,
            IPictogramaPorTagMgmt pictogramaPorTagMgmt, IPalabraClaveMgmt palabraClaveMgmt,
            IPictogramaPorCategoriaMgmt pictogramaPorCategoriaMgmt, ITagMgmt tagMgmt) : base("/pictogramas")
        {
            _pictogramaMgmt = pictogramaMgmt;
            _storageMgmt = storageMgmt;
            _actualizacionStorageJob = actualizacionStorageJob;
            _arasaacService = arasaacService;
            _pictogramaPorCategoriaMgmt = pictogramaPorCategoriaMgmt;
            _pictogramaPorTagMgmt = pictogramaPorTagMgmt;
            _palabraClaveMgmt = palabraClaveMgmt;
            _categoriaMgmt = categoriaMgmt;
            _tagMgmt = tagMgmt;

            #region "Arasaac"
            GetPictogramaPorIdDeArasaac();
            GetPictogramaPorIdYGuardarlo();
            GetPictogramasDeArasaacYGuardarlos();
            #endregion "Arasaac"

            #region "Storage"
            GetPictogramaDelStorage();
            GetPictogramaPorKeyword();
            GetPictogramasPorNombreCategoria();
            GetPictogramasPorCategoriaId();
            GetPictogramasPorNombreDeTag();
            GetPictogramasPorTagId();

            DeletePictogramaDelStorage();
            #endregion "Storage"
        }

        private void GetPictogramasDeArasaacYGuardarlos()
        {
            Get("/guardar", async (ctx) =>
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

        private void GetPictogramaPorIdDeArasaac()
        {
            Get("/{id:int}", async (ctx) =>
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
            Get("/{id:int}/guardar", async (ctx) =>
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
            Get("/{filename:minlength(1)}/obtener", async (ctx) =>
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
            Delete("/{filename:minlength(1)}", async (ctx) =>
            {
                var filename = ctx.Request.RouteValues.As<string>("filename");

                _storageMgmt.Borrar(filename);
                await ctx.Response.Negotiate("Pictograma eliminado");
            });
        }

        private void GetPictogramaPorKeyword()
        {
            Get("/nombre/{palabra:minlength(1)}", async (ctx) =>
            {
                var palabra = ctx.Request.RouteValues.As<string>("palabra");

                var keyword = await _palabraClaveMgmt.ObtenerKeyword(palabra);               

                var pictograma = _storageMgmt.Obtener(keyword.IdPictograma.ToString());

                if (pictograma != null)
                {
                    await ctx.Response.FromStream(pictograma, $"image/png",
                        new ContentDisposition($"attachment;filename=pictograma.png"));
                }
                else
                    await ctx.Response.Negotiate("Error obteniendo el pictograma");
            });
        }

        private void GetPictogramasPorNombreCategoria()
        {
            Get("/categorias/nombre/{nombre:minlength(1)}", async (ctx) =>
            {
                var nombre = ctx.Request.RouteValues.As<string>("nombre");

                var categoria = await _categoriaMgmt.ObtenerCategoria(nombre);
                await ObtenerPictogramasPorCategoriaId(ctx, categoria.Id);
            });
        }

        private void GetPictogramasPorCategoriaId()
        {
            Get("/categorias/id/{categoria:int}", async (ctx) =>
            {
                var categoria = ctx.Request.RouteValues.As<int>("categoria");

                await ObtenerPictogramasPorCategoriaId(ctx, categoria);
            });
        }

        private void GetPictogramasPorNombreDeTag()
        {
            Get("/tags/nombre/{nombre:minlength(1)}", async (ctx) =>
            {
                var nombre = ctx.Request.RouteValues.As<string>("nombre");

                var tag = await _tagMgmt.ObtenerTag(nombre);
                await ObtenerPictogramasPorTagId(ctx, tag.Id);
            });
        }

        private void GetPictogramasPorTagId()
        {
            Get("/tags/id/{tag:int}", async (ctx) =>
            {
                var tag = ctx.Request.RouteValues.As<int>("tag");

                await ObtenerPictogramasPorTagId(ctx, tag);
            });
        }

        private async Task ObtenerPictogramasPorCategoriaId(HttpContext ctx, int categoria)
        {
            var picsXcat = await _pictogramaPorCategoriaMgmt.ObtenerPictogramasPorCategoria(categoria);
            var pictogramasIds = picsXcat.Select(p => p.IdPictograma).ToList();

            await ObtenerPictogramasPorIds(ctx, pictogramasIds);
        }

        private async Task ObtenerPictogramasPorTagId(HttpContext ctx, int tag)
        {
            var picsXtag = await _pictogramaPorTagMgmt.ObtenerPictogramasPorTag(tag);
            var pictogramasIds = picsXtag.Select(p => p.IdPictograma).ToList();

            await ObtenerPictogramasPorIds(ctx, pictogramasIds);
        }

        private async Task ObtenerPictogramasPorIds(HttpContext ctx, List<int> pictogramasIds)
        {
            var pictogramas = await _pictogramaMgmt.ObtenerPictogramas(pictogramasIds);

            if (pictogramas != null)
            {
                await ctx.Response.Negotiate(pictogramas);
            }
            else
                await ctx.Response.Negotiate("Error obteniendo el pictograma");
        }
    }
}
