using Carter;
using Carter.Request;
using Carter.Response;
using PictogramasApi.Mgmt.Sql.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Modules
{
    public class CategoriasPorUsuarioModule : CarterModule
    {

        private readonly ICategoriaPorUsuarioMgmt _categoriaPorUsuarioMgmt;

        public CategoriasPorUsuarioModule(ICategoriaPorUsuarioMgmt categoriaPorUsuarioMgmt) : base("/categoriasPorUsuario")
        {
            _categoriaPorUsuarioMgmt = categoriaPorUsuarioMgmt;

            GetCategoriasPorUsuario();
            DeleteCategoriasPorUsuario();
            InsertarCategoriasPorUsuario();
        }

        private void GetCategoriasPorUsuario()
        {
            Get("/{idUsuario:minlength(1)}", async (ctx) =>
            {
                try
                {
                    var idUsuario = ctx.Request.RouteValues.As<int>("idUsuario");

                    var categorias = await _categoriaPorUsuarioMgmt.ObtenerCategoriasPorUsuario(idUsuario);

                    if (categorias != null)
                    {
                        await ctx.Response.Negotiate(categorias);
                    }
                    else
                    {
                        await ctx.Response.Negotiate("Error obteniendo categorias favoritas");
                    }
                } catch(Exception ex)
                {
                    throw ex;
                }                
            });
        }

        private void DeleteCategoriasPorUsuario()
        {
            Delete("/{idUsuario:minlength(1)}/{idCategoria:minlength(1)}", async (ctx) =>
            {
                try
                {
                    var idUsuario = ctx.Request.RouteValues.As<int>("idUsuario");
                    var idCategoria = ctx.Request.RouteValues.As<int>("idCategoria");

                    await _categoriaPorUsuarioMgmt.EliminarCategoriaPorUsuario(idUsuario, idCategoria);                 
                    await ctx.Response.Negotiate($"Relacion {idUsuario}_{idCategoria} eliminada");

                } 
                catch(Exception ex)
                {
                    throw ex;
                }                
            });
        }

        private void InsertarCategoriasPorUsuario()
        {
            Post("/{idUsuario:minlength(1)}/{idCategoria:minlength(1)}", async (ctx) =>
            {
                try
                {
                    var idUsuario = ctx.Request.RouteValues.As<int>("idUsuario");
                    var idCategoria = ctx.Request.RouteValues.As<int>("idCategoria");

                    await _categoriaPorUsuarioMgmt.InsertarCategoriaPorUsuario(idUsuario, idCategoria);
                    await ctx.Response.Negotiate($"Relacion {idUsuario}_{idCategoria} se inserto correctamente");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}
