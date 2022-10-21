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
    public class CategoriaMgmt : ICategoriaMgmt
    {
        private readonly DapperContext _context;

        public CategoriaMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task AgregarCategorias(List<Categoria> categorias)
        {
            try
            {
                string result = String.Join(",", categorias.Select(c => "('" + c.Nombre + "')"));
                string insert = $"insert into Categorias (nombre) values {result}";
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await Task.Run (() => connection.Execute(insert));
                    //await connection.InsertAsync(categorias); // error: This operation is only valid on generic types
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Categoria>> GetCategorias()
        {
            try
            { 
                return await Task.Run(() =>
                {
                    using (IDbConnection connection = _context.CreateConnection())
                    {
                        connection.Open();
                        var categorias = connection.GetList<Categoria>();
                        connection.Close();
                        return categorias.ToList();
                    }
                });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ObtenerTotalCategorias()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    int categorias = connection.GetList<Categoria>().Count();
                    connection.Close();
                    return categorias;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Categoria> ObtenerCategoria(string nombre)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    pgAnd.Predicates.Add(Predicates.Field<Categoria>(c => c.Nombre, Operator.Eq, nombre));
                    var categoria = (connection.GetList<Categoria>(pgAnd)).FirstOrDefault();
                    connection.Close();
                    return categoria;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Categoria>> ObtenerCategorias()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var categorias = (connection.GetList<Categoria>()).ToList();
                    connection.Close();
                    return categorias;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EliminarCategorias()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await Task.Run(() => connection.Execute("delete from categorias"));
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
