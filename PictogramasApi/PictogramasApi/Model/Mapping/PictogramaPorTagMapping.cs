using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class PictogramaPorTagMapping : ClassMapper<PictogramaPorTag>
    {
        public PictogramaPorTagMapping()
        {
            Table("PictogramasPorTags");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.IdPictograma).Column("IdPictograma");
            Map(x => x.IdTag).Column("IdTag");
        }
    }
}
