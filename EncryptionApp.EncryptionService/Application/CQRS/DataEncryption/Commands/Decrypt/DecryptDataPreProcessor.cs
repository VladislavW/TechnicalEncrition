using System.Threading;
using System.Threading.Tasks;
using EncryptionApp.EncryptionService.Application.Services;
using MediatR.Pipeline;

namespace EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Decrypt
{
    public sealed class DecryptDataPreProcessor : IRequestPreProcessor<DecryptDataCommand>
    {
        private readonly IAesKeyService _service;

        public DecryptDataPreProcessor(IAesKeyService service)
        {
            _service = service;
        }

        public Task Process(DecryptDataCommand request, CancellationToken cancellationToken)
        {
            var key = _service.GetLastRevisionKey();
            request.EnrichByEncryptionKey(key);
            return Task.CompletedTask;
        }
    }
}