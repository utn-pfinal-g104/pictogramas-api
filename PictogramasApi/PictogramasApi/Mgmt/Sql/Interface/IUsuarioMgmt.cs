
using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IUsuarioMgmt
    {
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuario(int id);
        Task CrearUsuario(Usuario usuario);
        Task<Usuario> GetUsuario(string username);
        Task ActualizarUsuario(Usuario usuario);
    }
}
