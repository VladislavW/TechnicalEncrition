using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Decrypt;
using EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Encrypt;
using MediatR;

namespace EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands
{
    public sealed class DataEncryptionCommandsHandler :
        IRequestHandler<DecryptDataCommand, string>,
        IRequestHandler<EncryptDataCommand, string>
    {
        public Task<string> Handle(DecryptDataCommand request, CancellationToken cancellationToken)
        {
            var iv = new byte[16];
            var buffer = Convert.FromBase64String(request.EncryptedData);

            using (var aes = Aes.Create())
            {
                aes.Key = request.EncryptionKey;
                aes.IV = iv;

                var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decrypt, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            return Task.FromResult(streamReader.ReadToEnd());
                        }
                    }
                }
            }
        }

        public Task<string> Handle(EncryptDataCommand request, CancellationToken cancellationToken)
        {
            var iv = new byte[16];
            byte[] array;

            using (var aes = Aes.Create())
            {
                aes.Key = request.EncryptionKey;
                aes.IV = iv;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(request.Data);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Task.FromResult(Convert.ToBase64String(array));
        }
    }
}