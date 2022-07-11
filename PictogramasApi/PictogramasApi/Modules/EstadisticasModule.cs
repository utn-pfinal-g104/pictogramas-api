using Carter;
using Carter.Response;
using PictogramasApi.Mgmt.NoSql;

namespace PictogramasApi.Modules
{
    public class EstadisticasModule : CarterModule
    {
        private readonly INeo4JMgmt _neo4JMgmt;

        public EstadisticasModule(INeo4JMgmt neo4JMgmt) : base("/estadisticas")
        {
            _neo4JMgmt = neo4JMgmt;

            GetRelaciones();
        }

        private void GetRelaciones()
        {
            Get("/", async (ctx) =>
            {
                var pictograma = await _neo4JMgmt.ObtenerPictograma(1);
                await ctx.Response.Negotiate("Funciona");
            });
        }
    }
}
