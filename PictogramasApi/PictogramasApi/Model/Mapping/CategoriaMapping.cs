﻿using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class CategoriaMapping : ClassMapper<Categoria>
    {
        public CategoriaMapping()
        {
            Table("Categorias");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.Nombre).Column("Nombre");
            Map(x => x.NombreOriginal).Column("NombreOriginal");
            Map(x => x.Nivel).Column("Nivel");
            Map(x => x.CategoriaPadre).Column("CategoriaPadre");
        }
    }
}
