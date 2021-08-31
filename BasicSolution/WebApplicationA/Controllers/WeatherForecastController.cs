using BasicClassLibrary;
using DBModels;
using Grpc.Net.Client;
using GrpcServiceA;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationA.Controllers
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
        [Route("Get")]
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
        [HttpGet]
        [Route("Get123")]
        public ApiResult Get123([FromHeader] string author, [FromQuery] int id)
        {
            var result = new ApiResult();
            result.Data = id.ToString();
            result.Code = 200;
            if (string.IsNullOrEmpty(author))
            {
                result.Code = 404;
            }
            result.Msg = author;
            return result;

        }
        [HttpPost]
        [Route("CreateHello")]
        public async Task<ApiResult> CreateHello([FromBody] yaeherpatientdoctor user)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "liyi" });
            ApiResult result = new ApiResult(200, "success", "Greeter 服务返回数据: " + user.DoctorID + user.DoctorName + reply.Message);
            return result;
        }

        [HttpPost]
        [Route("CreateHi")]
        public async Task<ApiResult> CreateHi(string id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHiAsync(
                new HiRequest { Name1 = "liyi" });
            ApiResult result = new ApiResult(200, "success", "Greeter 服务返回数据: " + id + reply.Message1);
            return result;
        }
        [HttpPost]
        [Route("CreateHelloWorld")]
        public async Task<ApiResult> CreateHelloWorld(string id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloWorldAsync(
                new HelloWorldRequest { Name2 = "liyi" });
            ApiResult result = new ApiResult(200, "success", "Greeter 服务返回数据: " + id + reply.Message2);
            return result;
        }
        [HttpPost]
        [Route("CreateHelloWorld1")]
        public async Task<ApiResult> CreateHelloWorld1(string id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloWorld1Async(
                new HelloWorldRequest { Name2 = "liyi" });
            ApiResult result = new ApiResult(200, "success", "Greeter1 服务返回数据: " + id + reply.Message2);
            return result;
        }
    }

}
