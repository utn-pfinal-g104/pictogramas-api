using DapperExtensions;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class PizarraMgmt : IPizarraMgmt
    {
        private readonly DapperContext _context;
        public PizarraMgmt(DapperContext context)
        {
            _context = context;
        }

        public Pizarra GuardarPizarra(Pizarra pizarra)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    connection.Insert<Pizarra>(pizarra);
                    connection.Close();
                    return pizarra;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Pizarra> ObtenerPizarras(int usuarioId)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    pgAnd.Predicates.Add(Predicates.Field<Pizarra>(p => p.UsuarioId, Operator.Eq, usuarioId));
                    List<Pizarra> pizarras = connection.GetList<Pizarra>(pgAnd).ToList();
                    connection.Close();
                    return pizarras;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Pizarra ObtenerPizarra(int pizarraId)
        {
            throw new NotImplementedException();
        }
    }
}
