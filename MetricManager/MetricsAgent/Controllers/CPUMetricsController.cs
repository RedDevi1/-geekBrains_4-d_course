using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsWithPercentiles([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] int percentile)
        {
            return Ok();
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsWithoutPercentiles([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
