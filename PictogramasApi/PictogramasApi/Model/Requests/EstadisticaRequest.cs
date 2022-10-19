using System;
using System.Collections.Generic;

namespace PictogramasApi.Model.Requests
{
    public class EstadisticaRequest
    {
        public string Id { get; set; }
        public Pictograma Pictograma { get; set; }
        public Pictograma Previo { get; set; }
        public List<Pictograma> TodosLosAnteriores { get; set; }
        public DateTime Fecha { get; set; }
        public int Usuario { get; set; }
    }
}
