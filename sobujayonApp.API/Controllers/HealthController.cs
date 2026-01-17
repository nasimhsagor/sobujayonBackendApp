using Microsoft.AspNetCore.Mvc;
using System;

namespace sobujayonApp.API.Controllers
{
    [Route("health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "Healthy", time = DateTime.UtcNow });
        }
    }
}
