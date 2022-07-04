
namespace PictogramasApi.Model
{
    public class PalabraClave
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public int Tipo { get; set; }
        public string Meaning { get; set; }
        public string Plural { get; set; }
        public bool HasLocution { get; set; }
        public int IdPictograma { get; set; }
    }
}
