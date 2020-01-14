namespace CognitiveServices.Examples.Functions
{
    public static class Constants
    {
        public const string TA_KEY = "<YOUR_TEXTANALYSIS_KEY>";

        public const string HEADER_NAME = "Ocp-Apim-Subscription-Key";

        public const string TEXT_ANALYTICS_ENDPOINT = "https://<YOUR_TEXTANALYSIS_SERVICE>.cognitiveservices.azure.com/";

        // For when you're running the text analysis in a Docker container, connect to this endpoint.
        // public const string TEXT_ANALYTICS_ENDPOINT = "http://localhost:5000/";
    }
}