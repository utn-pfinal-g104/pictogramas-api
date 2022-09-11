using System.Collections.Generic;

namespace PictogramasApi.Model
{
    public class Pizarra
    {
        public int Id { get; set; }
        public int Filas { get; set; }
        public int Columnas { get; set; }
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }

        public List<CeldaPizarra> Celdas { get; set; }
    }
}
