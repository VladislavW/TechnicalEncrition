using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EncryptionApp.ApiGateway.Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EncryptionApp.ApiGateway.Application.HttpClients
{
    internal sealed class EncryptionServiceHttpClient : IEncryptionServiceHttpClient
    {
        private readonly EncryptionServiceHttpClientOptions _clientsOptions;
        public Uri BaseEndpoint => new(_clientsOptions.Endpoint);
        public HttpClient HttpClient { get; }
        public string ServiceName { get; } = ServerClients.EncryptionService;

        public EncryptionServiceHttpClient(
            IOptions<EncryptionServiceHttpClientOptions> clientsOptions,
            IHttpClientFactory httpClientFactory)
        {
            _clientsOptions = clientsOptions.Value;
            HttpClient = httpClientFactory.CreateClient(ServiceName);
        }
        
        public async Task<string> EncryptAsync(string data, CancellationToken cancellationToken = default)
        {
           return await PostAsync("api/data-encryption/encrypt", data, cancellationToken);
        }

        public async Task<string> DecryptAsync(string data, CancellationToken cancellationToken = default)
        {
            return await PostAsync("api/data-encryption/decrypt", data, cancellationToken);
        }
        
        private async Task<string> PostAsync(
            string relativePath,
            object body,
            CancellationToken cancellationToken)
        {
            string response1;
            using (var response2 = await HttpClient.PostAsync(
                       new Uri(BaseEndpoint,
                           new Uri(relativePath, UriKind.Relative)).ToString(),
                       new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"),
                       cancellationToken))
            {
                response2.EnsureSuccessStatusCode();
                response1 = await response2.Content.ReadAsStringAsync();           
            }

            return response1;
        }
    }
}