﻿using Carter;
using Carter.Request;
using Carter.Response;
using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Utils;

namespace PictogramasApi.Modules
{
    public class CategoriasModule : CarterModule
    {
        private readonly ICategoriaMgmt _categoriaMgmt;
        private readonly ICategoriaPorUsuarioMgmt _categoriaPorUsuarioMgmt;
        private readonly IStorageMgmt _storageMgmt;

        public CategoriasModule(ICategoriaMgmt categoriaMgmt, ICategoriaPorUsuarioMgmt categoriaPorUsuarioMgmt, IStorageMgmt storageMgmt) : base("/categorias")
        {
            _categoriaMgmt = categoriaMgmt;
            _categoriaPorUsuarioMgmt = categoriaPorUsuarioMgmt;
            _storageMgmt = storageMgmt;

            GetCategorias();
            GetTotalCategorias();
            GetCategoriaaDelStorageAsBase64();
            GetCategoriasDeUsuario();
            DeleteCategoriasDeUsuario();
            InsertarCategoriasDeUsuario();
        }

        private void GetCategorias()
        {
            Get("/", async (ctx) =>
            {
                var categorias = await _categoriaMgmt.GetCategorias();
                await ctx.Response.Negotiate(categorias);
            });
        }

        private void GetTotalCategorias()
        {
            Get("/total", async (ctx) =>
            {
                var categorias = await _categoriaMgmt.ObtenerTotalCategorias();
                await ctx.Response.Negotiate(categorias);
            });
        }

        private void GetCategoriaaDelStorageAsBase64()
        {
            Get("/{filename:minlength(1)}/obtener/base64", async (ctx) =>
            {
                var filename = ctx.Request.RouteValues.As<string>("filename");

                var pictograma = _storageMgmt.ObtenerImagenCategoria(filename);

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
            });
        }

        private void GetCategoriasDeUsuario()
        {
            Get("/categoriasDeUsuario/{idUsuario:minlength(1)}", async (ctx) =>
            {
                var idUsuario = ctx.Request.RouteValues.As<int>("idUsuario");

                var categorias = _categoriaPorUsuarioMgmt.ObtenerCategoriasDeUsuario(idUsuario);

                if (categorias != null)
                {
                    await ctx.Response.Negotiate(categorias);
                }
                else
                {
                    await ctx.Response.Negotiate("Error obteniendo categorias favoritas");
                }
            });
        }

        private void DeleteCategoriasDeUsuario()
        {
            Delete("", async (ctx) =>
            {
                //TODO
            });
        }

        private void InsertarCategoriasDeUsuario()
        {
            Post("", async (ctx) =>
            {
                //TODO
            });
        }
    }
}
