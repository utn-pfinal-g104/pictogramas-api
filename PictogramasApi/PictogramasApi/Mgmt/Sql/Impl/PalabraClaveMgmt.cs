using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class PalabraClaveMgmt : IPalabraClaveMgmt
    {
        private readonly DapperContext _context;

        public PalabraClaveMgmt(DapperContext context)
        {
            _context = context;
        }
    }
}
