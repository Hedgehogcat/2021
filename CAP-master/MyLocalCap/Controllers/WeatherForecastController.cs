using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLocalCap.Controllers
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
        private readonly ICapPublisher publisher;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICapPublisher publisher)
        {
            _logger = logger;
            this.publisher = publisher;
        }

        [HttpGet]
        public async Task Publish()
        {
            await publisher.PublishAsync("test-message", DateTime.UtcNow);
        }

        [CapSubscribe("test-message")]
        [NonAction]
        public void Subscribe(DateTime date, [FromCap] IDictionary<string, string> headers)
        {
            var str = string.Join(",", headers.Select(kv => $"({kv.Key}:{kv.Value})"));
            _logger.LogInformation($"test-message subscribed with value {date}, headers : {str}");
        }
        /// <summary>
        /// 直接获取
        /// </summary>
        /// <returns></returns>
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
    }
}
