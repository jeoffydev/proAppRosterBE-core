
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
        .AddScoped<ISongRepository, EntityFrameworkSongRepository>();

        return services;
    }
}