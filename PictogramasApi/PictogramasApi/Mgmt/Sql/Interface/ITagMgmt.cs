
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface ITagMgmt
    {
        Task AgregarTags(HashSet<string> tags);
    }
}
