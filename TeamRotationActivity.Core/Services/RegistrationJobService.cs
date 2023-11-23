using Hangfire;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Jobs.Jobs;
using TeamRotationActivity.Jobs.Jobs.Interfaces;

namespace TeamRotationActivity.Core.Services;

public class RegistrationJobService : IRegistrationJobService
{
    public void StartJobs()
    {
        StartMessageSchedulerJob();
    }

    private void StartMessageSchedulerJob()
    {
        var manager = new RecurringJobManager();
        var jobId = Guid.NewGuid().ToString();

        manager.AddOrUpdate<IJob<MessageSchedulerJob>>(jobId, pr => pr.ExecuteAsync(jobId, CancellationToken.None), "0 6 * * 1-5");
    }
}
