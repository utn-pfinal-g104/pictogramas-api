using Carter;
using Carter.Response;
using PictogramasApi.Mgmt.Sql.Interface;
using System;

namespace PictogramasApi.Modules
{
    public class CategoriasModule : CarterModule
    {
        private readonly ICategoriaMgmt _categoriaMgmt;

        public CategoriasModule(ICategoriaMgmt categoriaMgmt) : base("/categorias")
        {
            _categoriaMgmt = categoriaMgmt;

            GetCategorias();
            GetTotalCategorias();
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
                var categorias = await _categoriaMgmt.GetCategorias();
                await ctx.Response.Negotiate(categorias);
            });
        }
    }
}
