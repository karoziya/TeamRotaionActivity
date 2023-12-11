using TeamRotationActivity.Domain.Interfaces.Jobs;

namespace TeamRotationActivity.Jobs.Jobs;

/// <summary>
/// Планировщик ротации участников команды.
/// </summary>
public class RotationSchedulerJob : IJob<RotationSchedulerJob>
{
    public Task ExecuteAsync(CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
}

