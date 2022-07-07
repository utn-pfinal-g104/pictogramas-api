using DapperExtensions;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class PictogramaPorCategoriaMgmt : IPictogramaPorCategoriaMgmt
    {
        private readonly DapperContext _context;

        public PictogramaPorCategoriaMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task AgregarRelaciones(List<PictogramaPorCategoria> picsXcats)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await connection.InsertAsync(picsXcats);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
    }
}
