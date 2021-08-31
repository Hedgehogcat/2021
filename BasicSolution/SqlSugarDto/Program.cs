using SqlSugar;
using System;

namespace SqlSugarDto
{
    class Program
    {
        static void Main(string[] args)
        {
            // 基目录，由程序集冲突解决程序用来探测程序集
            string path1 = AppDomain.CurrentDomain.BaseDirectory;
            // 拼接生成路径
            path1 = path1.Substring(0, path1.LastIndexOf("bin")).ToString()+"Models";
            // 获取模块的完整路径。
            string path2 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.ToString()+"Models";;
            // 创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Database=yaeherpatientdb;Data Source=192.168.10.3;User Id=sa;Password=123456;CharSet=utf8;port=3306",//连接符字串
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            });

            foreach (var item in db.DbMaintenance.GetTableInfoList())
            {
                /*
                 *//*实体名大写*//*
                string entityName = item.Name.ToUpper();
                  *//*首字母大写*//*
                string entityName =  item.Name.Substring(0,1).ToUpper()+item.Name.Substring(1); 

                db.MappingTables.Add(entityName, item.Name);
                foreach (var col in db.DbMaintenance.GetColumnInfosByTableName(item.Name))
                {
                    db.MappingColumns.Add(col.DbColumnName.ToUpper() *//*类的属性大写*//*, col.DbColumnName, entityName);
                }
                */
            }


            db.DbFirst.IsCreateAttribute().CreateClassFile(path1, "DBModels");
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
