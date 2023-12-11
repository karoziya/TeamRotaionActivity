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
    /// Идентификатор MessageRecurringJob.
    /// </summary>
    private const string MessageRecurringJobId = "MessageRecurringJobId";

    /// <summary>
    /// Идентификатор RotationRecurringJob.
    /// </summary>
    private const string RotationRecurringJobId = "RotationRecurringJobId";


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
        _builder.StartRecurringJob<MessageSchedulerJob>(MessageRecurringJobId, CronExpression);
        _builder.StartRecurringJob<RotationSchedulerJob>(RotationRecurringJobId, CronExpression);

        _builder.Trigger(MessageRecurringJobId);
        _builder.Trigger(RotationRecurringJobId);
    }
}
