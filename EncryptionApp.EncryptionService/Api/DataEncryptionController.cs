using System.Threading.Tasks;
using EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Decrypt;
using EncryptionApp.EncryptionService.Application.CQRS.DataEncryption.Commands.Encrypt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionApp.EncryptionService.Api
{
    [AllowAnonymous]
    [Route("api/data-encryption")]
    public sealed class DataEncryptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DataEncryptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("encrypt")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Encrypt([FromBody] string data)
        {
            var result = await _mediator.Send(new EncryptDataCommand(data));
            return Ok(result);
        }

        [HttpPost("decrypt")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Decrypt([FromBody] string encryptedData)
        {
            var result = await _mediator.Send(new DecryptDataCommand(encryptedData));
            return Ok(result);
        }
    }
}