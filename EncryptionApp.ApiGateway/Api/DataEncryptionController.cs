using System.Threading.Tasks;
using EncryptionApp.ApiGateway.Application.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionApp.ApiGateway.Api
{
    [AllowAnonymous]
    [Route("api/data-encryption")]
    public sealed class DataEncryptionController : ControllerBase
    {
        private readonly IEncryptionServiceHttpClient _httpClient;

        public DataEncryptionController(IEncryptionServiceHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("encrypt")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Encrypt([FromBody] string data)
        {
            var result = await _httpClient.EncryptAsync(data);
            return Ok(result);
        }

        [HttpPost("decrypt")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Decrypt([FromBody] string encryptedData)
        {
            var result = await _httpClient.DecryptAsync(encryptedData);
            return Ok(result);
        }
    }
}