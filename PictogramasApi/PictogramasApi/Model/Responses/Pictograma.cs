using System;
using System.Collections.Generic;

namespace PictogramasApi.Model.Responses
{
    public class Pictograma
    {
        public bool schematic { get; set; }
        public bool sex { get; set; }
        public bool violence { get; set; }
        public bool aac { get; set; }
        public bool aacColor { get; set; }
        public bool skin { get; set; }
        public bool hair { get; set; }
        public int downloads { get; set; }
        public List<string> categories { get; set; }
        public List<string> synsets { get; set; }
        public List<string> tags { get; set; }
        public int _id { get; set; }
        public DateTime created { get; set; }
        public DateTime lastUpdated { get; set; }
        public List<Keyword> keywords { get; set; }
    }
}
