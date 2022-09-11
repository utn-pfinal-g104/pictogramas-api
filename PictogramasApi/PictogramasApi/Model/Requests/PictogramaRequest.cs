using System.Collections.Generic;

namespace PictogramasApi.Model.Requests
{
    public class PictogramaRequest
    {
        public bool Schematic { get; set; }
        public bool Sex { get; set; }
        public bool Violence { get; set; }
        public bool Aac { get; set; }
        public bool AacColor { get; set; }
        public bool Skin { get; set; }
        public bool Hair { get; set; }
        public string File { get; set; }
        public string FileName { get; set; }
        public string Keyword { get; set; }
        public List<Categoria> CategoriasFiltradas {get; set;}
    }

}
