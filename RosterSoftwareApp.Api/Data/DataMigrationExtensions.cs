using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Data;

public static class DataMigrationExtensions
{
    public static void InitializeDbMigration(this IServiceProvider serviceProvider)
    {
        // To automatically migrate the EF changes every dotnet run
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RosterStoreContext>();
        dbContext.Database.Migrate();
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
        .AddScoped<IEventsRepository, EntityFrameworkEventRepository>();

        return services;
    }
}