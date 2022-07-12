using Carter;
using Carter.ModelBinding;
using Carter.Request;
using Carter.Response;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;

namespace PictogramasApi.Modules
{
    public class UsuariosModule : CarterModule
    {
        private readonly IUsuarioMgmt _usuarioMgmt;

        public UsuariosModule(IUsuarioMgmt usuarioMgmt) : base("/usuarios")
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
            Get("/", async (ctx) =>
            {
                var usuarios = await _usuarioMgmt.GetUsuarios();
                await ctx.Response.Negotiate(usuarios);
            });
        }

        private void GetUsuarioPorId()
        {
            Get("/{id:int}", async (ctx) => 
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
            Get("/{username:minlength(1)}", async (ctx) =>
            {
                var username = ctx.Request.RouteValues.As<string>("username");
                try
                {
                    Usuario usuario = await _usuarioMgmt.GetUsuario(username);
                    await ctx.Response.Negotiate(usuario);
                }
                catch (Exception ex)
                {
                    ctx.Response.StatusCode = 404;
                    await ctx.Response.AsJson(ex.Message);
                }
            });
        }

        private void PostUsuario()
        {
            Post("/", async (ctx) =>
            {
                var usuario = await ctx.Request.Bind<Usuario>();
                // TODO: Encriptar / hashear password
                await _usuarioMgmt.CrearUsuario(usuario);
                ctx.Response.StatusCode = 201;
                await ctx.Response.AsJson("Usuario creado");
            });
        }

        private void PatchUsuario()
        {
            Patch("/", async (ctx) =>
            {
                var usuario = await ctx.Request.Bind<Usuario>();
                // TODO: Encriptar / hashear password
                await _usuarioMgmt.ActualizarUsuario(usuario);
                ctx.Response.StatusCode = 201;
                await ctx.Response.AsJson("Usuario creado");
            });
        }
    }
}
