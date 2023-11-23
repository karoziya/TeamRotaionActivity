using Hangfire;
using TeamRotationActivity.Domain.Enums;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Domain.Models;
using TeamRotationActivity.Jobs.Jobs.Interfaces;

namespace TeamRotationActivity.Jobs.Jobs;

public class MessageSchedulerJob : IJob<MessageSchedulerJob>
{
    private readonly IActivitySaverService _activitySaverService;

    public MessageSchedulerJob(IActivitySaverService activitySaverService)
    {
        _activitySaverService = activitySaverService;
    }

    public async Task ExecuteAsync(string jobId, CancellationToken token = default)
    {
        var activities = await _activitySaverService.LoadActivitiesFromFileAsync();

        var activitiesUpdate = new List<ActivityWork>();

        if (activities == null)
        {
            return;
        }

        foreach (var activity in activities)
        {
            if (activity.ActivityDate.Date == DateTime.Now.Date)
            {
                var activityJobId = Guid.NewGuid().ToString();

                BackgroundJob.Schedule<IMessageJob>(pr => 
                    pr.ExecuteAsync(activityJobId + activity.Name, activity.ActivityAnnouncementMessage),
                    TimeSpan.FromSeconds(GetSecondsToJob(activity.ActivityDate)));

                if (activity.ActivityPeriod == ActivityPeriod.EveryDay)
                {
                    activity.ActivityDate = activity.ActivityDate.AddDays(1);

                    activitiesUpdate.Add(activity);
                }
            }
        }

        await _activitySaverService.SaveActivitiesAsync(activitiesUpdate);
    }

    private double GetSecondsToJob(DateTime time)
    {
        var timeSpan = time - DateTime.Now;
        var result = timeSpan.TotalSeconds;
        return result;
    }
}
