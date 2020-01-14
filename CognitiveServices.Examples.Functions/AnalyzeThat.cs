using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CognitiveServices.Examples.Functions
{
    public static class AnalyzeThat
    {
        /// <summary>
        /// This Function directly connects to a Text Analytics Cognitive Service endpoint.
        /// </summary>
        /// <param name="req">The incoming request</param>
        /// <param name="log">An <see cref="ILogger"/> for logging</param>
        /// <returns>An <see cref="OkObjectResult"/> containing the response</returns>
        [FunctionName("AnalyzeThat")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string text;
            string result = string.Empty;

            // Get the input text from the request
            using (var reader = new StreamReader(req.Body))
            {
                text = await reader.ReadToEndAsync();
            }

            // - Connect to the Text Analytics endpoint
            // - Add the authorization header
            // - Send the request to the endpoint and process the response
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.TEXT_ANALYTICS_ENDPOINT);
                client.DefaultRequestHeaders.Add(Constants.HEADER_NAME, Constants.TA_KEY);

                byte[] byteData = Encoding.UTF8.GetBytes(text);
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("text/analytics/v2.1/sentiment", content);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
            };

            return new OkObjectResult(result);
        }
    }
}