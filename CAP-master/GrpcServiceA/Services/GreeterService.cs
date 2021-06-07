using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceA
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
        public override Task<HiReply> SayHi(HiRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HiReply
            {
                Message1 = "Hi " + request.Name1
            });
        }
        public override Task<HelloWorldReply> SayHelloWorld(HelloWorldRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloWorldReply
            {
                Message2 = "HelloWorld " + request.Name2
            });
        }
        public override Task<HelloWorldReply> SayHelloWorld1(HelloWorldRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloWorldReply
            {
                Message2 = "HelloWorld1 " + request.Name2
            });
        }
    }
}
