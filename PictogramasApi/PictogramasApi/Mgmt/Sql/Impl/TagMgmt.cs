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
    public class TagMgmt : ITagMgmt
    {
        private readonly DapperContext _context;

        public TagMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task AgregarTags(List<Tag> tags)
        {
            try
            {
                string result = String.Join(",", tags.Select(t => "('" + t.Nombre + "')"));
                string insert = $"insert into Tags (nombre) values {result}";
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await Task.Run(() => connection.Execute(insert));
                    //await connection.InsertAsync(tags);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public async Task<List<Tag>> ObtenerTags()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var tags = (await connection.GetListAsync<Tag>()).ToList();
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
