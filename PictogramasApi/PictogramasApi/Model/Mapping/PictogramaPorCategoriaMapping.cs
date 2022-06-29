using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class PictogramaPorCategoriaMapping : ClassMapper<PictogramaPorCategoria>
    {
        public PictogramaPorCategoriaMapping()
        {
            Table("PictogramaPorCategoria");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.IdCategoria).Column("IdCategoria");
            Map(x => x.IdPictograma).Column("IdPictograma");
        }
    }
}
