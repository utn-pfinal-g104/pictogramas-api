using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface ICategoriaMgmt
    {
        Task<List<Categoria>> GetCategorias();
        Task AgregarCategorias(List<Categoria> categorias);
        Task<List<Categoria>> ObtenerCategorias();
        Task<Categoria> ObtenerCategoria(string nombre);
        Task<int> ObtenerTotalCategorias();
        Task EliminarCategorias();
    }
}
