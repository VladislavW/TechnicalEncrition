using FluentValidation;

namespace EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Encrypt
{
    public sealed class EncryptDataValidator : AbstractValidator<EncryptDataCommand>
    {
        public EncryptDataValidator()
        {
            RuleFor(item => item.Data)
                .NotEmpty()
                .NotNull();
        }
    }
}