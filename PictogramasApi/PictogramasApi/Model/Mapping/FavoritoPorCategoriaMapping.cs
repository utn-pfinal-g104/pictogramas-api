using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Model.Mapping
{
    public class FavoritoPorCategoriaMapping : ClassMapper<FavoritoPorUsuario>
    {
        public FavoritoPorCategoriaMapping()
        {
            Table("FavoritosPorUsuarios");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.IdUsuario).Column("UsuarioId");
            Map(x => x.IdPictograma).Column("PictogramaId");
        }
        
    }
}
