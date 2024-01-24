namespace Wallpaper.Net.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //指定项目可以部署为Windows服务
            builder.Host.UseWindowsService();
            //指定项目可以部署为Linux服务,可以和 UseWindowsService() 共存
            builder.Host.UseSystemd();

             

            var app = builder.Build();  
            app.Run();
        }
    }
}
