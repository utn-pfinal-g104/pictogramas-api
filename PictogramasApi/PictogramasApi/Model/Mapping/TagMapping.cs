using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class TagMapping : ClassMapper<Tag>
    {
        public TagMapping()
        {
            Table("Tags");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.Nombre).Column("Nombre");
        }
    }
}
