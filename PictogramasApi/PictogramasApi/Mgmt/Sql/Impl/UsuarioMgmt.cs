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
    public class UsuarioaMgmt : IUsuarioMgmt
    {
        private readonly DapperContext _context;
        public UsuarioaMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            try
            {
                return await Task.Run(async () =>
                {
                    using (IDbConnection connection = _context.CreateConnection())
                    {
                        connection.Open();
                        var categorias = connection.GetList<Usuario>();
                        connection.Close();
                        return categorias.ToList();
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> GetUsuario(int id)
        {
            return await Task.Run(async () =>
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();                        
                    var usuario = connection.Query<Usuario>("SELECT * FROM Usuarios WHERE Id=@Id", new { Id = id }).FirstOrDefault();
                    connection.Close();
                    if (usuario == null)
                    {
                        throw new Exception($"No se encontro usuario con id {id}");
                    }
                    return usuario;
                }
            });
            
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            try
            { 
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    connection.Insert<Usuario>(usuario);
                    connection.Close();
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> GetUsuario(string username, string password)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    
                    pgAnd.Predicates.Add(Predicates.Field<Usuario>(u => u.NombreUsuario, Operator.Eq, username));
                    pgAnd.Predicates.Add(Predicates.Field<Usuario>(u => u.Password, Operator.Eq, password));
                    Usuario usuario = ( connection.GetList<Usuario>(pgAnd)).FirstOrDefault();
                    connection.Close();
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ActualizarUsuario(Usuario usuario)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    connection.Update(usuario);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario GetUsuarioPorIdentificador(string identificador)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    pgAnd.Predicates.Add(Predicates.Field<Usuario>(u => u.Identificador, Operator.Eq, identificador));
                    Usuario usuario = (connection.GetList<Usuario>(pgAnd)).FirstOrDefault();
                    connection.Close();
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
