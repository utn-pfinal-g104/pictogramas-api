using System;

namespace PictogramasApi.Model
{
    public class Usuario
    {
        // Es int64 porque sino fallaba dapper extensions con el insert async
        public Int64 Id { get; set; }
        public string Identificador { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public bool Schematic { get; set; }
        public bool Sex { get; set; }
        public bool Violence { get; set; }
        public bool Aac { get; set; }
        public bool AacColor { get; set; }
        public bool Skin { get; set; }
        public bool Hair { get; set; }
        public int Nivel { get; set; }
        public DateTime UltimaActualizacion { get; set; }
    }
}
