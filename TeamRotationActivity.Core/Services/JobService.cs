using Hangfire;
using TeamRotationActivity.Domain.Interfaces.Jobs;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Jobs.Jobs;

namespace TeamRotationActivity.Core.Services;

/// <summary>
/// Сервис регистраций и запуска базовых фоновых служб.
/// </summary>
public class JobService : IJobService
{
    private const double immediatePeriod = 0;

    /// <summary>
    /// Конструктор <see cref="JobService"/>
    /// </summary>
    /// <param name="builder"></param>
    public JobService()
    {
    }

    /// <summary>
    /// Запуск базовых фоновых служб.
    /// </summary>
    public void StartJobs()
    {
        // new MessageSchedulerJob().ExecuteAsync(
        //StartRecurringJob<MessageSchedulerJob>("0 0 * * *");
        StartBackgroundJob<MessageSchedulerJob>(immediatePeriod);
        StartRecurringJob<MessageSchedulerJob>("* * * * *");
    }

    public void StartBackgroundJob<T>(double periodInSecond) where T : IJob<T>
    {
        BackgroundJob.Schedule<T>(job => job.ExecuteAsync(CancellationToken.None),
            TimeSpan.FromSeconds(periodInSecond));
    }

    public void StartBackgroundJob<T>(IJob<T> job, double periodInSecond)
    {
        BackgroundJob.Schedule(() => job.ExecuteAsync(CancellationToken.None), TimeSpan.FromSeconds(periodInSecond));
    }

    public static void StartRecurringJob<T>(string cron)
    {
        var manager = new RecurringJobManager();
        manager.AddOrUpdate<IJob<T>>(Guid.NewGuid().ToString(), pr => pr.ExecuteAsync(CancellationToken.None), cron);
    }
    public void StartRecurringJob<T>(string jobId, IJob<T> job, string cron)
    {
        var manager = new RecurringJobManager();
        manager.AddOrUpdate(jobId, () => job.ExecuteAsync(CancellationToken.None), cron);
    }

}
