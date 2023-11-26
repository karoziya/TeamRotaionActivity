using TeamRotationActivity.Domain.Interfaces.Jobs;

namespace TeamRotationActivity.Domain.Interfaces.Services;

/// <summary>
/// Регистрация и запуск базовых фоновых служб.
/// </summary>
public interface IJobService
{
    /// <summary>
    /// Запуск базовых фоновых служб.
    /// </summary>
    void StartJobs();

    void StartBackgroundJob<T>(double periodInSecond) where T : IJob<T>;

    void StartBackgroundJob<T>(IJob<T> job, double periodInSecond);

    void StartRecurringJob<T>(string jobId, IJob<T> job, string cron);
}

