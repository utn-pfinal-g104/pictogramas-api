﻿using DapperExtensions;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class PictogramaPorTagMgmt : IPictogramaPorTagMgmt
    {
        private readonly DapperContext _context;

        public PictogramaPorTagMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task AgregarRelaciones(List<PictogramaPorTag> picsXtags)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await connection.InsertAsync(picsXtags);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
    }
}
