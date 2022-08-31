using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class PizarraMapping : ClassMapper<Pizarra>
    {
        public PizarraMapping()
        {
            Table("CeldaPizarra");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.Filas).Column("Filas");
            Map(x => x.Columnas).Column(".");

        }
    }
}
