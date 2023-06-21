using RosterSoftwareApp.Api.AllDtos;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Endpoints;

public static class NotificationEndpoint
{
    const string GetNotificationEndPointName = "GetNotificationByID";

    public static RouteGroupBuilder MapNotificationsEndpoint(this IEndpointRouteBuilder routes)
    {
        var groupRoute = routes.MapGroup("/notifications")
                        .WithParameterValidation();

        /* this can be read access which admin also has */

        // Get all Notifications 
        groupRoute.MapGet("/", async (INotificationRepository notificationRepository) =>
        (await notificationRepository.GetAllNotificationsAsync()).Select(e => e.AsNotificationDto()))
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        );

        // Get Notification by ID 
        groupRoute.MapGet("/{id}", async (INotificationRepository notificationRepository, int id) =>
        {
            Notification? n = await notificationRepository.GetNotificationAsync(id);
            return n is not null ? Results.Ok(n.AsNotificationDto()) : Results.NotFound();

        }).WithName(GetNotificationEndPointName)
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        );

        // Create Notification and received the Dtos type
        groupRoute.MapPost("/", async (INotificationRepository notificationRepository, CreateNotificationDto nDto) =>
        {
            //Map the DTOs type to Notification
            Notification no = new()
            {
                Title = nDto.Title,
                Description = nDto.Description,
                Active = nDto.Active
            };
            await notificationRepository.CreateNotificationAsync(no);
            // return the latest created using the Get by ID
            return Results.CreatedAtRoute(GetNotificationEndPointName, new { Id = no.Id }, no);
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );


        // Edit Notification
        groupRoute.MapPut("/{id}", async (INotificationRepository notificationRepository, int id, UpdateNotificationDto updateNotificationDto) =>
        {
            Notification? no = await notificationRepository.GetNotificationAsync(id);
            if (no is null)
            {
                return Results.NotFound();
            }
            no.Title = updateNotificationDto.Title;
            no.Description = updateNotificationDto.Description;
            no.Active = updateNotificationDto.Active;

            await notificationRepository.UpdateNotificationAsync(no);

            return Results.NoContent();
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        // DELETE notification

        groupRoute.MapDelete("/{id}", async (INotificationRepository notificationRepository, int id) =>
        {
            Notification? no = await notificationRepository.GetNotificationAsync(id);
            if (no is not null)
            {
                await notificationRepository.DeleteNotificationAsync(id);
            }
            return Results.NoContent();
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );



        return groupRoute;
    }

}