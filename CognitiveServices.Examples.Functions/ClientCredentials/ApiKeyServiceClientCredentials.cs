using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Rest;

namespace CognitiveServices.Examples.Functions.ClientCredentials
{
    public class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        #region Fields

        private readonly string apiKey;

        #endregion

        #region Constructors

        public ApiKeyServiceClientCredentials(string apiKey)
        {
            this.apiKey = apiKey;
        }

        #endregion

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Headers.Add(Constants.HEADER_NAME, apiKey);

            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}