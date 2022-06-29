using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt
{
    public interface ICategoriaMgmt
    {
        Task<List<Categoria>> GetCategorias();
    }
}
