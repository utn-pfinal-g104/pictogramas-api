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
    public class PictogramaPorTagMgmt : IPictogramaPorTagMgmt
    {
        private readonly DapperContext _context;

        public PictogramaPorTagMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task AgregarRelaciones(List<PictogramaPorTag> picsXtags)
        {
            try
            {
                int cantidadDeInserts = picsXtags.Count() / 1000 + 1;

                List<PictogramaPorTag> picxXtagsRecortadas = new List<PictogramaPorTag>();
                for (int i = 0; i < cantidadDeInserts; i++)
                {
                    picxXtagsRecortadas = picsXtags.Skip(i * 1000).Take(1000).ToList();
                    string result = String.Join(",", picxXtagsRecortadas.Select(p =>
                        "(" + p.IdPictograma + ","
                        + p.IdTag + ")"
                    ));
                    string insert = $"insert into PictogramasPorTags (IdPictograma, IdTag) values {result}";
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
    }
}
