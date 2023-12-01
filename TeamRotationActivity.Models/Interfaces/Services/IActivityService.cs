using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Domain.Interfaces.Services;

/// <summary>
/// Сервис активности.
/// </summary>
public interface IActivityService
{
    /// <summary>
    /// Пересчитать дату проведения на основе периодичности.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    /// <returns>Обновленная активность.</returns>
    ActivityWork CalculateActivityDate(ActivityWork activityWork);

    /// <summary>
    /// Создать активность.
    /// </summary>
    /// <returns>Созданная активность.</returns>
    ActivityWork CreateActivities();

    /// <summary>
    /// Актуализовать дату проведения активности.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    ActivityWork ActualizeActivityDate(ActivityWork activityWork);
}

