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
            //ָ����Ŀ���Բ���ΪWindows����
            builder.Host.UseWindowsService();
            //ָ����Ŀ���Բ���ΪLinux����,���Ժ� UseWindowsService() ����
            builder.Host.UseSystemd();
             
            //ע��Sqlsugar��ط���
            builder.Services.AddSqlsugarSetup();

            // Quartz ��ʱ����
            builder.Services.AddQuartz(); 

            //services.AddHostedService<QuartzHostedService>();

            builder.Services.AddSingleton<IQuartzManager, QuartzManager>();


            var app = builder.Build();
            //��ȡ�������
            var jobService= app.Services.GetService<IJobService>();
            await jobService.StartAll();

            FileHelper.Write("����ʼ��", "run");
            app.Run();
        }
    }
}
