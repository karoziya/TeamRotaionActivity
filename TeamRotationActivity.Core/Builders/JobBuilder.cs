using Hangfire;
using System.Linq.Expressions;
using TeamRotationActivity.Domain.Interfaces.Builders;
using TeamRotationActivity.Domain.Interfaces.Jobs;

namespace TeamRotationActivity.Core.Builders;

/// <summary>
/// Создатель фоновых задач.
/// </summary>
public class JobBuilder : IJobBuilder
{
    /// <summary>
    /// Создать запланированную задачу.
    /// </summary>
    /// <typeparam name="T">Тип запускаемой задачи.</typeparam>
    /// <param name="action">Запускаемый метод.</param>
    /// <param name="valueSecond">Количество секунд до запуска задачи.</param>
    public void ScheduleJobBuild<T>(Expression<Action<T>> action, double valueSecond)
    {
        BackgroundJob.Schedule(action, TimeSpan.FromSeconds(valueSecond));
    }

    /// <summary>
    /// Создать повторяющуюся задачу по cron.
    /// </summary>
    /// <typeparam name="T">Класс джобы.</typeparam>
    /// <param name="jobId">Идентификатор джобы.</param>
    /// <param name="cron">Cron выражение.</param>
    void IJobBuilder.StartRecurringJob<T>(string jobId, string cron)
    {
        var manager = new RecurringJobManager();
        manager.AddOrUpdate<IJob<T>>(jobId, pr => pr.ExecuteAsync(jobId, CancellationToken.None), cron);
    }

    /// <summary>
    /// Немедленное выполнение повторяющейся задачи.
    /// </summary>
    /// <param name="jobId">Идентификатор джобы.</param>
    public void Trigger(string jobId)
    {
        var manager = new RecurringJobManager();
        manager.Trigger(jobId);
    }
}