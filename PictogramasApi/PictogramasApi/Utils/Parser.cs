using System;
using System.IO;
using System.Text;

namespace PictogramasApi.Utils
{
    public static class Parser
    {
        // Posibilidad para convertir en base64 para mostrar en la web
        public static string ConvertToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(bytes);
        }
    }
}
