using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Domain.Interfaces.Services;

/// <summary>
/// Сервис ротации.
/// </summary>
public interface IRotationService
{
    /// <summary>
    /// Определяет, нужна ли смена ведущего в активности.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    /// <returns>Нужна ли ротация.</returns>
    public bool NeedRotation(ActivityWork activityWork);
    
    /// <summary>
    /// Сменить ведущего активности.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    public void Rotate(ActivityWork activityWork);
}

