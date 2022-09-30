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
    public class PalabraClaveMgmt : IPalabraClaveMgmt
    {
        private readonly DapperContext _context;

        public PalabraClaveMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task AgregarPalabraClave(Pictograma pictograma, string keyword)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    connection.Insert(new PalabraClave { Keyword = keyword, IdPictograma = pictograma.Id, Meaning = "", Tipo= 0, Plural="", HasLocution=false });
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AgregarPalabrasClaves(List<PalabraClave> palabrasClaves)
        {
            try
            {
                int cantidadDeInserts = palabrasClaves.Count() / 1000 + 1;

                List<PalabraClave> keywordsRecortadas = new List<PalabraClave>();
                for(int i=0; i< cantidadDeInserts; i++)
                {
                    keywordsRecortadas = palabrasClaves.Skip(i * 1000).Take(1000).ToList();
                    string result = String.Join(",", keywordsRecortadas.Select(p =>
                        "('" + p.Keyword + "',"
                        + p.Tipo + ",'"
                        + p.Meaning + "','"
                        + p.Plural + "',"
                        + (p.HasLocution ? 1 : 0) + ","
                        + p.IdPictograma + ")"
                    ));
                    string insert = $"insert into Keywords (Keyword,Tipo,Meaning,Plural,HasLocution,IdPictograma) values {result}";
                    using (IDbConnection connection = _context.CreateConnection())
                    {
                        connection.Open();
                        await Task.Run(() => connection.Execute(insert));
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public void EliminarPalabraClave(int pictogramaId)
        {
            try
            {
                //using (IDbConnection connection = _context.CreateConnection())
                //{
                //    connection.Open();
                //    var pgAnd = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
                //    pgAnd.Predicates.Add(Predicates.Field<PalabraClave>(c => c.IdPictograma, Operator.Eq, pictogramaId));
                //    var cantidadPictogramas = (connection.Delete<Pictograma>(pgAnd));
                //    connection.Close();
                //}

                string delete = $"delete from keywords where idpictograma = {pictogramaId} ";
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    connection.Execute(delete);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EliminarPalabrasClaves()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await Task.Run(() => connection.Execute("delete from keywords"));
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PalabraClave> ObtenerKeyword(string palabra)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    pgAnd.Predicates.Add(Predicates.Field<PalabraClave>(p => p.Keyword, Operator.Eq, palabra));
                    var keyword = (connection.GetList<PalabraClave>(pgAnd)).FirstOrDefault();
                    connection.Close();
                    return keyword;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PalabraClave>> ObtenerKeywords()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var tags = (connection.GetList<PalabraClave>()).ToList();
                    connection.Close();
                    return tags;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
