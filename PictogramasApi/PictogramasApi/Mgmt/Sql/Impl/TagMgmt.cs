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
    public class TagMgmt : ITagMgmt
    {
        private readonly DapperContext _context;

        public TagMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task AgregarTags(HashSet<string> tags)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await connection.InsertAsync((IEnumerable<Tag>)tags);
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
