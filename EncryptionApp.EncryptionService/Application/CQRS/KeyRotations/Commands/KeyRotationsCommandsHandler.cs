using System.Threading;
using System.Threading.Tasks;
using EncryptionApp.EncryptionService.Application.CQRS.KeyRotations.Commands.Rotates;
using EncryptionApp.EncryptionService.Application.Services;
using MediatR;

namespace EncryptionApp.EncryptionService.Application.CQRS.KeyRotations.Commands
{
    public sealed class KeyRotationsCommandsHandler:
        IRequestHandler<RotatesKeyCommand>
    {
        private readonly IAesKeyService _service;

        public KeyRotationsCommandsHandler(IAesKeyService service)
        {
            _service = service;
        }
        
        public Task<Unit> Handle(RotatesKeyCommand request, CancellationToken cancellationToken)
        {
            _service.GenerateNewKey();
            return Task.FromResult(Unit.Value);
        }
    }
}