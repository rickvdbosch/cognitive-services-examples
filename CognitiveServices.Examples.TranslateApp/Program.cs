using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CognitiveServices.Examples.TranslateApp
{
    class Program
    {
        #region Constants

        private const string TEXT_TRANSLATION_SERVICE = "<YOUR_TEXTRANSLATION_SERVICE>";
        private const string TEXT_TRANSLATION_KEY = "<YOUR_TEXTTRANSLATION_KEY>";
        private const string TEXT_TRANSLATION_LOCATION = "<YOUR_TEXTTRANSLATION_LOCATION>";

        private const string TRANSLATE = "What would you like to translate?";

        #endregion

        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            var token = await GetTokenAsync(client);

            Console.WriteLine(TRANSLATE);
            var input = Console.ReadLine();
            
            // As long as the input is NOT 'q', translate the text fron English to Dutch.
            while (!input.Equals("q"))
            {
                // Create an HttpRequestMessage to send to the translator service.
                var body = new object[] { new { Text = input } };
                using var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from=en&to=nl"),
                    Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
                };
                request.Headers.Add("Authorization", $"Bearer {token}");

                // Send the request and process the result.
                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                Console.WriteLine(TRANSLATE);
                input = Console.ReadLine();
            }

            Console.WriteLine("We're done here ...");
        }

        #region Private methods

        /// <summary>
        /// Gets an access token for a TextTranslation Cognitive Service
        /// </summary>
        /// <param name="client">The <see cref="HttpClient"/> to use for the request.</param>
        /// <returns>An access token.</returns>
        private static async Task<string> GetTokenAsync(HttpClient client)
        {
            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(TEXT_TRANSLATION_SERVICE)
            };
            request.Headers.Add("Ocp-Apim-Subscription-Key", TEXT_TRANSLATION_KEY);
            request.Headers.Add("Ocp-Apim-Subscription-Region", TEXT_TRANSLATION_LOCATION);
            var response = await client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }

        #endregion
    }
}