namespace PictogramasApi.Model
{
    public class CeldaPizarra
    {
        public int Id { get; set; }
        public int PizarraId { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public string Contenido { get; set; }
        public string TipoContenido { get; set; }
        public string Color { get; set; }
        public string Identificacion { get; set; }
    }
}
