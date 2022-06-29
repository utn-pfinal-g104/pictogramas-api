using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class TagPorPictogramaMapping : ClassMapper<TagPorPictograma>
    {
        public TagPorPictogramaMapping()
        {
            Table("PictogramaPorCategoria");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.IdTag).Column("IdTag");
            Map(x => x.IdPictograma).Column("IdPictograma");
        }
    }
}
