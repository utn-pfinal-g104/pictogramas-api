using Carter;
using Carter.Request;
using Carter.Response;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
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
            GetUsuarioPorId();
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

        private void GetUsuarioPorId()
        {

            Get("/usuarios/{id:int}", async (ctx) => 
            {
                var id = ctx.Request.RouteValues.As<int>("id");
                try
                {                    
                    Usuario usuario = await _usuarioMgmt.GetUsuario(id);
                    await ctx.Response.Negotiate(usuario);
                }
                catch (Exception ex)
                {
                    ctx.Response.StatusCode = 404;
                    await ctx.Response.AsJson(ex.Message);
                }
                
            });
        }

        private void GetUsuarioPorNombre()
        {

        }

        private void PostUsuario()
        {
            //Post("/usuarios", async (ctx) =>
            //{
               
            //});
        }

        private void PatchUsuario()
        {

        }
    }
}
