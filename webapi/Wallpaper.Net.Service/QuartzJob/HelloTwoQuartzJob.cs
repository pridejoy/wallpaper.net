using Quartz;

namespace Wallpaper.Net.Service.QuartzNET
{
    public class HelloTwoQuartzJob : IJob
    {
        /// <summary>
        /// 当前任务执行的Core表达式
        /// </summary>
        public static string Cron = "*/1 * * * * ?";

        /// <summary>
        /// 任务***
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        { 
            try
            {
                //业务处理逻辑
                Console.WriteLine("Hello2222");
            }
            catch (Exception ex)
            { 
            }  
        }
    }

}
