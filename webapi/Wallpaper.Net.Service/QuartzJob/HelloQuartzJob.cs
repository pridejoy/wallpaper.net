using Newtonsoft.Json;
using Quartz;
using SqlSugar;
using Wallpaper.Net.Service.Entity; 

namespace Wallpaper.Net.Service.QuartzNET
{
    public class HelloQuartzJob : IJob
    {
        /// <summary>
        /// 当前任务执行的Core表达式
        /// </summary>
        public static string Cron = "*/1 * * * * ?"; 

        private readonly ISqlSugarClient _db;

        public HelloQuartzJob(ISqlSugarClient db)
        {
            _db = db;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                //业务处理逻辑
                Console.WriteLine("111111111111111");

                // 查询数据库表
                var date = await _db.Queryable<bs_gallery>()
                    .Where(x => x.IsDelete == false)
                    .OrderBy(x => SqlFunc.GetRandom())
                    .FirstAsync();

                Console.WriteLine(JsonConvert.SerializeObject(date));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                FileHelper.Write(ex.Message,"error");
            }
        }
    }

}
