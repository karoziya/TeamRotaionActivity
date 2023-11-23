using Microsoft.Extensions.DependencyInjection;
using TeamRotationActivity.Core.Services;
using TeamRotationActivity.Domain.Interfaces.Services;
using TeamRotationActivity.Jobs.Extensions;

namespace TeamRotationActivity.Core;

/// <summary>
/// Класс расширения. Билдер зависимостей.
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Метод расширения конфигурации сервисов.
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IActivityService, ActivityService>();
        serviceCollection.AddScoped<IRotationService, RotationService>();
        serviceCollection.AddScoped<IActivitySaverService, ActivitySaverService>();
        serviceCollection.AddScoped<IMessageSenderService, MattermostService>();

        serviceCollection.AddCustomHangfire();

        serviceCollection.AddSingleton<IRegistrationJobService, RegistrationJobService>();

    }
}

