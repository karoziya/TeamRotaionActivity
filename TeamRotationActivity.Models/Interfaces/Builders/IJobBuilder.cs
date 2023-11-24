using TeamRotationActivity.Domain.Interfaces.Jobs;

namespace TeamRotationActivity.Domain.Interfaces.Builders;

/// <summary>
/// Создатель фоновых задач.
/// </summary>
public interface IJobBuilder
{
    /// <summary>
    /// Создать задачу по отправке сообщения.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="valueSecond">Количество секунд до отправки.</param>
    void SendMessageScheduleJobBuild(string message, double valueSecond);

    /// <summary>
    /// Создать повторяющуюся задачу по cron.
    /// </summary>
    /// <typeparam name="T">Класс джобы.</typeparam>
    /// <param name="jobId">Идентификатор джобы.</param>
    /// <param name="cron">Cron выражение.</param>
    void StartRecurringJob<T>(string jobId, string cron) where T : class, IJob<T>;

    /// <summary>
    /// Немедленное выполнение джобы.
    /// </summary>
    /// <param name="jobId">Идентификатор джобы.</param>
    void Trigger(string jobId);
}

