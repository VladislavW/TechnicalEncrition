using MediatR;

namespace EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Encrypt
{
    public sealed record EncryptDataCommand(string Data) : DataCommandBase, IRequest<string>
    {
    }
}