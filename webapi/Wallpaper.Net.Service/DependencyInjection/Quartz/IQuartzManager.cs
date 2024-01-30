using Quartz;

namespace Wallpaper.Net.Service.QuartzNET
{
    public interface IQuartzManager
    {
        // 获取Quartz调度程序实例
        Task<IScheduler> GetScheduler(CancellationToken token = default);

        // 启动Quartz调度程序
        Task Start(CancellationToken token = default);

        // 关闭Quartz调度程序
        Task Shutdown(CancellationToken token = default);

        // 检查作业是否存在
        Task<bool> CheckExists(string jobName, CancellationToken token = default);

        // 添加作业到Quartz调度程序，使用泛型类型参数
        Task AddJob<T>(JobInfo jobInfo, CancellationToken token = default)  where T : IJob;

        // 添加作业到Quartz调度程序，使用作业类型参数
        Task AddJob(Type jobType, JobInfo jobInfo, CancellationToken token = default);

        // 删除指定名称的作业
        Task<bool> DeleteJob(string jobName, CancellationToken token = default);

        // 暂停指定名称的作业
        Task PauseJob(string jobName, CancellationToken token = default);

        // 恢复暂停的作业
        Task ResumeJob(string jobName, CancellationToken token = default);
    }
}