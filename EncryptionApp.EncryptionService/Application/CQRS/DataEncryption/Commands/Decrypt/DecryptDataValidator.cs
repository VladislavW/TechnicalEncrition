using FluentValidation;

namespace EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Decrypt
{
    public sealed class DecryptDataValidator : AbstractValidator<DecryptDataCommand>
    {
        public DecryptDataValidator()
        {
            RuleFor(item => item.EncryptedData)
                .NotEmpty()
                .NotNull();
        }
    }
}