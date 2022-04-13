using System.Threading;
using System.Threading.Tasks;

namespace EncryptionApp.ApiGateway.Application.HttpClients
{
    public interface IEncryptionServiceHttpClient
    {
        Task<string> EncryptAsync(string data, CancellationToken cancellationToken = default);
        Task<string> DecryptAsync(string data, CancellationToken cancellationToken = default);
    }
}