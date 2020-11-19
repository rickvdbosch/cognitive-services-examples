using System;
using System.IO;

using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;

namespace CognitiveServices.Examples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a CustomVisionPredictionClient, initialize it with the settings of your 
            // Custom Vision project and send a file over to classify.
            var client = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(Constants.CV_KEY))
            {
                Endpoint = Constants.ENDPOINT
            };
            
            using var stream = File.Open("<YOUR_TESTFILE_NAME>", FileMode.Open);
            var prediction = client.ClassifyImage(Constants.PROJECT_ID, Constants.PUBLISHED_NAME, stream);
            foreach (var pred in prediction.Predictions)
            {
                Console.WriteLine($"{pred.TagName}: {pred.Probability}");
            }
            Console.ReadLine();
        }
    }
}
