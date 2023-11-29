using System.Linq.Expressions;
using TeamRotationActivity.Domain.Interfaces.Jobs;

namespace TeamRotationActivity.Domain.Interfaces.Builders;

/// <summary>
/// Создатель фоновых задач.
/// </summary>
public interface IJobBuilder
{
    /// <summary>
    /// Создать запланированную задачу.
    /// </summary>
    /// <typeparam name="T">Тип запускаемой задачи.</typeparam>
    /// <param name="action">Запускаемый метод.</param>
    /// <param name="valueSecond">Количество секунд до запуска задачи.</param>
    void ScheduleJobBuild<T>(Expression<Action<T>> action, double valueSecond);

    /// <summary>
    /// Создать повторяющуюся задачу по cron.
    /// </summary>
    /// <typeparam name="T">Класс джобы.</typeparam>
    /// <param name="jobId">Идентификатор джобы.</param>
    /// <param name="cron">Cron выражение.</param>
    void StartRecurringJob<T>(string jobId, string cron) where T : class, IJob<T>;

    /// <summary>
    /// Немедленное выполнение повторяющейся задачи.
    /// </summary>
    /// <param name="jobId">Идентификатор джобы.</param>
    void Trigger(string jobId);
}

