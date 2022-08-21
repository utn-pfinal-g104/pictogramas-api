using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Model.Mapping
{
    public class CategoriaPorUsuarioMapping : ClassMapper<CategoriaPorUsuario>
    {
        public CategoriaPorUsuarioMapping()
        {
            Table("CategoriasPorUsuarios");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.UsuarioId).Column("UsuarioId");
            Map(x => x.CategoriaId).Column("CategoriaId");
        }
    }
}
