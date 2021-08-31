using BasicClassLibrary;
using DBModels;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceB
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
            int pageIndex = 1;
            int pageSize = 20;
            int totalCount = 0;
             
            //µ•±Ì∑÷“≥
            List<yaeherpatientdoctor> page =  DbScoped.Sugar.Queryable<yaeherpatientdoctor>().ToPageList(pageIndex, pageSize, ref totalCount);


            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
