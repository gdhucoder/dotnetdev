using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLayerWebApiDemo.Model;
using NLayerWebApiDemo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerWebApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly ICatService _catService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                        ICatService catService)
        {
            _logger = logger;
            _catService = catService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost]
        public Cats Add(Cats input)
        {
            _catService.Add(input);
            return input;
        }

        [HttpPost]
        public Cats Find(Cats input)
        {
            return _catService.Find(input.id); ;
        }


        [HttpGet]
        public void TestDM()
        {
            _catService.TestDM();
        }
    }
}
