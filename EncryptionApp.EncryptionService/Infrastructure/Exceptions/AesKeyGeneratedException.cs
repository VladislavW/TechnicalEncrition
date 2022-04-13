using System;
using System.Runtime.Serialization;

namespace EncryptionApp.EncryptionService.Infrastructure.Exceptions
{
    [Serializable]
    public class AesKeyGeneratedException : Exception
    {
        public AesKeyGeneratedException() : base("Aes key is not generated")
        {
        }

        public AesKeyGeneratedException(string message) : base(message)
        {
        }

        public AesKeyGeneratedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AesKeyGeneratedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}