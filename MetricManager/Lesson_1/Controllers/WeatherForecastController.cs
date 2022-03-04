using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson_1.Controllers
{
    [ApiController]
    [Route("/")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private ValuesHolder _holderOfTheTemperature;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ValuesHolder holderOfTheTemperature)
        {
            _logger = logger;
            _holderOfTheTemperature = holderOfTheTemperature;
        }

        [HttpPost, Route("Create/{time}/{temperature}")]
        public IActionResult Create([FromRoute] DateTime time, int temperature)
        {
            if (!_holderOfTheTemperature.Values.ContainsKey(time))
            {
                var weather = new WeatherForecast { Date = time, TemperatureC = temperature };
                _holderOfTheTemperature.Values.Add(weather.Date, weather);
            }               
            else
                Console.WriteLine($"An element with Key = {time} already exists.");
            return Ok();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery] DateTime begining, [FromQuery] DateTime end)
        {
            return Enumerable.TakeWhile(_holderOfTheTemperature.Values.Values, index => (index.Date >= begining && index.Date <= end))
            .ToArray();
        }

        [HttpPut]
        public IActionResult Update([FromQuery] DateTime dateToUpdate, [FromQuery] int newValue)
        {
            foreach (var w in _holderOfTheTemperature.Values)
                if (w.Key == dateToUpdate)
                    w.Value.TemperatureC = newValue;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] DateTime toDelete)
        {
            foreach (var w in _holderOfTheTemperature.Values)
                if (w.Key == toDelete)
                    _holderOfTheTemperature.Values.Remove(w.Key);
            return Ok();
        }
    }
}
