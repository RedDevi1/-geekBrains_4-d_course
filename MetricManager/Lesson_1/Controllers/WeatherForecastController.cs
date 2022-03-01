using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson_1.Controllers
{
    [ApiController]
    [Route("WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private SortedList<WeatherForecast, WeatherForecast> _holderOfTheTemperature;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromQuery] DateTime time, [FromQuery] int temperature)
        {
            if (!_holderOfTheTemperature.ContainsKey(time))
            {
                var curWeather = new WeatherForecast { Date = time, TemperatureC = temperature };
                _holderOfTheTemperature.Add(curWeather.Date, curWeather);
            }               
            else
                throw new ArgumentException($"An element with Key = {time} already exists.");
            return Ok();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(DateTime begining, DateTime end)
        {
            return Enumerable.TakeWhile(_holderOfTheTemperature.Values, index => )
            .ToArray();
        }

        [HttpPut]
        public IActionResult Update([FromQuery] string stringsToUpdate, [FromQuery] string newValue)
        {
            //foreach (var date in )
            return Ok();
        }
    }
}
