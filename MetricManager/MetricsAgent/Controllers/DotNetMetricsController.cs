using Microsoft.AspNetCore.Http;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using System.Collections.Generic;
using MetricsAgent.Metrics;
using MetricsAgent.DAL.Interfaces;
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
        private readonly IMapper mapper;
        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            this.repository = repository;
            this.mapper = mapper;
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

            IList<DotnetMetric> metrics = repository.GetAll();
            var response = new AllDotnetMetricsResponse()
            {
                Metrics = new List<DotnetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<DotnetMetricDto>(metric));
            }
            return Ok(response);
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCount([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IList<DotnetMetric> GetMetricsWithoutPercentiles([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            return repository.GetByTimePeriod(fromTime, toTime);
        }
    }
}
