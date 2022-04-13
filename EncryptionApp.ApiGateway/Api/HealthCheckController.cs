using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionApp.ApiGateway.Api
{
    [AllowAnonymous]
    public sealed class HealthCheckController : ControllerBase
    {
        [HttpGet("api/ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}