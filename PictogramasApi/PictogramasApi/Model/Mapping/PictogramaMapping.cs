using DapperExtensions.Mapper;

namespace PictogramasApi.Model.Mapping
{
    public class PictogramaMapping : ClassMapper<Pictograma>
    {
        public PictogramaMapping()
        {
            Table("Pictogramas");
            Map(x => x.Id).Column("Id").Key(KeyType.Identity);
            Map(x => x.Hair).Column("Hair");
            Map(x => x.Schematic).Column("Schematic");
            Map(x => x.Sex).Column("Sex");
            Map(x => x.Skin).Column("Skin");
            Map(x => x.Violence).Column("Violence");
            Map(x => x.Aac).Column("Aac");
            Map(x => x.AacColor).Column("AacColor");
            Map(x => x.IdArasaac).Column("IdArasaac");
            Map(x => x.IdUsuario).Column("IdUsuario");
            Map(x => x.Categorias).Ignore();
            Map(x => x.Tags).Ignore();
            Map(x => x.Keywords).Ignore();
            Map(x => x.UltimaActualizacion).Column("UltimaActualizacion");
        }
    }
}
