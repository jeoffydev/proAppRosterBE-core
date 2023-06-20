using RosterSoftwareApp.Api.Data;

namespace RosterSoftwareApp.Api.Endpoints;

public static class NoResultEndpoint
{
    public static RouteGroupBuilder MapNoResultEndpoint(this IEndpointRouteBuilder routes)
    {
        var groupRoute = routes.MapGroup("/no-result")
                        .WithParameterValidation();


        // Get null output 
        groupRoute.MapGet("/", () =>
        {
            return Results.Ok(null);

        });


        return groupRoute;
    }
}