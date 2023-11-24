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

        _builder.StartRecurringJob<MessageSchedulerJob>(messageRecurringJobId, "0 6 * * *");
        _builder.StartRecurringJob<RotationSchedulerJob>(rotationRecurringJobId, "0 6 * * *");

        if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 18)
        {
            _builder.Trigger(messageRecurringJobId);
            _builder.Trigger(rotationRecurringJobId);
        }
    }
}
