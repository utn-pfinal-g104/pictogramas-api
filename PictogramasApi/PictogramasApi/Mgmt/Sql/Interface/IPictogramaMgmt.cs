using PictogramasApi.Model;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IPictogramaMgmt
    {
        Task<Pictograma> ObtenerPictogramaPorPalabra(string palabra);
    }
}
