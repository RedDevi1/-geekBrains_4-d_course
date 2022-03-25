using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using System;
using System.Collections.Generic;
using MetricsAgent.Metrics;
using MetricsAgent.DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : ControllerBase
    {
        private readonly ILogger<HDDMetricsController> _logger;
        private IHddMetricsRepository repository;
        private readonly IMapper mapper;
        public HDDMetricsController(ILogger<HDDMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HDDMetricsController");
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            repository.Create(new HddMetric
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

            IList<HddMetric> metrics = repository.GetAll();
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<HddMetricDto>(metric));
            }
            return Ok(response);
        }

        [HttpGet("left")]
        public IActionResult GetFreeSpaceSizeInMegabytes()
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            return Ok();
        }
    }
}
