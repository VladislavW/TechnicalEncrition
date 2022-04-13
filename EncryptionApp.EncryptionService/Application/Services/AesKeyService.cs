using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography;
using EncryptionApp.EncryptionService.Infrastructure.Exceptions;

namespace EncryptionApp.EncryptionService.Application.Services
{
    public interface IAesKeyService
    {
        void GenerateNewKey();
        byte[] GetLastRevisionKey();
    }

    internal sealed class AesKeyService : IAesKeyService
    {
        private ConcurrentDictionary<int, byte[]> _keys = new();

        public AesKeyService()
        {
            GenerateNewKey();
        }

        public void GenerateNewKey()
        {
            using (var aes = Aes.Create())
            {
                var lastKey = _keys.LastOrDefault();
                aes.GenerateKey();
                
                if (lastKey.Value == null)
                {
                    if (!_keys.TryAdd(0, aes.Key))
                    {
                        throw new AesKeyGeneratedException();
                    }
                }

                var oldRevision = lastKey.Key;
                var newRevision = oldRevision + 1;
                if (!_keys.TryAdd(newRevision, aes.Key))
                {
                    throw new AesKeyGeneratedException();
                }
            }
        }

        public byte[] GetLastRevisionKey()
        {
            if (_keys == null || !_keys.Any())
            {
                throw new AesKeyGeneratedException();
            }

            var latestKey = _keys.LastOrDefault();

            if (latestKey.Value == null)
            {
                throw new AesKeyGeneratedException();
            }

            return latestKey.Value;
        }
    }
}