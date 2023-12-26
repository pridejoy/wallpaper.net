
using Newtonsoft.Json;
using SqlSugar;
using Wallpaper.Net.Repository.Model;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //初始数据库

            var ConnectionString = File.ReadAllText("D:\\db2.txt");

             SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });

            Type[] types = typeof(ConsoleApp1.Program).Assembly.GetTypes()
              .Where(it => it.FullName.Contains("Wallpaper."))//命名空间过滤，当然你也可以写其他条件过滤
              .ToArray();


            string dbpath = Path.Combine(AppContext.BaseDirectory, "InitData");

            #region 生成脚本文件

            //foreach (var type in types)
            //{
            //    var tablename = type.Name;
            //    var list = db.Queryable<dynamic>().AS(tablename).Take(200).ToList();//没实体一样
            //    File.WriteAllText(dbpath + $"/{tablename}.txt", JsonConvert.SerializeObject(list));
            //} 

           // var list = db.Queryable<bs_bingwallpaper>()
           //      .OrderByDescending(x=>x.StartDate)
           //     .Take(365).ToList();//没实体一样
           // File.WriteAllText(dbpath + $"/bs_bingwallpaper.txt", JsonConvert.SerializeObject(list));

           // var bs_gallerylist = db.Queryable<bs_gallery>() 
           //.Take(500).ToList();//没实体一样
           // File.WriteAllText(dbpath + $"/bs_gallery.txt", JsonConvert.SerializeObject(bs_gallerylist));

             
            #endregion

            db.DbMaintenance.CreateDatabase();
             

            db.CodeFirst.SetStringDefaultLength(200).InitTables(types);
            //根据types创建表

            //初始化数据
            var wechatconfig = new bs_wx_config
            {
                WechatAppID = "wx7560d334dd837f70",
                WechatAppSecret = "你自己小程序的密钥",
                WechatAppName = "迷恋图库",
                UpTime = DateTime.Now,
                AccessToken="1",
                ExpireTime=1200,
                NextUpTime=DateTime.Now,
                Res="初始化"
                
            };

            db.Storageable(wechatconfig).ToStorage().AsInsertable.ExecuteCommand();

            string path = Path.Combine(AppContext.BaseDirectory, "InitData");
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles("*.txt");
            foreach (var file in files)
            {
                using var reader = file.OpenText();
                string s = reader.ReadToEnd();
                var table = JsonConvert.DeserializeObject<System.Data.DataTable>(s);
                if (table.Rows.Count == 0)
                {
                    continue;
                }
                table.TableName = file.Name.Replace(".txt", "");

                db.Storageable(table).WhereColumns("Id").ToStorage().AsInsertable.ExecuteCommand();
            }

        }
    }
}
