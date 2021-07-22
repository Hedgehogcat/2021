using Grpc.Net.Client;
using GrpcServiceA;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
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
        public class msgBack
        {
            public msgBack(string head, string body, string code)
            {
                Head = head;
                Body = body;
                Code = code;
            }
            public msgBack() { }
            public string Head { get; set; }
            public string Body { get; set; }
            public string Code { get; set; }
        }
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
        public msgBack Get123([FromHeader]string author,[FromQuery]int id)
        {
            var Back = new msgBack();
             Back.Head = id.ToString();
             Back.Code = "200";
            if (string.IsNullOrEmpty(author)){
                Back.Code = "404";
            }
            Back.Body = author;
            return Back;

        }
        [HttpPost]
        [Route("CreateHello")]
        public async Task<msgBack> CreateHello([FromBody]User user)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "liyi" });
            msgBack back = new msgBack(user.personname, "Greeter 服务返回数据: " + reply.Message, "success");
            return back;
        }

        [HttpPost]
        [Route("CreateHi")]
        public async Task<msgBack> CreateHi(string id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHiAsync(
                new HiRequest { Name1 = "liyi" });
            msgBack back = new msgBack(id, "Greeter 服务返回数据: " + reply.Message1, "success");
            return back;
        }
        [HttpPost]
        [Route("CreateHelloWorld")]
        public async Task<msgBack> CreateHelloWorld(string id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloWorldAsync(
                new HelloWorldRequest { Name2 = "liyi" });
            msgBack back = new msgBack(id, "Greeter 服务返回数据: " + reply.Message2, "success");
            return back;
        }
        [HttpPost]
        [Route("CreateHelloWorld1")]
        public async Task<msgBack> CreateHelloWorld1(string id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloWorld1Async(
                new HelloWorldRequest { Name2 = "liyi" });
            msgBack back = new msgBack(id, "Greeter1 服务返回数据: " + reply.Message2, "success");
            return back;
        }
    }

}
