using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RAMMetricsController : ControllerBase
    {
        private readonly ILogger<CPUMetricsController> _logger;
        public RAMMetricsController(ILogger<CPUMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
        }
        [HttpGet("available")]
        public IActionResult GetFreeRAMInMegabytes()
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            return Ok();
        }
    }
}
