using TeamRotationActivity.Domain.Interfaces.Jobs;
using TeamRotationActivity.Domain.Interfaces.Services;

namespace TeamRotationActivity.Jobs.Jobs;

/// <summary>
/// Планировщик отправления сообщений по активности.
/// </summary>
public class MessageSchedulerJob : IJob<MessageSchedulerJob>
{
    private readonly ISaverService _activitySaverService;
    private readonly IActivityService _activityService;
    private readonly IJobService jobService;
    private readonly IMessageSenderService  messageService;


    /// <summary>
    /// Создать экземпляр <see cref="MessageSchedulerJob"/>
    /// </summary>
    /// <param name="activitySaverService"></param>
    /// <param name="activityService"></param>
    /// <param name="builder"></param>
    public MessageSchedulerJob(ISaverService activitySaverService, IActivityService activityService, IJobService jobService, IMessageSenderService messageService)
    {
        _activitySaverService = activitySaverService;
        _activityService = activityService;
        this.jobService = jobService;
        this.messageService = messageService;
    }

    public async Task ExecuteAsync(CancellationToken token = default)
    {
        var activities = await _activitySaverService.LoadActivitiesFromFileAsync();
        if (activities == null)
        {
            return;
        }

        foreach (var activity in activities)
        {
            //var messagejob = new MessageJob(activity.ActivityAnnouncementMessage, messageService);
            var messagejob = new MessageJob(messageService);
            // TODO: Нужно придумать как задавать расписание.
            this.jobService.StartRecurringJob(activity.Name, messagejob, "20 9 * * *");
        }
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
