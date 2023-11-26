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
        StartRecurringJob<MessageSchedulerJob>("* * * * *");
    }

    public static void StartBackgroundJob<T>(double periodInSecond) where T : IJob<T>
    {
        BackgroundJob.Schedule<T>(job => job.ExecuteAsync(CancellationToken.None),
            TimeSpan.FromSeconds(periodInSecond));
    }

    public static void StartRecurringJob<T>( string cron)
    {
        var manager = new RecurringJobManager();
        manager.AddOrUpdate<IJob<T>>(Guid.NewGuid().ToString(), pr => pr.ExecuteAsync(CancellationToken.None), cron);
    }

}
