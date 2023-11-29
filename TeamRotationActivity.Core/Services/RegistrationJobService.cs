using TeamRotationActivity.Domain.Interfaces.Builders;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Jobs.Jobs;

namespace TeamRotationActivity.Core.Services;

/// <summary>
/// Сервис регистраций и запуска базовых фоновых служб.
/// </summary>
public class RegistrationJobService : IRegistrationJobService
{
    private readonly IJobBuilder _builder;

    /// <summary>
    /// Cron выражение.
    /// </summary>
    private const string CronExpression = "0 0 * * *";

    /// <summary>
    /// Конструктор <see cref="RegistrationJobService"/>
    /// </summary>
    /// <param name="builder"></param>
    public RegistrationJobService(IJobBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Запуск базовых фоновых служб.
    /// </summary>
    public void StartJobs()
    {
        var messageRecurringJobId = Guid.NewGuid().ToString();
        var rotationRecurringJobId = Guid.NewGuid().ToString();

        _builder.StartRecurringJob<MessageSchedulerJob>(messageRecurringJobId, CronExpression);
        _builder.StartRecurringJob<RotationSchedulerJob>(rotationRecurringJobId, CronExpression);

        _builder.Trigger(messageRecurringJobId);
        _builder.Trigger(rotationRecurringJobId);
    }
}
