using Carter;
using Carter.ModelBinding;
using Carter.Request;
using Carter.Response;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;
using System.Net;

namespace PictogramasApi.Modules
{
    public class PizarrasModule : CarterModule
    {
        private readonly IPizarraMgmt _pizarraMgmt;

        public PizarrasModule(IPizarraMgmt pizarraMgmt) : base("/pizarras")
        {
            _pizarraMgmt = pizarraMgmt;

            GetPizarras();
            PostPizarra();
            PutPizarra();
            DeletePizarra();
        }

        private void GetPizarras()
        {
            Get("/{usuarioId:int}", async (ctx) =>
            {
                var usuarioId = ctx.Request.RouteValues.As<int>("usuarioId");
                var pizarras = _pizarraMgmt.ObtenerPizarras(usuarioId);                   
                await ctx.Response.Negotiate(pizarras);
            });
        }

        private void PostPizarra()
        {
            Post("/", async (ctx) =>
            {
                try
                {
                    var request = await ctx.Request.Bind<Pizarra>();
                    request = _pizarraMgmt.GuardarPizarra(request);
                    _pizarraMgmt.GuardarCeldasDePizarra(request);
                    await ctx.Response.Negotiate(request);
                }
                catch (Exception ex)
                {
                    ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await ctx.Response.Negotiate("Error en creacion");
                }
            });
        }

        private void PutPizarra()
        {
            Put("/", async (ctx) =>
            {
                try
                {
                    var request = await ctx.Request.Bind<Pizarra>();
                    _pizarraMgmt.ActualizarPizarra(request);
                    _pizarraMgmt.GuardarCeldasDePizarra(request);
                    await ctx.Response.Negotiate("Pizarra Actualizada");
                }
                catch (Exception ex)
                {
                    ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await ctx.Response.Negotiate("Error en actualizacion");
                }
            });
        }

        private void DeletePizarra()
        {
            Delete("/{pizarraId:int}", async (ctx) =>
            {
                try
                {
                    var id = ctx.Request.RouteValues.As<int>("pizarraId");
                    _pizarraMgmt.BorrarPizarra(id);
                    await ctx.Response.Negotiate("Pizarra Eliminada");
                }
                catch (Exception ex)
                {
                    ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await ctx.Response.Negotiate("Error en eliminacion");
                }
            });
        }
    }
}
