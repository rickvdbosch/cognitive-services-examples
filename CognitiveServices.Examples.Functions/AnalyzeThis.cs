using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;

using CognitiveServices.Examples.Functions.ClientCredentials;

namespace CognitiveServices.Examples.Functions
{
    public static class AnalyzeThis
    {
        /// <summary>
        /// This Function connects to a Text Analytics Cognitive Service using a <see cref="TextAnalyticsClient"/>.
        /// </summary>
        /// <param name="req">The incoming request</param>
        /// <param name="log">An <see cref="ILogger"/> for logging</param>
        /// <returns>An <see cref="OkObjectResult"/> containing the response</returns>
        [FunctionName("AnalyzeThis")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string text;

            // Get the input text from the request
            using (var reader = new StreamReader(req.Body))
            {
                text = await reader.ReadToEndAsync();
            }
            // - Setup the ApiKeyServiceClientCredentials
            // - Setup the TextAnalyticsClient
            // - Call client.SentimentAsync() and process the results
            var credentials = new ApiKeyServiceClientCredentials(Constants.TA_KEY);
            var client = new TextAnalyticsClient(credentials)
            {
                Endpoint = Constants.TEXT_ANALYTICS_ENDPOINT
            };
            var result = await client.SentimentAsync(text);

            return new OkObjectResult(result);
        }
    }
}