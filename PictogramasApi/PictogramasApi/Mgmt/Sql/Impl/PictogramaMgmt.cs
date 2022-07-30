using Dapper;
using DapperExtensions;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class PictogramaMgmt : IPictogramaMgmt
    {
        private readonly DapperContext _context;

        public PictogramaMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task<Pictograma> AgregarPictograma(Pictograma pictograma)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await Task.Run(() => connection.Insert(pictograma));
                    connection.Close();
                    return pictograma;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AgregarPictogramas(List<Pictograma> pictogramas)
        {
            try
            {
                int cantidadDeInserts = pictogramas.Count() / 1000 + 1;

                List<Pictograma> pictogramasRecortados = new List<Pictograma>();
                for (int i = 0; i < cantidadDeInserts; i++)
                {
                    pictogramasRecortados = pictogramas.Skip(i * 1000).Take(1000).ToList();
                    string result = String.Join(",", pictogramasRecortados.Select(p =>
                        "(" + (p.Schematic ? 1 : 0) + ","
                        + (p.Sex ? 1 : 0) + ","
                        + (p.Violence ? 1 : 0) + ","
                        + (p.Aac ? 1 : 0) + ","
                        + (p.AacColor ? 1 : 0) + ","
                        + (p.Skin ? 1 : 0) + ","
                        + (p.Hair ? 1 : 0) + ","
                        + p.IdArasaac + ","
                        + "null" + ")"
                    ));
                    string insert = $"insert into Pictogramas (Schematic,Sex,Violence,Aac,AacColor,Skin,Hair,IdArasaac,IdUsuario) values {result}";
                    using (IDbConnection connection = _context.CreateConnection())
                    {
                        connection.Open();
                        await Task.Run(() => connection.Execute(insert));
                        //await connection.InsertAsync(pictogramas);
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public async Task<List<Pictograma>> ObtenerInformacionPictogramas()
        {
            try
            {
                var pics = await ObtenerPictogramas();

                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var sql = $"select * from pictogramas p left join PictogramasPorCategorias pc on (pc.IdPictograma = p.Id) left join PictogramasPorTags pt on (pt.IdPictograma = p.Id) left join keywords k on (k.IdPictograma = p.Id) left join Categorias c on (c.Id = pc.IdCategoria) left join Tags t on (t.Id = pt.IdTag)";
                    var pictogramas = await connection.QueryAsync<Pictograma, PictogramaPorCategoria, PictogramaPorTag, PalabraClave, Categoria, Tag, Pictograma>(sql, (p, pc, pt, k, c, t) => {
                        var picto = pics.FirstOrDefault(pic => pic.Id == p.Id);
                        if (c != null)
                        {
                            if (picto.Categorias != null)
                            {
                                if (!picto.Categorias.Any(cat => cat.Id == c.Id))
                                    picto.Categorias.Add(c);
                            }
                            else
                                picto.Categorias = new List<Categoria> { c };
                        }

                        if (t != null)
                        {
                            if (picto.Tags != null)
                            {
                                if (!picto.Tags.Any(tag => tag.Id == t.Id))
                                    picto.Tags.Add(t);
                            }
                            else
                                picto.Tags = new List<Tag> { t };
                        }

                        if (k != null)
                        {
                            if (picto.Keywords != null)
                            {
                                if (!picto.Keywords.Any(key => key.Id == k.Id))
                                    picto.Keywords.Add(k);
                            }
                            else
                                picto.Keywords = new List<PalabraClave> { k };
                        }

                        return p;
                    },
                    splitOn: "Id,Id,Id,Id,Id", commandTimeout: 300000);

                    connection.Close();
                    return pics;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Pictograma>> ObtenerPictogramas()
        {
            try
            {                
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pictogramas = (connection.GetList<Pictograma>()).ToList();
                    connection.Close();
                    return pictogramas;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Pictograma>> ObtenerPictogramasPorIds(List<int> pictogramasIds)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var sql = $"SELECT p.*, k.* from Pictogramas p JOIN Keywords k on p.id = k.IdPictograma WHERE p.id in ({string.Join(",", pictogramasIds)})";
                    var pictogramas = await connection.QueryAsync<Pictograma, PalabraClave, Pictograma>(sql, (pictograma, keyword) => {
                        if (pictograma.Keywords != null)
                            pictograma.Keywords.Add(keyword);
                        else
                            pictograma.Keywords = new List<PalabraClave> { keyword };
                        return pictograma;
                    },
                    splitOn: "Id");

                    connection.Close();
                    return pictogramas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ObtenerTotalPictogramas()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    pgAnd.Predicates.Add(Predicates.Field<Pictograma>(c => c.IdUsuario, Operator.Eq, null));
                    var cantidadPictogramas = (connection.GetList<Pictograma>(pgAnd)).Count();
                    connection.Close();
                    return cantidadPictogramas;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ObtenerTotalPictogramas(int usuarioId)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
                    pgAnd.Predicates.Add(Predicates.Field<Pictograma>(c => c.IdUsuario, Operator.Eq, usuarioId));
                    pgAnd.Predicates.Add(Predicates.Field<Pictograma>(c => c.IdUsuario, Operator.Eq, null));
                    var cantidadPictogramas = (connection.GetList<Pictograma>(pgAnd)).Count();
                    connection.Close();
                    return cantidadPictogramas;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
