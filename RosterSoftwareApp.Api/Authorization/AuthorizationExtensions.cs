using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RosterSoftwareApp.Api.Data;

namespace RosterSoftwareApp.Api.Authorization;

public static class AuthorizationExtensions
{

    public static IServiceCollection AuthorizeRoleAdminExtensions(this IServiceCollection services)
    {
        services.AddScoped<IClaimsTransformation, ScopeTransformation>()
        .AddAuthorization(options =>
        {
            options.AddPolicy(
                PoliciesClaim.ReadAccess, builder =>
                builder.RequireClaim("scope", "roster:read")
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, "Auth0")
            );
            options.AddPolicy(
                PoliciesClaim.WriteAccess, builder =>
                builder.RequireClaim("scope", "roster:write")
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, "Auth0")
            );
        });

        return services;
    }
}