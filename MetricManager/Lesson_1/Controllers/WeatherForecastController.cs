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
        private SortedList<DateTime, WeatherForecast> _holderOfTheTemperature;
        private WeatherForecast weather;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromQuery] DateTime time, [FromQuery] int temperature)
        {
            if (!_holderOfTheTemperature.ContainsKey(time))
            {
                weather = new WeatherForecast { Date = time, TemperatureC = temperature };
                _holderOfTheTemperature.Add(weather.Date, weather);
            }               
            else
                throw new ArgumentException($"An element with Key = {time} already exists.");
            return Ok();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery] DateTime begining, [FromQuery] DateTime end)
        {
            return Enumerable.TakeWhile(_holderOfTheTemperature.Values, index => (index.Date >= begining && index.Date <= end))
            .ToArray();
        }

        [HttpPut]
        public IActionResult Update([FromQuery] DateTime dateToUpdate, [FromQuery] int newValue)
        {
            foreach (var w in _holderOfTheTemperature)
                if (w.Key == dateToUpdate)
                    w.Value.TemperatureC = newValue;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] DateTime toDelete)
        {
            foreach (var w in _holderOfTheTemperature)
                if (w.Key == toDelete)
                    _holderOfTheTemperature.Remove(w.Key);
            return Ok();
        }
    }
}
