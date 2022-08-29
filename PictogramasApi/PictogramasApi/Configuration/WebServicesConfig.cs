using System;

namespace PictogramasApi.Configuration
{
    public class WebServicesConfig
    {
        public const string Section = "WebServices";
        public String ArasaacUri { get; set; }
        public String MicrosoftTranslator { get; set; }
        public String MicrosoftTranslatorKey { get; set; }
        public String MicrosoftTranslatorRegion { get; set; }
    }
}
