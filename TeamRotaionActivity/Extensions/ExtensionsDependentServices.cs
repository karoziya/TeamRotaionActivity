using TeamRotationActivity.Core;
using TeamRotationActivity.Database.Extensions;

namespace TeamRotationActivity.Extensions;

/// <summary>
/// Расширение регистрации сервисов.
/// </summary>
public static class ExtensionsDependentServices
{
    /// <summary>
    /// Регистрация сервисов.
    /// </summary>
    /// <param name="builder"><see cref="WebApplicationBuilder"/>.</param>
    /// <returns></returns>
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.ConfigureServices();
        builder.Services.RegisterDataLayer(builder.Configuration);

        return builder;
    }

    /// <summary>
    /// Метод регистрации зависимостей Database слоя.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <exception cref="ArgumentNullException"></exception>
    private static void RegisterDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionDb");

        if (connectionString != null)
        {
            services.AddDbServices(connectionString);

            Console.WriteLine(connectionString);
        }
        else
        {
            throw new ArgumentNullException("ConnectionString is null");
        }
    }
}

