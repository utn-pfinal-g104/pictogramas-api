using Carter;
using Carter.Response;
using PictogramasApi.Mgmt.Sql.Interface;

namespace PictogramasApi.Modules
{
    public class CategoriasModule : CarterModule
    {
        private readonly ICategoriaMgmt _categoriaMgmt;

        public CategoriasModule(ICategoriaMgmt categoriaMgmt) : base("/categorias")
        {
            _categoriaMgmt = categoriaMgmt;

            GetCategorias();
        }

        private void GetCategorias()
        {
            Get("/", async (ctx) =>
            {
                var categorias = await _categoriaMgmt.GetCategorias();
                await ctx.Response.Negotiate(categorias);
            });
        }
    }
}
