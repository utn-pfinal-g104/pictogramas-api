using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class CeldaPizarraMapping : ClassMapper<CeldaPizarra>
    {
        public CeldaPizarraMapping()
        {
            Table("CeldaPizarra");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.PizarraId).Column("PizarraId");
            Map(x => x.Fila).Column("Fila");
            Map(x => x.Columna).Column("Columna");
            Map(x => x.TipoContenido).Column("TipoContenido");
            Map(x => x.Contenido).Column("Contenido");
            Map(x => x.Color).Column("Color");
            Map(x => x.Identificacion).Column("Identificacion");
        }
    }
}
