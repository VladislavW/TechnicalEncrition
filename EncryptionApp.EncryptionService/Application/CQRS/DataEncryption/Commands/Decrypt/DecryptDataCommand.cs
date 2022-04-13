using MediatR;

namespace EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Decrypt
{
    public sealed record DecryptDataCommand(string EncryptedData) : DataCommandBase, IRequest<string>
    {
    }
}