using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamRotationActivity.Database.Data;
using TeamRotationActivity.Database.Repository;
using TeamRotationActivity.Domain.Interfaces.Repository;

namespace TeamRotationActivity.Database.Extensions;

/// <summary>
/// Класс регистрации зависимостей Database слоя.
/// </summary>
public static class DatabaseRegistrationExtensions
{
    /// <summary>
    /// Зарегистрировать сервисы Database слоя.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbServices(this IServiceCollection services,
        string connectionString, Action<DbContextOptionsBuilder>? configureAction = null)
    {
        services.AddDbContext<TeamRotationActivityDbContext>(builder =>
        {
            builder.UseNpgsql(connectionString);
            builder.UseSnakeCaseNamingConvention();
            configureAction?.Invoke(builder);
        });

        services.AddScoped(typeof(IRepository<>), (typeof(BaseRepository<>)));

        return services;
    }
}

