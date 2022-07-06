using Carter;
using Carter.Response;
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
            Get("/usuarios", async (ctx) =>
            {
                var usuarios = await _usuarioMgmt.GetUsuarios();
                await ctx.Response.Negotiate(usuarios);
            });
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
