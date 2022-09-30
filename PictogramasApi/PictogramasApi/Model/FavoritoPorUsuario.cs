using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictogramasApi.Model
{
    public class FavoritoPorUsuario
    {
        public string Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdPictograma { get; set; }
    }
}
