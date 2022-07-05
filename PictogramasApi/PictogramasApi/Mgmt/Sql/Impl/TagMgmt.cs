using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class TagMgmt : ITagMgmt
    {
        private readonly DapperContext _context;

        public TagMgmt(DapperContext context)
        {
            _context = context;
        }
    }
}
