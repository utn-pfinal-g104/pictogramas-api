using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class EstadisticaMapping : ClassMapper<Estadistica>
    {
        public EstadisticaMapping()
        {
            Table("Estadisticas");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.Identificacion).Column("Identificacion");
            Map(x => x.Pictograma).Column("Pictograma");
            Map(x => x.PictogramaPrevio).Column("PictogramaPrevio");
            Map(x => x.Fecha).Column("Fecha");
            Map(x => x.Usuario).Column("Usuario");
        }
    }
}
