using System.Collections.Generic;

namespace PictogramasApi.Model.Responses
{
    public class TraduccionResponse
    {
        public List<Traducciones> Translations { get; set; }
    }

    public class Traducciones
    {
        public string Text { get; set; }
        public string To { get; set; }
    }
}
