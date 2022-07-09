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
                int cantidadDeInserts = picsXcats.Count() / 1000 + 1;

                List<PictogramaPorCategoria> picxXcatsRecortadas = new List<PictogramaPorCategoria>();
                for (int i = 0; i < cantidadDeInserts; i++)
                {
                    picxXcatsRecortadas = picsXcats.Skip(i * 1000).Take(1000).ToList();
                    string result = String.Join(",", picxXcatsRecortadas.Select(p =>
                        "(" + p.IdPictograma + ","
                        + p.IdCategoria + ")"
                    ));
                    string insert = $"insert into PictogramasPorCategorias (IdPictograma, IdCategoria) values {result}";
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
