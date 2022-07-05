using Carter;
using PictogramasApi.Mgmt.Sql.Interface;
using System;

namespace PictogramasApi.Modules
{
    public class UsuarioModule : CarterModule
    {
        private readonly IUsuarioMgmt _usuarioMgmt;

        public UsuarioModule(IUsuarioMgmt usuarioMgmt)
        {
            _usuarioMgmt = usuarioMgmt;

            GetUsuarios();
            GetUsuarioPorNombre();
            PostUsuario();
            PatchUsuario();
        }

        private void GetUsuarios()
        {
            throw new NotImplementedException();
        }

        private void GetUsuarioPorNombre()
        {
            throw new NotImplementedException();
        }

        private void PostUsuario()
        {
            throw new NotImplementedException();
        }

        private void PatchUsuario()
        {
            throw new NotImplementedException();
        }
    }
}
