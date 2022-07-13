using System.Collections.Generic;
using System.IO;

namespace PictogramasApi.Model
{
    public class Pictograma
    { 
        public int Id { get; set; }
        public bool Schematic { get; set; }
        public bool Sex { get; set; }
        public bool Violence { get; set; }
        public bool Aac { get; set; }
        public bool AacColor { get; set; }
        public bool Skin { get; set; }
        public bool Hair { get; set; }
        public int IdArasaac { get; set; }
        public int IdUsuario { get; set; }

        public List<Categoria> Categorias { get; set; }
        public List<Tag> Tags { get; set; }
        public List<PalabraClave> Keywords { get; set; }
    }
}
