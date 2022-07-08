using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IPictogramaMgmt
    {
        Task<Pictograma> ObtenerPictogramaPorPalabra(string palabra);
        Task AgregarPictogramas(List<Pictograma> pictogramas);
        Task<List<Pictograma>> ObtenerPictogramas();
    }
}
