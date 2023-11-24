using Hangfire;
using TeamRotationActivity.Domain.Interfaces.Builders;
using TeamRotationActivity.Domain.Interfaces.Jobs;
using TeamRotationActivity.Domain.Interfaces.Services;

namespace TeamRotationActivity.Core.Builders;

/// <summary>
/// Создатель фоновых задач.
/// </summary>
public class JobBuilder : IJobBuilder
{
    /// <summary>
    /// Создать задачу по отправке сообщения.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="valueSecond">Количество секунд до отправки.</param>
    public void SendMessageScheduleJobBuild(string message, double valueSecond)
    {
        BackgroundJob.Schedule<IMessageSenderService>(ms => ms.SendMessage(message),
            TimeSpan.FromSeconds(valueSecond));
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
    /// Немедленное выполнение джобы.
    /// </summary>
    /// <param name="jobId">Идентификатор джобы.</param>
    public void Trigger(string jobId)
    {
        var manager = new RecurringJobManager();
        manager.Trigger(jobId);
    }
}