using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class PizarraMapping : ClassMapper<Pizarra>
    {
        public PizarraMapping()
        {
            Table("Pizarras");
            Map(x => x.Id).Column("Id").Key(KeyType.Assigned);
            Map(x => x.Filas).Column("Filas");
            Map(x => x.Columnas).Column("Columnas");
            Map(x => x.Nombre).Column("Nombre");
            Map(x => x.UsuarioId).Column("UsuarioId").Key(KeyType.Assigned);
        }
    }
}
