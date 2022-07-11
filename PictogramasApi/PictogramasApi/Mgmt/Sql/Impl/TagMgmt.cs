using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
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

        public async Task<Tag> ObtenerTag(string nombre)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    pgAnd.Predicates.Add(Predicates.Field<Tag>(t => t.Nombre, Operator.Eq, nombre));
                    var tag = (await connection.GetListAsync<Tag>(pgAnd)).FirstOrDefault();
                    connection.Close();
                    return tag;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
