using System;
using System.Net;
using System.Security.Cryptography;
using EncryptionApp.EncryptionService.Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace EncryptionApp.EncryptionService.Infrastructure.Helpers
{
    internal static class ErrorHelper
    {
        public static ErrorResponse CreateErrorResponse(Exception exception, bool includeStackTrace = false)
        {
            if (exception == null)
            {
                return null;
            }

            ErrorResponse errorResponse;

            switch (exception)
            {
                case FluentValidation.ValidationException ex:
                    errorResponse = new ErrorResponse(HttpStatusCode.BadRequest, ex.Message, JsonConvert.SerializeObject(ex.Errors));
                    break;
                
                case CryptographicException ex:
                    errorResponse = new ErrorResponse(HttpStatusCode.Conflict, ex.Message);
                    break;
                
                case AesKeyGeneratedException ex:
                    errorResponse = new ErrorResponse(HttpStatusCode.PreconditionFailed, ex.Message);
                    break;

                case { } ex:
                    errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error",
                        ex.Message);
                    break;

                default:
                    return null;
            }

            if (includeStackTrace)
            {
                errorResponse.SetStackTrace(exception.StackTrace);
            }

            return errorResponse;
        }
    }
}