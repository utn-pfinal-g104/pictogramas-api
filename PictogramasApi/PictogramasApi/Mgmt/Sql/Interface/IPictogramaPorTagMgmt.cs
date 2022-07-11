
using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IPictogramaPorTagMgmt
    {
        Task AgregarRelaciones(List<PictogramaPorTag> picsXtags);
        Task<List<PictogramaPorTag>> ObtenerPictogramasPorTag(int tag);
    }
}
