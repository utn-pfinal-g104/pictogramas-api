using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PictogramasApi.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PictogramasApi.Services
{
    public class GoogleTranslateService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IOptions<WebServicesConfig> WebServices { get; set; }

        public GoogleTranslateService(IOptions<WebServicesConfig> webServices, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            WebServices = webServices;
        }

        public async Task<string> TraducirTexto(string textoOriginal)
        {
            object[] body1 = new object[] { new { Text = textoOriginal } };
            var requestBody1 = JsonConvert.SerializeObject(body1);
            string result;
            using (var client = new HttpClient())
            using (var request1 = new HttpRequestMessage())
            {
                // Build the request.
                request1.Method = HttpMethod.Post;
                request1.RequestUri = new Uri(WebServices.Value.MicrosoftTranslator + "?api-version=3.0&from=es&to=en");
                request1.Content = new StringContent(requestBody1, Encoding.UTF8, "application/json");
                request1.Headers.Add("Ocp-Apim-Subscription-Key", WebServices.Value.MicrosoftTranslatorKey);
                request1.Headers.Add("Ocp-Apim-Subscription-Region", WebServices.Value.MicrosoftTranslatorRegion);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request1).ConfigureAwait(false);
                // Read response as a string.
                result = await response.Content.ReadAsStringAsync();
            }

            object[] body2 = new object[] { new { Text = result } };
            var requestBody2 = JsonConvert.SerializeObject(body2);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(WebServices.Value.MicrosoftTranslator + "?api-version=3.0&from=en&to=es");
                request.Content = new StringContent(requestBody2, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", WebServices.Value.MicrosoftTranslatorKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", WebServices.Value.MicrosoftTranslatorRegion);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
