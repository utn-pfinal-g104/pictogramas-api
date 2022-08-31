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
                    await ctx.Response.Negotiate("");
                }
            });
        }
    }
}
