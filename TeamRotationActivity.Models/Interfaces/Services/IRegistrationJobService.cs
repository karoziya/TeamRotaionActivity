namespace TeamRotationActivity.Domain.Interfaces.Services;

/// <summary>
/// Регистрация и запуск фоновых служб.
/// </summary>
public interface IRegistrationJobService
{
    /// <summary>
    /// Запуск фоновых служб.
    /// </summary>
    void StartJobs();
}

