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
                return await Task.Run(async () =>
                {
                    using (IDbConnection connection = _context.CreateConnection())
                    {
                        connection.Open();
                        var categorias = await connection.GetListAsync<Categoria>();
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

        public async Task<List<Categoria>> ObtenerCategorias()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var categorias = (await connection.GetListAsync<Categoria>()).ToList();
                    connection.Close();
                    return categorias;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
