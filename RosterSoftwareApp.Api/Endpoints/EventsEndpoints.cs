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
        groupRoute.MapGet("/", (IEventsRepository eventsRepository) =>
        eventsRepository.GetAll().Select(e => e.AsDto()));

        // Get Event by ID 
        groupRoute.MapGet("/{id}", (IEventsRepository eventsRepository, int id) =>
        {
            Event? ev = eventsRepository.GetEvent(id);
            return ev is not null ? Results.Ok(ev.AsDto()) : Results.NotFound();

        }).WithName(GetEventEndPointName); //so we can use after the create result CreatedAtRoute()

        // Create Event and received the Dtos type
        groupRoute.MapPost("/", (IEventsRepository eventsRepository, CreateEventDto evDto) =>
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
            eventsRepository.CreateEvent(ev);
            // return the latest created using the Get by ID
            return Results.CreatedAtRoute(GetEventEndPointName, new { Id = ev.Id }, ev);
        });

        // Edit Event
        groupRoute.MapPut("/{id}", (IEventsRepository eventsRepository, int id, EventDto updateEventDto) =>
        {
            Event? ev = eventsRepository.GetEvent(id);
            if (ev is null)
            {
                return Results.NotFound();
            }
            ev.Title = updateEventDto.Title;
            ev.Description = updateEventDto.Description;
            ev.EventDate = updateEventDto.EventDate;
            ev.EventTime = updateEventDto.EventTime;
            ev.Active = updateEventDto.Active;

            eventsRepository.UpdateEvent(ev);

            return Results.NoContent();
        });

        // DELETE event

        groupRoute.MapDelete("/{id}", (IEventsRepository eventsRepository, int id) =>
        {
            Event? ev = eventsRepository.GetEvent(id);
            if (ev is not null)
            {
                eventsRepository.DeleteEvent(id);
            }
            return Results.NoContent();
        });

        return groupRoute;
    }
}