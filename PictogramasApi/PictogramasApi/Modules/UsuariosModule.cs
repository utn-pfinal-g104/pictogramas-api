using Carter;
using Carter.ModelBinding;
using Carter.Request;
using Carter.Response;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using PictogramasApi.Model.Requests;
using PictogramasApi.Utils;
using System;
using System.Text;

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
            GetUsuarioPorUsernameYPassword();
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

        private void GetUsuarioPorUsernameYPassword()
        {
            Get("/{username:minlength(1)}/{password:minlength(1)}", async (ctx) =>
            {
                var username = ctx.Request.RouteValues.As<string>("username");
                var password = ctx.Request.RouteValues.As<string>("password");
                try
                {
                    password = Seguridad.sha256_hash(password);
                    Usuario usuario = await _usuarioMgmt.GetUsuario(username, password);
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
                var usuarioRequest = await ctx.Request.Bind<UsuarioRequest>();
                // TODO: Encriptar / hashear password
                // Verificamos si ya existe

                //var p = Seguridad.DecryptStringAES(usuarioRequest.Password);
                //byte[] bytes = Encoding.ASCII.GetBytes(usuarioRequest.Password);
                //var password = Seguridad.DesencriptarPassword(bytes);

                var password = Seguridad.sha256_hash(usuarioRequest.Password);

                Usuario usuario = await _usuarioMgmt.GetUsuario(usuarioRequest.NombreUsuario, password);
                if (usuario == null)
                    usuario = await _usuarioMgmt.CrearUsuario(new Usuario { NombreUsuario = usuarioRequest.NombreUsuario, Password = password });

                ctx.Response.StatusCode = 201;
                await ctx.Response.AsJson(usuario);
            });
        }



        private void PatchUsuario()
        {
            Patch("/", async (ctx) =>
            {
                var usuario = await ctx.Request.Bind<Usuario>();
                // TODO: Encriptar / hashear password
                usuario.Password = Seguridad.sha256_hash(usuario.Password);
                await _usuarioMgmt.ActualizarUsuario(usuario);
                ctx.Response.StatusCode = 201;
                await ctx.Response.AsJson("Usuario creado");
            });
        }
    }
}
