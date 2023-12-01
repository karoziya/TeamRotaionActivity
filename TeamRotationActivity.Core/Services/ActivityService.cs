using TeamRotationActivity.Domain.Enums;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Core.Services;

/// <summary>
/// Сервис активности.
/// </summary>
public class ActivityService : IActivityService
{
    /// <summary>
    /// Пересчитать дату проведения на основе периодичности.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    /// <returns>Обновленная активность.</returns>
    public ActivityWork CalculateActivityDate(ActivityWork activityWork)
    {
        if (activityWork.ActivityDate.Date > DateTime.Now.Date)
        {
            return activityWork;
        }

        switch (activityWork.ActivityPeriod)
        {
            case ActivityPeriod.EveryDay:
                activityWork.ActivityDate = activityWork.ActivityDate.AddDays(1);
                break;

            case ActivityPeriod.EveryTwoDays:
                activityWork.ActivityDate = activityWork.ActivityDate.AddDays(2);
                break;

            case ActivityPeriod.EveryWeek:
                activityWork.ActivityDate = activityWork.ActivityDate.AddDays(7);
                break;

            case ActivityPeriod.EveryTwoWeeks:
                activityWork.ActivityDate = activityWork.ActivityDate.AddDays(14);
                break;

            default:
                activityWork.ActivityDate = activityWork.ActivityDate.AddMonths(1);
                break;
        }

        return activityWork;
    }

    /// <summary>
    /// Создать активность.
    /// </summary>
    /// <returns>Созданная активность.</returns>
    public ActivityWork CreateActivities()
    {
        var activities = new ActivityWork
        {
            Id = Guid.NewGuid(),
            Name = "Daily Meeting",
            Description = "Дейли митинг ежедневный",
            RotationPeriod = RotationPeriod.Weekly,
            Members = new List<Member>
            {
                new() { Id = Guid.NewGuid(), Name = "Максим", LastName = "Минаев" },
                new() { Id = Guid.NewGuid(), Name = "Сергей", LastName = "Камышев" },
                new() { Id = Guid.NewGuid(), Name = "Сергей", LastName = "Полянских" }
            },
            ActivityPeriod = ActivityPeriod.EveryDay,
            ActivityAnnouncementMessage = "@here Митинг в 9:20  :eyes: \r\nhttps://directum.ktalk.ru/redteam\r\n Проводит ",
            NextRotation = DateTime.Now,
            ActivityDate = new DateTime(2023, 11, 23, 22, 28, 00),
            LastRotation = DateTime.Now
        };

        return activities;
    }

    /// <summary>
    /// Актуализовать дату проведения активности.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    public ActivityWork ActualizeActivityDate(ActivityWork activityWork)
    {
        if (activityWork.ActivityDate.Date < DateTime.Now.Date && activityWork.ActivityPeriod == ActivityPeriod.EveryDay)
        {
            activityWork.ActivityDate =
                activityWork.ActivityDate.AddDays(DateTime.Now.Day - activityWork.ActivityDate.Day);
        }

        return activityWork;
    }
}

