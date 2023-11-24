namespace TeamRotationActivity.Domain.Interfaces.Services;

/// <summary>
/// Регистрация и запуск базовых фоновых служб.
/// </summary>
public interface IRegistrationJobService
{
    /// <summary>
    /// Запуск базовых фоновых служб.
    /// </summary>
    void StartJobs();
}

