using Quartz.Impl;
using Quartz;
using System.Reflection;
using SqlSugar;

namespace Wallpaper.Net.Service.QuartzNET
{
    public class JobScheduler:IJobScheduler
    {
        private readonly IScheduler scheduler;
        private readonly ISqlSugarClient _db;

        public JobScheduler(ISqlSugarClient db)
        {
            scheduler = new StdSchedulerFactory().GetScheduler().GetAwaiter().GetResult(); 
            _db = db;
        }
         
        /// <summary>
        /// 开始执行所有的job
        /// </summary>
        /// <returns></returns>
        public async Task ScheduleJobs()
        {
            var jobTypes = GetJobTypes(); // 获取所有实现了 IJob 接口的类型
            foreach (var type in jobTypes)
            {
                var jobDetail = JobBuilder.Create(type).Build();
                var trigger = TriggerBuilder.Create().WithCronSchedule(GetCronExpression(type)).Build();
                await scheduler.ScheduleJob(jobDetail, trigger);
            }
            await scheduler.Start();
        }

        private IEnumerable<Type> GetJobTypes()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(IJob).IsAssignableFrom(type));
        }


        private string GetCronExpression(Type type)
        {
            // 可以根据需要自定义获取 cron 表达式的逻辑
            return (string)type.GetField("Cron", BindingFlags.Static | BindingFlags.Public)?.GetValue(null);
        }
    }
}
