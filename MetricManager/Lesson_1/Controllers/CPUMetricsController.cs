using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Responses;
using MetricsManager.Requests;
using MetricsManager.Client;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : ControllerBase
    {
        private readonly ILogger<CPUMetricsController> _logger;
        private readonly IHttpClientFactory clientFactory;

        public CPUMetricsController(ILogger<CPUMetricsController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CPUMetricsController");
            this.clientFactory = clientFactory;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            return Ok();
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Привет, это мое первое сообщение в лог");
            return Ok();
        }

        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/api/metrics/cpu/from/1/to/999999?var=val&var1=val1");
        //    request.Headers.Add("Accept", "application/vnd.github.v3+json");
        //    var client = clientFactory.CreateClient();
        //    HttpResponseMessage response = client.SendAsync(request).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        using var responseStream = response.Content.ReadAsStreamAsync().Result;
        //        var metricsResponse = JsonSerializer.DeserializeAsync
        //        <AllCpuMetricsApiResponse>(responseStream).Result;
        //    }
        //    else
        //    {
        //        // ошибка при получении ответа
        //    }
        //    return Ok();
        //}

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            // логируем, что мы пошли в соседний сервис
            _logger.LogInformation($"starting new request to metrics agent");
            // обращение в сервис
            var metrics = MetricsAgentClient.GetCpuMetrics(new GetAllCpuMetricsApiRequest
            {
                FromTime = fromTime,
                ToTime = toTime
            });
            // возвращаем ответ
            return Ok(metrics);
        }
    }
}
