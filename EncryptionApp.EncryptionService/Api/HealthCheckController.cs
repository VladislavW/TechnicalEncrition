using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionApp.EncryptionService.Api
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