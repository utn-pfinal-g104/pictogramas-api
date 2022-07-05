using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class PictogramaPorTagMgmt : IPictogramaPorTagMgmt
    {
        private readonly DapperContext _context;

        public PictogramaPorTagMgmt(DapperContext context)
        {
            _context = context;
        }
    }
}
