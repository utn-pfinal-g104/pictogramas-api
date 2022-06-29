using DapperExtensions;
using PictogramasApi.Configuration;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Impl
{
    public class CategoriaMgmt : ICategoriaMgmt
    {
        private readonly DapperContext _context;

        public CategoriaMgmt(DapperContext context)
        {
            _context = context;
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
    }
}
