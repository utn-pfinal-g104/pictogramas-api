using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface ITagMgmt
    {
        Task AgregarTags(List<Tag> tags);
        Task<List<Tag>> ObtenerTags();
        Task<Tag> ObtenerTag(string nombre);
    }
}
