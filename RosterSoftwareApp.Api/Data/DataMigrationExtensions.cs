
using Microsoft.EntityFrameworkCore;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Data;

public static class DataMigrationExtensions
{
    public static async Task InitializeDbMigrationAsync(this IServiceProvider serviceProvider)
    {
        // To automatically migrate the EF changes every dotnet run
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RosterStoreContext>();
        await dbContext.Database.MigrateAsync();

        var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
                                    .CreateLogger("Database Migration");
        logger.LogInformation(5, "Database is ready!");
    }


    public static IServiceCollection JsonHandler(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddControllers().AddNewtonsoftJson(x =>
            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        return serviceProvider;
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionDBString = configuration.GetConnectionString("RosterAppContext");
        // Add the DB connectionString
        // previous code for not using the DBcontext ->  .AddSingleton<IEventsRepository, InMemEventsRepository>();
        services.AddSqlServer<RosterStoreContext>(connectionDBString)
        .AddScoped<IEventsRepository, EntityFrameworkEventRepository>()
        .AddScoped<ISongRepository, EntityFrameworkSongRepository>()
        .AddScoped<IEventSongRepository, EntityFrameworkEventSongRepository>()
        .AddScoped<IInstrumentRepository, EntityFrameworkInstrumentRepository>()
        .AddScoped<INotificationRepository, EntityFrameworkNotificationRepository>()
        .AddScoped<IMemberInstrumentRepository, EntityFrameworkMemberInstrumentRepository>();

        return services;
    }
}