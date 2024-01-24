namespace Wallpaper.Net.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //ָ����Ŀ���Բ���ΪWindows����
            builder.Host.UseWindowsService();
            //ָ����Ŀ���Բ���ΪLinux����,���Ժ� UseWindowsService() ����
            builder.Host.UseSystemd();

             

            var app = builder.Build();  
            app.Run();
        }
    }
}
