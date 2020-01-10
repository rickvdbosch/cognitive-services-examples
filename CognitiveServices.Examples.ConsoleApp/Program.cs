using System;
using System.IO;

using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;

namespace CognitiveServices.Examples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new CustomVisionPredictionClient
            {
                ApiKey = Constants.CV_KEY,
                Endpoint = Constants.ENDPOINT
            };
            using var stream = File.Open(@"c:\temp\testfile01.jpg", FileMode.Open);
            var prediction = client.ClassifyImage(Constants.PROJECT_ID, Constants.PUBLISHED_NAME, stream);
            foreach (var pred in prediction.Predictions)
            {
                Console.WriteLine($"{pred.TagName}: {pred.Probability}");
            }
            Console.ReadLine();
        }
    }
}
