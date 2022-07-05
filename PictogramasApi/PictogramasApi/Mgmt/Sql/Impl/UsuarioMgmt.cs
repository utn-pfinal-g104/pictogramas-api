using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class UsuarioMgmt : IUsuarioMgmt
    {
        private readonly DapperContext _context;

        public UsuarioMgmt(DapperContext context)
        {
            _context = context;
        }
    }
}
