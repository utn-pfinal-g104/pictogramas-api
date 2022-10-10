using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface ICategoriaPorUsuarioMgmt
    {
        Task<List<CategoriaPorUsuario>> ObtenerCategoriasDeUsuario(int idUsuario);
        Task EliminarCategoriasDeUsuario(int idUsuario);
        Task InsertarCategoriasDeUsuario(int idUsuario, List<int> idCategoria); 
    }
}
