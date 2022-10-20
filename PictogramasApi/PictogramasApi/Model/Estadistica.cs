using System;

namespace PictogramasApi.Model
{
    public class Estadistica
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public int Pictograma { get; set; }
        public int? PictogramaPrevio { get; set; }
        public DateTime Fecha { get; set; }
        public int Usuario { get; set; }
    }
}
