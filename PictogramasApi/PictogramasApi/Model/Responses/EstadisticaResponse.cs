using System.Collections.Generic;

namespace PictogramasApi.Model.Responses
{
    public class EstadisticaResponse
    {
        public List<Estadistica> TodasLasEstadisticas { get; set; }
        public List<int> PictogramasMasUtilizados { get; set; }
        public List<int> CategoriasMasUtilizadas { get; set; }
        public int CantidadDePictogramasDistintosUtilizados { get; set; }
        public int CantidadDeCategoriasDistintasUtilizadas { get; set; }
    }
}
