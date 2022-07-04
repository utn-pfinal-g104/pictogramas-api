using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class PalabraClaveMapping : ClassMapper<PalabraClave>
    {
        public PalabraClaveMapping()
        {
            Table("Keywords");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.IdPictograma).Column("IdPictograma");
            Map(x => x.Keyword).Column("Keyword");
            Map(x => x.Meaning).Column("Meaning");
            Map(x => x.Tipo).Column("Tipo");
            Map(x => x.Plural).Column("Plural");
            Map(x => x.HasLocution).Column("HasLocution");
        }
    }
}
