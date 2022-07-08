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
                        var categorias = await connection.GetListAsync<Usuario>();
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

    }
}
