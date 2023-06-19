
namespace RosterSoftwareApp.Api.Cors;

public static class CorsExtensions
{
    private static string allowedOriginSetting = "AllowedOrigin";
    public static IServiceCollection AddRosterCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(
            options =>
            {
                options.AddDefaultPolicy(corsBuilder =>
                {
                    var allowedOrigin = configuration[allowedOriginSetting]
                        ?? throw new InvalidOperationException("Allowed origin is not set!");
                    corsBuilder.WithOrigins(allowedOrigin)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            }
        );


        return services;
    }
}