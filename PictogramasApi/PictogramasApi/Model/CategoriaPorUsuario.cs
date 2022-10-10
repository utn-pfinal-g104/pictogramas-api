using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Model
{
    public class CategoriaPorUsuario
    {
        public string Id { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
    }
}
