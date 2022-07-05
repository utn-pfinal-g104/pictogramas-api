using System;

namespace PictogramasApi.Configuration
{
    public class Neo4JConfig
    {
        public const string Section = "Neo4J";
        public String Uri { get; set; }
        public String User { get; set; }
        public String Password { get; set; }
    }
}
