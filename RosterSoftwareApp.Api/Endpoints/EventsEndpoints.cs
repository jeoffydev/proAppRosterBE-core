using RosterSoftwareApp.Api.Dtos;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Endpoints;

public static class EventsEndpoints
{
    const string GetEventEndPointName = "GetEventByID";

    public static RouteGroupBuilder MapEventsEndpoint(this IEndpointRouteBuilder routes)
    {

        var groupRoute = routes.MapGroup("/events")
                        .WithParameterValidation();

        // Get all Events
        groupRoute.MapGet("/", async (IEventsRepository eventsRepository) =>
        (await eventsRepository.GetAllAsync()).Select(e => e.AsDto()));

        // Get Event by ID 
        groupRoute.MapGet("/{id}", async (IEventsRepository eventsRepository, int id) =>
        {
            Event? ev = await eventsRepository.GetEventAsync(id);
            return ev is not null ? Results.Ok(ev.AsDto()) : Results.NotFound();

        }).WithName(GetEventEndPointName); //so we can use after the create result CreatedAtRoute()

        // Create Event and received the Dtos type
        groupRoute.MapPost("/", async (IEventsRepository eventsRepository, CreateEventDto evDto) =>
        {
            //Map the DTOs type to Event type
            Event ev = new()
            {
                Title = evDto.Title,
                EventDate = evDto.EventDate,
                EventTime = evDto.EventTime,
                Description = evDto.Description,
                Active = evDto.Active
            };
            await eventsRepository.CreateEventAsync(ev);
            // return the latest created using the Get by ID
            return Results.CreatedAtRoute(GetEventEndPointName, new { Id = ev.Id }, ev);
        });

        // Edit Event
        groupRoute.MapPut("/{id}", async (IEventsRepository eventsRepository, int id, UpdateEventDto updateEventDto) =>
        {
            Event? ev = await eventsRepository.GetEventAsync(id);
            if (ev is null)
            {
                return Results.NotFound();
            }
            ev.Title = updateEventDto.Title;
            ev.Description = updateEventDto.Description;
            ev.EventDate = updateEventDto.EventDate;
            ev.EventTime = updateEventDto.EventTime;
            ev.Active = updateEventDto.Active;

            await eventsRepository.UpdateEventAsync(ev);

            return Results.NoContent();
        });

        // DELETE event

        groupRoute.MapDelete("/{id}", async (IEventsRepository eventsRepository, int id) =>
        {
            Event? ev = await eventsRepository.GetEventAsync(id);
            if (ev is not null)
            {
                await eventsRepository.DeleteEventAsync(id);
            }
            return Results.NoContent();
        });

        return groupRoute;
    }
}