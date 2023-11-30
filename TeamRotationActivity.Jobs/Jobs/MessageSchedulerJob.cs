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
    private readonly IReadWriteService _readWriteService;
    private readonly IActivityService _activityService;
    private readonly IJobBuilder _builder;

    /// <summary>
    /// Создать экземпляр <see cref="MessageSchedulerJob"/>
    /// </summary>
    /// <param name="readWriteService"></param>
    /// <param name="activityService"></param>
    /// <param name="builder"></param>
    public MessageSchedulerJob(IReadWriteService readWriteService, IActivityService activityService, IJobBuilder builder)
    {
        _readWriteService = readWriteService;
        _activityService = activityService;
        _builder = builder;
    }

    public async Task ExecuteAsync(CancellationToken token = default)
    {
        var activities = await _readWriteService.LoadActivitiesFromFileAsync();
        if (activities == null)
        {
            return;
        }

        var updateActivities = new List<ActivityWork>();

        foreach (var activity in activities)
        {
            var actualizeActivity = ActualizeActivityDate(activity);
            CreateJobIfDateHasArrived(actualizeActivity);
            var activityUpdate = _activityService.CalculateActivityDate(actualizeActivity);
            updateActivities.Add(activityUpdate);
        }

        await _readWriteService.SaveActivitiesAsync(updateActivities);
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

    /// <summary>
    /// Создать джобу для отправки сообщения если подошла дата.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    private void CreateJobIfDateHasArrived(ActivityWork activityWork)
    {
        var secondToJob = GetSecondsToJob(activityWork.ActivityDate);
        if (activityWork.ActivityDate.Date == DateTime.Now.Date && secondToJob >= 0)
        {
            _builder.ScheduleJobBuild<IMessageSenderService>(ms => ms.SendMessage(CreateMessage(activityWork)), activityWork.Name, secondToJob);
        }
    }

    /// <summary>
    /// Создать сообщение для отправки.
    /// </summary>
    /// <param name="activityWork">Активность.</param>
    /// <returns></returns>
    private string CreateMessage(ActivityWork activityWork)
    {
        var memberActivity = activityWork.Members?.FirstOrDefault();
        return activityWork.ActivityAnnouncementMessage + memberActivity?.LastName + " " + memberActivity?.Name;
    }
}
