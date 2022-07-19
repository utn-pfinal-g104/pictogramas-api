using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IPictogramaMgmt
    {
        Task AgregarPictogramas(List<Pictograma> pictogramas);
        Task<List<Pictograma>> ObtenerPictogramas();
        Task<List<Pictograma>> ObtenerPictogramasPorIds(List<int> pictogramasIds);
        Task<int> ObtenerTotalPictogramas();
        Task<List<Pictograma>> ObtenerInformacionPictogramas();
    }
}
