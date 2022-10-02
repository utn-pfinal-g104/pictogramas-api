using Dapper;
using DapperExtensions;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
                    foreach(var p in pizarras)
                    {
                        p.Celdas = ObtenerCeldasDePizarra(p.Id, p.UsuarioId);
                    }
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

        public List<CeldaPizarra> ObtenerCeldasDePizarra(int pizarraId, int usuarioId)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    pgAnd.Predicates.Add(Predicates.Field<CeldaPizarra>(c => c.PizarraId, Operator.Eq, pizarraId));
                    pgAnd.Predicates.Add(Predicates.Field<CeldaPizarra>(c => c.UsuarioId, Operator.Eq, usuarioId));
                    List<CeldaPizarra> celdas = connection.GetList<CeldaPizarra>(pgAnd).ToList();
                    connection.Close();
                    return celdas;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Pizarra GuardarCeldasDePizarra(Pizarra pizarra)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    foreach(var celda in pizarra.Celdas)
                    {
                        celda.PizarraId = pizarra.Id;
                        celda.UsuarioId = pizarra.UsuarioId;
                        connection.Insert<CeldaPizarra>(celda);
                    }
                    connection.Close();
                    return pizarra;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarPizarra(Pizarra pizarra)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    connection.Update<Pizarra>(pizarra);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarPizarra(int pizarraId)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    StringBuilder query = new StringBuilder();
                    var pizarrasQuery = $"delete from CeldaPizarra where pizarraid = {pizarraId} ";
                    var celdasQuery = $"delete from pizarras where id = {pizarraId} ";
                    query.Append(pizarrasQuery);
                    query.Append(celdasQuery);
                    connection.Execute(query.ToString());
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCeldasDePizarra(Pizarra pizarra)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    foreach (var celda in pizarra.Celdas)
                    {
                        connection.Update<CeldaPizarra>(celda);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
