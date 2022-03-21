using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
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
        private readonly ILogger<RAMMetricsController> _logger;
        private IRamMetricsRepository repository;
        public RAMMetricsController(ILogger<RAMMetricsController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RAMMetricsController");
            this.repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            repository.Create(new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            var metrics = repository.GetAll();
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            return Ok(response);
        }

        [HttpGet("available")]
        public IActionResult GetFreeRAMInMegabytes()
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            return Ok();
        }
    }
}
