using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rollbar;

namespace RollbarDemoREACT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;


        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            RollbarLocator.RollbarInstance.Configure(new RollbarConfig("f987f9ea6fdf4d4d9458aab45ec7d3cf"));
            //RollbarLocator.RollbarInstance.Info("Rollbar is configured properly.");

            try
            {

                var rng = new Random();

                //throw new InvalidOperationException("data object cannot be XXXX");

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
                              

            } catch (System.Exception ex) {
                RollbarLocator.RollbarInstance.AsBlockingLogger(TimeSpan.FromSeconds(1)).Error(ex);

                return null;
            }

        }
    }
}
