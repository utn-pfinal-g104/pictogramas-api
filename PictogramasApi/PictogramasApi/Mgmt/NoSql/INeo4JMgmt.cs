using PictogramasApi.Model;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.NoSql
{
    public interface INeo4JMgmt
    {
        Task<PictogramaGrafo> ObtenerPictograma(int id);
    }
}
