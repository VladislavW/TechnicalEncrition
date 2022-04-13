using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EncryptionApp.ApiGateway.Infrastructure;
using Newtonsoft.Json;

namespace EncryptionApp.ApiGateway.Application.HttpClients
{
    internal sealed class EncryptionServiceHttpClient : IEncryptionServiceHttpClient
    {
        public Uri BaseEndpoint => new("https://localhost:5001");
        public HttpClient HttpClient { get; }
        public string ServiceName { get; } = ServerClients.EncryptionService;

        public EncryptionServiceHttpClient(IHttpClientFactory httpClientFactory)
        {
            HttpClient = httpClientFactory.CreateClient(ServiceName);
        }
        
        public async Task<string> EncryptAsync(string data, CancellationToken cancellationToken = default)
        {
           return await PostAsync<string>("api/data-encryption/encrypt", new {data}, cancellationToken);
        }

        public async Task<string> DecryptAsync(string data, CancellationToken cancellationToken = default)
        {
            return await PostAsync<string>("api/data-encryption/decrypt", new {data}, cancellationToken);
        }
        
        private async Task<TResponse> PostAsync<TResponse>(
            string relativePath,
            object body,
            CancellationToken cancellationToken)
        {
            TResponse response1;
            using (var response2 = await HttpClient.PostAsync(
                       new Uri(BaseEndpoint,
                           new Uri(relativePath, UriKind.Relative)).ToString(),
                       new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"),
                       cancellationToken))
            {
                response2.EnsureSuccessStatusCode();
                response1 = JsonConvert.DeserializeObject<TResponse>(await response2.Content.ReadAsStringAsync());
            }

            return response1;
        }
    }
}