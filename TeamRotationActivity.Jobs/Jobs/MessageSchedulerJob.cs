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
    private readonly ISaverService _activitySaverService;
    private readonly IActivityService _activityService;
    private readonly IJobBuilder _builder;

    /// <summary>
    /// Создать экземпляр <see cref="MessageSchedulerJob"/>
    /// </summary>
    /// <param name="activitySaverService"></param>
    /// <param name="activityService"></param>
    /// <param name="builder"></param>
    public MessageSchedulerJob(ISaverService activitySaverService, IActivityService activityService, IJobBuilder builder)
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

        var activitiesUpdate = new List<ActivityWork>();

        foreach (var activity in activities)
        {
            if (activity.ActivityDate.Date == DateTime.Now.Date)
            {
                _builder.SendMessageScheduleJobBuild(activity.ActivityAnnouncementMessage, GetSecondsToJob(activity.ActivityDate));

                var activityUpdate = _activityService.CalculateActivityDate(activity);

                activitiesUpdate.Add(activityUpdate);
            }
        }

        await _activitySaverService.SaveActivitiesAsync(activitiesUpdate);
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
}
