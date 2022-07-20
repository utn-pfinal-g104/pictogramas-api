using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Model.Requests
{
    public class UsuarioRequest
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }
}
