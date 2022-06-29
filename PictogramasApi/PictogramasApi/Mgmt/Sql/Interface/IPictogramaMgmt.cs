using PictogramasApi.Model;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt
{
    public interface IPictogramaMgmt
    {
        Task<Pictograma> ObtenerPictogramaPorPalabra(string palabra);
    }
}
