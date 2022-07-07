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
    public class PalabraClaveMgmt : IPalabraClaveMgmt
    {
        private readonly DapperContext _context;

        public PalabraClaveMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task AgregarPalabrasClaves(List<PalabraClave> palabrasClaves)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await connection.InsertAsync((IEnumerable<PalabraClave>)palabrasClaves);
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
