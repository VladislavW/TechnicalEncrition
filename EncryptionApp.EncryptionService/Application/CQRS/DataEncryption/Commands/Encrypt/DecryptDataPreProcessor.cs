using System.Threading;
using System.Threading.Tasks;
using EncryptionApp.EncryptionService.Application.Services;
using MediatR.Pipeline;

namespace EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Encrypt
{
    public sealed class EncryptDataPreProcessor : IRequestPreProcessor<EncryptDataCommand>
    {
        private readonly IAesKeyService _service;

        public EncryptDataPreProcessor(IAesKeyService service)
        {
            _service = service;
        }
        
        public Task Process(EncryptDataCommand request, CancellationToken cancellationToken)
        {
            var key = _service.GetLastRevisionKey();
            request.EnrichByEncryptionKey(key);
            return Task.CompletedTask;
        }
    }
}