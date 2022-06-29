using PictogramasApi.Model;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt
{
    public interface INeo4JMgmt
    {
        Task<PictogramaGrafo> ObtenerPictograma(int id);
    }
}
