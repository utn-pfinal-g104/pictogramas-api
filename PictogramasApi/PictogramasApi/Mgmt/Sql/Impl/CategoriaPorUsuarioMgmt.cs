using Dapper;
using DapperExtensions;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictogramasApi.Mgmt.Sql.Impl
{
    public class CategoriaPorUsuarioMgmt : ICategoriaPorUsuarioMgmt
    {
        private readonly DapperContext _context;

        public CategoriaPorUsuarioMgmt(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaPorUsuario>> ObtenerCategoriasPorUsuario(int idUsuario)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var pgAnd = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
                    pgAnd.Predicates.Add(Predicates.Field<CategoriaPorUsuario>(c => c.UsuarioId, Operator.Eq, idUsuario));
                    var categorias = (connection.GetList<CategoriaPorUsuario>(pgAnd).ToList());
                    connection.Close();
                    return categorias;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EliminarCategoriaPorUsuario(int idUsuario, int idCategoria)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    var query = $"delete from CategoriasPorUsuarios where UsuarioId = {idUsuario} and CategoriaId = {idCategoria};";
                    await Task.Run(() => connection.Execute(query));
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task InsertarCategoriasPorUsuario(int idUsuario, List<int> idsCategorias)
        {
            StringBuilder sb = new StringBuilder("");

            foreach (int idCategoria in idsCategorias){
                string insert = $"insert into CategoriasPorUsuarios (Id, UsuarioId, CategoriaId) values ('{idUsuario}_{idCategoria}', {idUsuario}, {idCategoria});";
                sb.Append(insert);
            }           

            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await Task.Run(() => connection.Execute(sb.ToString())); //TODO chequear si algun insert falla, ¿que pasa?

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InsertarCategoriaPorUsuario(int idUsuario, int idCategoria)
        {
            try
            {
                string insert = $"insert into CategoriasPorUsuarios (Id, UsuarioId, CategoriaId) values ('{idUsuario}_{idCategoria}', {idUsuario}, {idCategoria});";
                
                using (IDbConnection connection = _context.CreateConnection())
                {
                    connection.Open();
                    await Task.Run(() => connection.Execute(insert));

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Categoria para usuario {idUsuario}_{idCategoria} ya configurada");
                //throw ex;
            }
        }
    }
}
