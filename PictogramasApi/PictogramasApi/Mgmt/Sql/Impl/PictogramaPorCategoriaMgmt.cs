﻿using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class PictogramaPorCategoriaMgmt : IPictogramaPorCategoriaMgmt
    {
        private readonly DapperContext _context;

        public PictogramaPorCategoriaMgmt(DapperContext context)
        {
            _context = context;
        }
    }
}
