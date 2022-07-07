﻿using DapperExtensions;
using DapperExtensions.Predicate;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await connection.InsertAsync(pictogramas);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public async Task<Pictograma> ObtenerPictogramaPorPalabra(string palabra)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (IDbConnection connection = _context.CreateConnection())
                    {
                        connection.Open();
                        var pgAnd = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                        //TODO: La palabra asociada al pictograma se encuentra en la tabla keywords, por lo cual requiere joinear
                        pgAnd.Predicates.Add(Predicates.Field<Pictograma>(p => p.Hair.ToString(), Operator.Eq, palabra)); 
                        var pictograma = await connection.GetAsync<Pictograma>(pgAnd);
                        connection.Close();
                        return pictograma;
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
