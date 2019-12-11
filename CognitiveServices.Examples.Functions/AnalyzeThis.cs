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
        [FunctionName("AnalyzeThis")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string text;

            using (var reader = new StreamReader(req.Body))
            {
                text = await reader.ReadToEndAsync();
            }
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