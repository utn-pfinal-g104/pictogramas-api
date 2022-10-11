using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface ICategoriaPorUsuarioMgmt
    {
        Task<List<CategoriaPorUsuario>> ObtenerCategoriasPorUsuario(int idUsuario);
        Task EliminarCategoriaPorUsuario(int idUsuario, int idCategoria);
        Task InsertarCategoriasPorUsuario(int idUsuario, List<int> idCategoria);
        Task InsertarCategoriaPorUsuario(int idUsuario, int idCategoria);

    }
}
