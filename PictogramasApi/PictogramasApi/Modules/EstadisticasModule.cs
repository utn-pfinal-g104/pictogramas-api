using Carter;
using Carter.ModelBinding;
using Carter.Request;
using Carter.Response;
using PictogramasApi.Mgmt.NoSql;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using PictogramasApi.Model.Requests;
using PictogramasApi.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PictogramasApi.Modules
{
    public class EstadisticasModule : CarterModule
    {
        private readonly INeo4JMgmt _neo4JMgmt;
        private readonly IEstadisticaMgmt _estadisticaMgmt;
        private readonly IPictogramaPorCategoriaMgmt _picsPorCatMgmt;

        public EstadisticasModule(INeo4JMgmt neo4JMgmt, IEstadisticaMgmt estadisticaMgmt,
            IPictogramaPorCategoriaMgmt picsPorCatMgmt) : base("/estadisticas")
        {
            _neo4JMgmt = neo4JMgmt;
            _estadisticaMgmt = estadisticaMgmt;
            _picsPorCatMgmt = picsPorCatMgmt;

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

                var cantidadDePictogramasDistintosUtilizados = 0;
                var cantidadDeCategoriasDistintasUtilizadas = 0;
                var categoriasMasUtilizdas = new List<int>();
                var pictogramasMasUtilizados = new List<int>();

                var diccionarioCategorias = new Dictionary<int, int>();
                var diccionarioPictogramas = new Dictionary<int, int>();

                foreach (var registro in estadisticas)
                {
                    var categorias = _picsPorCatMgmt.ObtenerCategoriasPorPictograma(registro.Pictograma);

                    //Completo el diccionario de pictogramas, contabilizando cuantas veces se utilizo
                    if (diccionarioPictogramas.ContainsKey(registro.Pictograma))
                    {
                        var usoActual = diccionarioPictogramas[registro.Pictograma];
                        diccionarioPictogramas[registro.Pictograma] = usoActual +1;
                    }
                    else
                    {
                        diccionarioPictogramas.Add(registro.Pictograma, 1);
                    }

                    foreach (var categoria in categorias)
                    {
                        // Completo el diccionario con la categoria aumentando las veces que se utilizo, con esto puedo sacar cantidad total y mas utilizadas
                        if (diccionarioCategorias.ContainsKey(categoria.IdCategoria))
                        {
                            var usoActual = diccionarioCategorias[categoria.IdCategoria];
                            diccionarioCategorias[categoria.IdCategoria] = usoActual + 1;
                        }
                        else
                        {
                            diccionarioCategorias.Add(categoria.IdCategoria, 1);
                        }
                    }
                }

                cantidadDePictogramasDistintosUtilizados = diccionarioPictogramas.Count();
                cantidadDeCategoriasDistintasUtilizadas = diccionarioCategorias.Count();

                // Ordeno los diccionarios y saco los 5 elementos con mayor value (mas utilizados)
                categoriasMasUtilizdas = (from entry in diccionarioCategorias orderby entry.Value descending select entry.Key)
                    .Take(5).ToList();

                pictogramasMasUtilizados = (from entry in diccionarioPictogramas orderby entry.Value descending select entry.Key)
                    .Take(5).ToList();

                var reporteEstadisticas = new EstadisticaResponse
                {
                    TodasLasEstadisticas = estadisticas,
                    CantidadDeCategoriasDistintasUtilizadas = cantidadDeCategoriasDistintasUtilizadas,
                    CantidadDePictogramasDistintosUtilizados = cantidadDePictogramasDistintosUtilizados,
                    CategoriasMasUtilizdas = categoriasMasUtilizdas,
                    PictogramasMasUtilizados = pictogramasMasUtilizados
                };

                ctx.Response.StatusCode = 200;
                await ctx.Response.AsJson(reporteEstadisticas);
            });
        }

        private void PostEstadistica()
        {
            Post("/", async (ctx) =>
            {
                var estadisticaRequest = await ctx.Request.Bind<EstadisticaRequest>();

                var estadisticas = new List<Estadistica>();

                if (estadisticaRequest.Previo == null)
                {
                    // Es una seleccion de un unico pictograma, solo tengo una estadistica para guardar
                    var estadistica = new Estadistica
                    {
                        Fecha = estadisticaRequest.Fecha,
                        Identificacion = estadisticaRequest.Id,
                        Pictograma = estadisticaRequest.Pictograma.Id,
                        PictogramaPrevio = null,
                        Usuario = estadisticaRequest.Usuario
                    };

                    _estadisticaMgmt.InsertarEstadistica(estadistica);
                }
                else
                {
                    var pictogramasAnteriores = estadisticaRequest.TodosLosAnteriores;
                    //Hay una lista de pictogramas para guardar
                    //TODO: Finalizar guardado de anteriores
                    for (int i = 0; i < pictogramasAnteriores.Count; i++)
                    {
                        if (i == 0)
                        {
                            // El primero de todos es el primer pictograma
                            var est = new Estadistica
                            {
                                Fecha = estadisticaRequest.Fecha,
                                Identificacion = estadisticaRequest.Id,
                                Pictograma = pictogramasAnteriores[i].Id,
                                PictogramaPrevio = null,
                                Usuario = estadisticaRequest.Usuario
                            };
                            _estadisticaMgmt.InsertarEstadistica(est);
                        }
                        else
                        {
                            // Este pictograma ya cuenta con uno anterior
                            var est = new Estadistica
                            {
                                Fecha = estadisticaRequest.Fecha,
                                Identificacion = estadisticaRequest.Id,
                                Pictograma = pictogramasAnteriores[i].Id,
                                PictogramaPrevio = pictogramasAnteriores[i-1].Id,
                                Usuario = estadisticaRequest.Usuario
                            };
                            _estadisticaMgmt.InsertarEstadistica(est);
                        }
                    }

                    //Luego de insertar los anteriores, inserto el ultimo ya que este no se encuentra en todos los anteriores
                    var estadistica = new Estadistica
                    {
                        Fecha = estadisticaRequest.Fecha,
                        Identificacion = estadisticaRequest.Id,
                        Pictograma = estadisticaRequest.Pictograma.Id,
                        PictogramaPrevio = estadisticaRequest.Previo.Id,
                        Usuario = estadisticaRequest.Usuario
                    };
                    _estadisticaMgmt.InsertarEstadistica(estadistica);
                }

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
