﻿using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IUsuarioMgmt
    {
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuario(int id);
        Task<Usuario> CrearUsuario(Usuario usuario);
        Task<Usuario> GetUsuario(string username, string passwrod);
        Task ActualizarUsuario(Usuario usuario);
    }
}
