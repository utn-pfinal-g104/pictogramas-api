using Dapper;
using DapperExtensions;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
                int cantidadDeInserts = palabrasClaves.Count() / 1000 + 1;

                List<PalabraClave> keywordsRecortadas = new List<PalabraClave>();
                for(int i=0; i< cantidadDeInserts; i++)
                {
                    keywordsRecortadas = palabrasClaves.Skip(i * 1000).Take(1000).ToList();
                    string result = String.Join(",", keywordsRecortadas.Select(p =>
                        "('" + p.Keyword + "',"
                        + p.Tipo + ",'"
                        + p.Meaning + "','"
                        + p.Plural + "',"
                        + (p.HasLocution ? 1 : 0) + ","
                        + p.IdPictograma + ")"
                    ));
                    string insert = $"insert into Keywords (Keyword,Tipo,Meaning,Plural,HasLocution,IdPictograma) values {result}";
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
