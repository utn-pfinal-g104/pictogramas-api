
namespace PictogramasApi.Model
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CategoriaPadre { get; set; }

        public int Nivel { get; set; }
    }
}
