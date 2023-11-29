using TeamRotationActivity.Domain.Enums;
using TeamRotationActivity.Domain.Interfaces.Builders;
using TeamRotationActivity.Domain.Interfaces.Jobs;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Jobs.Jobs;

/// <summary>
/// Планировщик отправления сообщений по активности.
/// </summary>
public class MessageSchedulerJob : IJob<MessageSchedulerJob>
{
    private readonly IReadWriteService _activitySaverService;
    private readonly IActivityService _activityService;
    private readonly IJobBuilder _builder;

    /// <summary>
    /// Создать экземпляр <see cref="MessageSchedulerJob"/>
    /// </summary>
    /// <param name="activitySaverService"></param>
    /// <param name="activityService"></param>
    /// <param name="builder"></param>
    public MessageSchedulerJob(IReadWriteService activitySaverService, IActivityService activityService, IJobBuilder builder)
    {
        _activitySaverService = activitySaverService;
        _activityService = activityService;
        _builder = builder;
    }

    public async Task ExecuteAsync(string jobId, CancellationToken token = default)
    {
        var activities = await _activitySaverService.LoadActivitiesFromFileAsync();
        if (activities == null)
        {
            return;
        }

        var updateActivities = new List<ActivityWork>();

        foreach (var activity in activities)
        {
            var actualizeActivity = ActualizeActivityDate(activity);
            var secondToJob = GetSecondsToJob(actualizeActivity.ActivityDate);
            if (actualizeActivity.ActivityDate.Date == DateTime.Now.Date && secondToJob >= 0)
            {
                var memberActivity = actualizeActivity.Members?.FirstOrDefault();
                var messageActivity = actualizeActivity.ActivityAnnouncementMessage + memberActivity?.LastName + " " + memberActivity?.Name;
                _builder.ScheduleJobBuild<IMessageSenderService>(ms => ms.SendMessage(messageActivity), secondToJob);
            }

            var activityUpdate = _activityService.CalculateActivityDate(actualizeActivity);
            updateActivities.Add(activityUpdate);
        }

        await _activitySaverService.SaveActivitiesAsync(updateActivities);
    }

    /// <summary>
    /// Посчитать разницу между нужным временем и текущим.
    /// </summary>
    /// <param name="time">Время.</param>
    /// <returns>Количество секунд.</returns>
    private double GetSecondsToJob(DateTime time)
    {
        var timeSpan = time - DateTime.Now;
        var result = timeSpan.TotalSeconds;
        return result;
    }

    /// <summary>
    /// Актуализовать дату проведения активности.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    /// <returns></returns>
    private ActivityWork ActualizeActivityDate(ActivityWork activityWork)
    {
        if (activityWork.ActivityDate.Date < DateTime.Now.Date && activityWork.ActivityPeriod == ActivityPeriod.EveryDay)
        {
            activityWork.ActivityDate =
                activityWork.ActivityDate.AddDays(DateTime.Now.Day - activityWork.ActivityDate.Day);
        }

        return activityWork;
    }
}
