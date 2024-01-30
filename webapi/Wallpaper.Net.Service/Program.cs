using Quartz.Impl;
using Quartz;
using Wallpaper.Net.Service.QuartzNET; 

namespace Wallpaper.Net.Service
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //指定项目可以部署为Windows服务
            builder.Host.UseWindowsService();
            //指定项目可以部署为Linux服务,可以和 UseWindowsService() 共存
            builder.Host.UseSystemd();
             
            //注册Sqlsugar相关服务
            builder.Services.AddSqlsugarSetup();

            // Quartz 定时任务
            builder.Services.AddQuartz(); 

            //services.AddHostedService<QuartzHostedService>();

            builder.Services.AddSingleton<IQuartzManager, QuartzManager>();


            var app = builder.Build();
            //获取任务服务
            var jobService= app.Services.GetService<IJobService>();
            await jobService.StartAll();

            FileHelper.Write("任务开始了", "run");
            app.Run();
        }
    }
}
