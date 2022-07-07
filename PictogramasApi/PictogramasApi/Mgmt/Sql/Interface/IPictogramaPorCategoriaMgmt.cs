﻿
using PictogramasApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Interface
{
    public interface IPictogramaPorCategoriaMgmt
    {
        Task AgregarRelaciones(List<PictogramaPorCategoria> picsXcats);
    }
}
