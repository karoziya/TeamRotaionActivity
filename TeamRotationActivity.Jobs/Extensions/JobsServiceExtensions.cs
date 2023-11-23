using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TeamRotationActivity.Jobs.Jobs;
using TeamRotationActivity.Jobs.Jobs.Interfaces;

namespace TeamRotationActivity.Jobs.Extensions;

public static class JobsServiceExtensions
{
    /// <summary>
    /// Добавление Hangfire сервиса.
    /// </summary>
    /// <param name="serviceCollection">Токен.</param>
    public static void AddCustomHangfire(this IServiceCollection serviceCollection)
    {
        AddHangfireConfiguration();
        AddHangfireServices(serviceCollection);
        AddJobsServices(serviceCollection);
    }

    /// <summary>
    /// Добавление к Hangfire дашборда для просмотра статусов джоб.
    /// </summary>
    /// <param name="app">Билдер.</param>
    public static void UseCustomHangfireDashboard(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard();
    }

    private static void AddJobsServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IJob<MessageSchedulerJob>, MessageSchedulerJob>();
        serviceCollection.AddScoped<IMessageJob, MessageJob>();
    }

    private static void AddHangfireConfiguration()
    {
        GlobalConfiguration.Configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseColouredConsoleLogProvider()
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSerilogLogProvider();
    }

    private static void AddHangfireServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHangfire(config =>
        {
            config.UseInMemoryStorage();
        });

        serviceCollection.AddHangfireServer();
    }
}

