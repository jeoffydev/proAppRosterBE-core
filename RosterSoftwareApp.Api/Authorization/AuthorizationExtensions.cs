using RosterSoftwareApp.Api.Data;

namespace RosterSoftwareApp.Api.Authorization;

public static class AuthorizationExtensions
{

    public static IServiceCollection AuthorizeRoleAdminExtensions(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                PoliciesClaim.ReadAccess, builder =>
                builder.RequireClaim("scope", "roster:read")
                .RequireRole(UserRolePolicy.AdminRole)
            );
            options.AddPolicy(
                PoliciesClaim.WriteAccess, builder =>
                builder.RequireClaim("scope", "roster:write")
                .RequireRole(UserRolePolicy.AdminRole)
            );
        });

        return services;
    }
}