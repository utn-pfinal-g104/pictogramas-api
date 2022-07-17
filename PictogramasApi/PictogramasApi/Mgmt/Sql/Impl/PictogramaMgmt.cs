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
    public class PictogramaMgmt : IPictogramaMgmt
    {
        private readonly DapperContext _context;

        public PictogramaMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task AgregarPictogramas(List<Pictograma> pictogramas)
        {
            try
            {
                int cantidadDeInserts = pictogramas.Count() / 1000 + 1;

                List<Pictograma> pictogramasRecortados = new List<Pictograma>();
                for (int i = 0; i < cantidadDeInserts; i++)
                {
                    pictogramasRecortados = pictogramas.Skip(i * 1000).Take(1000).ToList();
                    string result = String.Join(",", pictogramasRecortados.Select(p =>
                        "(" + (p.Schematic ? 1 : 0) + ","
                        + (p.Sex ? 1 : 0) + ","
                        + (p.Violence ? 1 : 0) + ","
                        + (p.Aac ? 1 : 0) + ","
                        + (p.AacColor ? 1 : 0) + ","
                        + (p.Skin ? 1 : 0) + ","
                        + (p.Hair ? 1 : 0) + ","
                        + p.IdArasaac + ","
                        + "null" + ")"
                    ));
                    string insert = $"insert into Pictogramas (Schematic,Sex,Violence,Aac,AacColor,Skin,Hair,IdArasaac,IdUsuario) values {result}";
                    using (IDbConnection connection = _context.CreateConnection())
                    {
                        connection.Open();
                        await Task.Run(() => connection.Execute(insert));
                        //await connection.InsertAsync(pictogramas);
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public async Task<List<Pictograma>> ObtenerPictogramas()
        {
            try
            {                
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pictogramas = (await connection.GetListAsync<Pictograma>()).ToList();
                    connection.Close();
                    return pictogramas;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Pictograma>> ObtenerPictogramas(List<int> pictogramasIds)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var sql = $"SELECT p.*, k.* from Pictogramas p JOIN Keywords k on p.id = k.IdPictograma WHERE p.id in ({string.Join(",", pictogramasIds)})";
                    var pictogramas = await connection.QueryAsync<Pictograma, PalabraClave, Pictograma>(sql, (pictograma, keyword) => {
                        if (pictograma.Keywords != null)
                            pictograma.Keywords.Add(keyword);
                        else
                            pictograma.Keywords = new List<PalabraClave> { keyword };
                        return pictograma;
                    },
                    splitOn: "Id");

                    connection.Close();
                    return pictogramas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ObtenerTotalPictogramas()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pictogramas = await connection.CountAsync<Pictograma>();
                    connection.Close();
                    return pictogramas;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
