using System.Text;

namespace EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands
{
    public record DataCommandBase
    {
        protected internal byte[] EncryptionKey { get; private set; }

        internal void EnrichByEncryptionKey(string encryptionKey)
        {
            EncryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
        }

        internal void EnrichByEncryptionKey(byte[] encryptionKey)
        {
            EncryptionKey = encryptionKey;
        }
    }
}