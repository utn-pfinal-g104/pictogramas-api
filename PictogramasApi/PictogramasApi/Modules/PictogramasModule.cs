using Carter;
using Carter.ModelBinding;
using Carter.Request;
using Carter.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PictogramasApi.Jobs;
using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using PictogramasApi.Model.Requests;
using PictogramasApi.Services;
using PictogramasApi.Utils;
using System;
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
        private readonly IPalabraClaveMgmt _palabraClaveMgmt;
        private readonly ICategoriaMgmt _categoriaMgmt;

        private readonly ActualizacionStorageJob _actualizacionStorageJob;
        private readonly ArasaacService _arasaacService;
        private readonly ILogger<PictogramasModule> _logger;

        public PictogramasModule(ArasaacService arasaacService, IPictogramaMgmt pictogramaMgmt, 
            IStorageMgmt storageMgmt, ActualizacionStorageJob actualizacionStorageJob, ICategoriaMgmt categoriaMgmt,
            IPalabraClaveMgmt palabraClaveMgmt, ILogger<PictogramasModule> logger,
            IPictogramaPorCategoriaMgmt pictogramaPorCategoriaMgmt) : base("/pictogramas")
        {
            _pictogramaMgmt = pictogramaMgmt;
            _storageMgmt = storageMgmt;
            _actualizacionStorageJob = actualizacionStorageJob;
            _arasaacService = arasaacService;
            _pictogramaPorCategoriaMgmt = pictogramaPorCategoriaMgmt;
            _palabraClaveMgmt = palabraClaveMgmt;
            _categoriaMgmt = categoriaMgmt;
            _logger = logger;

            #region "Arasaac"
            GetPictogramaPorIdDeArasaac();
            GetPictogramaPorIdYGuardarlo();
            GetPictogramasDeArasaacYGuardarlos();
            #endregion "Arasaac"

            #region "BD"
            GetTotalPictogramas();
            GetInformacionPictogramas();
            DeletePictogramaDeUsuario();
            GetFavoritos();
            InsertFavorito();
            DeleteFavorito();
            DeletePictogramaPropio();

            #endregion "BD"

            #region "Storage"
            GetPictogramaDelStorage();
            GetPictogramaDelStorageAsBase64();
            GetPictogramaPorKeyword();
            GetPictogramasPorNombreCategoria();
            GetPictogramasPorCategoriaId();
            GetTotalPictogramasGuardados();

            PostPictogramaPropio();
            DeletePictogramaDelStorage();
            BorrarImagenesDeStoragePorRango();
            #endregion "Storage"
        }

        private void GetTotalPictogramasGuardados()
        {
            Get("/imagenes/total", async (ctx) =>
            {
                var pictogramasSubidosArasaac = (await _storageMgmt.ObtenerTotalImagenesPictogramas()).Where(p => !p.Contains("_"));
                await ctx.Response.Negotiate(pictogramasSubidosArasaac);
            });
        }

        private void PostPictogramaPropio()
        {
            Post("/imagen", async (ctx) =>
            {
                var request = await ctx.Request.Bind<Pictograma>();
                var id = ctx.Request.RouteValues.As<int>("id");
                var imagen = Parser.ConvertFromBase64(request.Imagen);
                _storageMgmt.Guardar(imagen, id.ToString());
                await ctx.Response.Negotiate("Creado");
            });
        }

        private void DeletePictogramaPropio()
        {
            Delete("/propios/{usuario:int}/{identificador:minlength(1)}", async (ctx) =>
            {
                var usuario = ctx.Request.RouteValues.As<int>("usuario");
                var identificador = ctx.Request.RouteValues.As<string>("identificador");

                var pictograma = _pictogramaMgmt.ObtenerPictogramaPropio(usuario, identificador);
                if (pictograma != null)
                {
                    _palabraClaveMgmt.EliminarPalabraClave(pictograma.Id);
                    await _pictogramaMgmt.EliminarPictogramaDeUsuario(pictograma.Id);
                    _storageMgmt.Borrar(pictograma.Id.ToString());
                }
                await ctx.Response.Negotiate("");
            });
        }

        private void GetInformacionPictogramas()
        {
            Get("/informacion", async (ctx) =>
            {
                List<Pictograma> pictogramas;
                if (Int32.TryParse(ctx.Request.Query["UsuarioId"], out int usuarioId))
                    pictogramas = await _pictogramaMgmt.ObtenerInformacionPictogramas(usuarioId);
                else
                    pictogramas = await _pictogramaMgmt.ObtenerInformacionPictogramas(null);
                await ctx.Response.Negotiate(pictogramas);
            });
        }

        private void GetPictogramasDeArasaacYGuardarlos()
        {
            Get("/guardar", async (ctx) =>
            {
                await _actualizacionStorageJob.ActualizarPictogramas();
                await ctx.Response.Negotiate("Pictogramas actualizados");
            });
        }

        private void BorrarImagenesDeStoragePorRango()
        {
            Get("/desde/{desde:int}/hasta/{hasta:int}", async (ctx) =>
            {
                var desde = ctx.Request.RouteValues.As<int>("desde");
                var hasta = ctx.Request.RouteValues.As<int>("hasta");
                for(int i = desde; i <= hasta; i++)
                {
                    _storageMgmt.Borrar(i.ToString());
                }
                await ctx.Response.Negotiate("Pictogramas actualizados");
            });
        }

        private void GetTotalPictogramas()
        {
            Get("/total", async (ctx) =>
            {
                int total;
                if (Int32.TryParse(ctx.Request.Query["UsuarioId"], out int usuarioId))
                    total = await _pictogramaMgmt.ObtenerTotalPictogramas(usuarioId);
                else
                    total = await _pictogramaMgmt.ObtenerTotalPictogramas();
                await ctx.Response.Negotiate(total);
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
                {
                    ctx.Response.StatusCode = 404;
                    await ctx.Response.Negotiate("No existe el pictograma");
                }
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
                {
                    ctx.Response.StatusCode = 404;
                    await ctx.Response.Negotiate("No existe el pictograma");
                }
            });
        }

        private void GetPictogramaDelStorageAsBase64()
        {
            Get("/{filename:minlength(1)}/obtener/base64", async (ctx) =>
            {
                try
                {
                    var filename = ctx.Request.RouteValues.As<string>("filename");

                    var pictograma = _storageMgmt.Obtener(filename);

                    if (pictograma != null)
                    {
                        var stringEnBase64 = Parser.ConvertToBase64(pictograma);
                        await ctx.Response.Negotiate(stringEnBase64);
                    }
                    else
                    {
                        ctx.Response.StatusCode = 404;
                        await ctx.Response.Negotiate("No existe el pictograma");
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Error al obtener pictograma del storage: {ex.Message}");
                    await ctx.Response.Negotiate($"Error al descargar pictograma: {ex.Message}");
                }
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

                if (keyword != null)
                {
                    var pictograma = _storageMgmt.Obtener(keyword.IdPictograma.ToString());
                    await ctx.Response.FromStream(pictograma, $"image/png",
                        new ContentDisposition($"attachment;filename=pictograma.png"));
                }
                else
                {
                    ctx.Response.StatusCode = 404;
                    await ctx.Response.Negotiate("No existe pictograma asociado a esa palabra");
                }
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

        private async Task ObtenerPictogramasPorCategoriaId(HttpContext ctx, int categoria)
        {
            var picsXcat = await _pictogramaPorCategoriaMgmt.ObtenerPictogramasPorCategoria(categoria);
            var pictogramasIds = picsXcat.Select(p => p.IdPictograma).ToList();

            await ObtenerPictogramasPorIds(ctx, pictogramasIds);
        }

        private async Task ObtenerPictogramasPorIds(HttpContext ctx, List<int> pictogramasIds)
        {
            var pictogramas = await _pictogramaMgmt.ObtenerPictogramasPorIds(pictogramasIds);

            if (pictogramas != null)
            {
                await ctx.Response.Negotiate(pictogramas);
            }
            else
                await ctx.Response.Negotiate("Error obteniendo el pictograma");
        }

        private void DeletePictogramaDeUsuario()
        {
            Delete("/pictogramasDeUsuario/{idPictogramaUsuario:minlength(1)}", async (ctx) =>
            {
                var idPictogramaUsuario = ctx.Request.RouteValues.As<int>("idPictogramaUsuario");

                await _pictogramaMgmt.EliminarPictogramaDeUsuario(idPictogramaUsuario);
                await ctx.Response.Negotiate("Pictograma eliminado de la base de datos");
            });            
        }

        private void GetFavoritos()
        {
            Get("/favoritos/{idUsuario:minlength(1)}/", async (ctx) =>
            {
                var idUsuario = ctx.Request.RouteValues.As<int>("idUsuario");

                var favoritos = await _pictogramaMgmt.ObtenerFavoritos(idUsuario);
                
                if (favoritos != null)
                {
                    await ctx.Response.Negotiate(favoritos);
                } 
                else
                {
                    await ctx.Response.Negotiate("Error obteniendo los pictogramas favoritos");
                }
            });
        }

        private void InsertFavorito()
        {
            Post("/favoritos/{idUsuario:minlength(1)}/{idPictograma:minlength(1)}", async (ctx) =>
            {
                var idPictograma = ctx.Request.RouteValues.As<int>("idPictograma");
                var idUsuario = ctx.Request.RouteValues.As<int>("idUsuario");

                await _pictogramaMgmt.AgregarFavorito(idUsuario, idPictograma);
                await ctx.Response.Negotiate("favorito guardado");
            });
        }

        private void DeleteFavorito()
        {
            Delete("/favoritos/{idUsuario:minlength(1)}/{idPictograma:minlength(1)}", async (ctx) =>
            {
                var idPictograma = ctx.Request.RouteValues.As<int>("idPictograma");
                var idUsuario = ctx.Request.RouteValues.As<int>("idUsuario");

                await _pictogramaMgmt.EliminarFavorito(idUsuario, idPictograma);
                await ctx.Response.Negotiate("favorito eliminado");
            });
        }
    }
}
