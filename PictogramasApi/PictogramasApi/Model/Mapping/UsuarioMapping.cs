using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class UsuarioMapping : ClassMapper<Usuario>
    {
        public UsuarioMapping()
        {
            Table("Usuarios");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.NombreUsuario).Column("NombreUsuario");
            Map(x => x.Password).Column("Password");
            Map(x => x.Hair).Column("Hair");
            Map(x => x.Schematic).Column("Schematic");
            Map(x => x.Sex).Column("Sex");
            Map(x => x.Skin).Column("Skin");
            Map(x => x.Violence).Column("Violence");
            Map(x => x.Aac).Column("Aac");
            Map(x => x.AacColor).Column("AacColor");
        }
    }
}
