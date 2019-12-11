using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CognitiveServices.Examples.TranslateApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Wat wil je vertalen?");
            var input = Console.ReadLine();
            using var client = new HttpClient();
            var token = await GetTokenAsync(client);
            while (!input.Equals("q"))
            {
                var body = new object[] { new { Text = input } };
                using var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from=nl&to=en"),
                    Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
                };

                request.Headers.Add("Authorization", $"Bearer {token}");
                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                Console.WriteLine("Wat wil je vertalen?");
                input = Console.ReadLine();
            }
            Console.WriteLine("We're done here ...");
        }

        #region Private methods

        private static async Task<string> GetTokenAsync(HttpClient client)
        {
            using var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri("https://cse-tt-01.cognitiveservices.azure.com/sts/v1.0/issuetoken");
            request.Headers.Add("Ocp-Apim-Subscription-Key", "2564c9ae1e2445f992c0c99c0e7f2299");
            request.Headers.Add("Ocp-Apim-Subscription-Region", "westeurope");
            var response = await client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }

        #endregion
    }
}