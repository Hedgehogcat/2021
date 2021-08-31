using BasicClassLibrary;
using Dapper;
using DBModels;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        //建立数据库连接
        public static MySqlConnection mysqlconn()
        {
            string ConnString = AppSetting.GetConfig("ConnectionStrings:BaseDb");
            var conntion = new MySqlConnection(ConnString);
            conntion.Open();
            return conntion;
        }

        //列表
        public List<yaeherpatientdoctor> GetList()
        {
            using (IDbConnection con = mysqlconn())
            {
                string sqlcommandtex = "select * from yaeherpatientdoctor";
                List<yaeherpatientdoctor> userlist = SqlMapper.Query<yaeherpatientdoctor>(con, sqlcommandtex).ToList();
                return userlist;
            }
        }
        

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            List<yaeherpatientdoctor> users = GetList();

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
