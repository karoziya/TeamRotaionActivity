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
}

