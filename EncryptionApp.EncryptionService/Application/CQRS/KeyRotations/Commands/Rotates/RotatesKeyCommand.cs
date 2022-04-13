using MediatR;

namespace EncryptionApp.EncryptionService.Application.CQRS.KeyRotations.Commands.Rotates
{
    public sealed record RotatesKeyCommand() : IRequest<Unit>
    {
    }
}