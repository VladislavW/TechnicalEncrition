using System.Threading.Tasks;
using EncryptionApp.EncryptionService.Application.CQRS.KeyRotations.Commands.Rotates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionApp.EncryptionService.Api
{
    [AllowAnonymous]
    [Route("api/key-rotation")]
    public sealed class KeyRotationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KeyRotationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("rotates")]
        public async Task<IActionResult> Rotates()
        {
            var result = await _mediator.Send(new RotatesKeyCommand());
            return Ok(result);
        }
    }
}