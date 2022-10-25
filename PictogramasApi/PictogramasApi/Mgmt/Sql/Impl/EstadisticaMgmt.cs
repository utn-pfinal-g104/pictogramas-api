using Dapper;
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
    public class EstadisticaMgmt : IEstadisticaMgmt
    {
        private readonly DapperContext _context;

        public EstadisticaMgmt(DapperContext context)
        {
            _context = context;
        }

        public void InsertarEstadistica(Estadistica estadistica)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    connection.Insert<Estadistica>(estadistica);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Estadistica> ObtenerEstadisticasDeUsuario(int usuario)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
                    pgAnd.Predicates.Add(Predicates.Field<Estadistica>(e => e.Usuario, Operator.Eq, usuario));
                    var estadisticas = connection.GetList<Estadistica>(pgAnd);
                    connection.Close();
                    return estadisticas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Estadistica> ObtenerRecientes(int cantidad, int usuario)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    string query = $"select top {cantidad} * from estadisticas where Usuario = {usuario}";
                    var estadisticas = connection.Query<Estadistica>(query).ToList();
                    connection.Close();
                    return estadisticas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
