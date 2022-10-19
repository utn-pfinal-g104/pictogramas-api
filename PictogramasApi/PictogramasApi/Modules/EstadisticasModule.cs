using Carter;
using Carter.ModelBinding;
using Carter.Request;
using Carter.Response;
using PictogramasApi.Mgmt.NoSql;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using PictogramasApi.Model.Requests;
using System;

namespace PictogramasApi.Modules
{
    public class EstadisticasModule : CarterModule
    {
        private readonly INeo4JMgmt _neo4JMgmt;
        private readonly IEstadisticaMgmt _estadisticaMgmt;

        public EstadisticasModule(INeo4JMgmt neo4JMgmt, IEstadisticaMgmt estadisticaMgmt) : base("/estadisticas")
        {
            _neo4JMgmt = neo4JMgmt;
            _estadisticaMgmt = estadisticaMgmt;

            // Esto quedo deprecadisimo
            GetRelaciones();

            GetEstadisticasPorUsuario();

            PostEstadistica();
        }

        private void GetEstadisticasPorUsuario()
        {
            Get("/{usuario:int}", async (ctx) =>
            {
                var usuario = ctx.Request.RouteValues.As<int>("usuario");
                var estadisticas = _estadisticaMgmt.ObtenerEstadisticasDeUsuario(usuario);

                ctx.Response.StatusCode = 200;
                await ctx.Response.AsJson(estadisticas);
            });
        }

        private void PostEstadistica()
        {
            Post("/", async (ctx) =>
            {
                var estadisticaRequest = await ctx.Request.Bind<EstadisticaRequest>();

                var estadistica = new Estadistica
                {
                    Fecha = estadisticaRequest.Fecha,
                    Identificacion = estadisticaRequest.Id,
                    Pictograma = estadisticaRequest.Pictograma.Id,
                    PictogramaPrevio = estadisticaRequest.Previo.Id,
                    Usuario = estadisticaRequest.Usuario
                };

                _estadisticaMgmt.InsertarEstadistica(estadistica);

                ctx.Response.StatusCode = 201;
                await ctx.Response.AsJson("Creado");
            });
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
