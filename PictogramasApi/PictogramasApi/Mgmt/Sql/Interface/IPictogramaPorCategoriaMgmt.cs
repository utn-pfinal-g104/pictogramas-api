
using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IPictogramaPorCategoriaMgmt
    {
        Task AgregarRelaciones(Pictograma pictograma, List<Categoria> categorias);
        Task AgregarRelaciones(List<PictogramaPorCategoria> picsXcats);
        Task<List<PictogramaPorCategoria>> ObtenerPictogramasPorCategoria(int categoria);
        List<PictogramaPorCategoria> ObtenerCategoriasPorPictograma(int pictograma);
        Task EliminarRelaciones();
    }
}
