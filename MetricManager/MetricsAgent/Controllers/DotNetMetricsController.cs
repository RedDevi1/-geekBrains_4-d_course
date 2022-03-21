using Microsoft.AspNetCore.Http;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository repository;
        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            this.repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotnetMetricCreateRequest request)
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            repository.Create(new DotnetMetric
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
            var response = new AllDotnetMetricsResponse()
            {
                Metrics = new List<DotnetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotnetMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            return Ok(response);
        }
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCount([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            return Ok();
        }
    }
}
