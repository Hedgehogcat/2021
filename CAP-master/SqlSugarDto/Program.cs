using SqlSugar;
using System;

namespace SqlSugarDto
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Database=mysql_test;Data Source=192.168.10.101;User Id=root;Password=123456;CharSet=utf8;port=3306",//连接符字串
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            });

            db.DbFirst.IsCreateAttribute().CreateClassFile("f:\\1", "Models");
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
