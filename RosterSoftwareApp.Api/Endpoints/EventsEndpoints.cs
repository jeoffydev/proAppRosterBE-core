using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Endpoints;

public static class EventsEndpoints
{
    const string GetEventEndPointName = "GetEventByID";

    public static RouteGroupBuilder MapEventsEndpoint(this IEndpointRouteBuilder routes)
    {
        InMemEventsRepository eventsRepository = new();

        var groupRoute = routes.MapGroup("/events")
                        .WithParameterValidation();

        // Get all Events
        groupRoute.MapGet("/", () => eventsRepository.GetAll());

        // Get Event by ID 
        groupRoute.MapGet("/{id}", (int id) =>
        {
            Event? ev = eventsRepository.GetEvent(id);
            return ev is not null ? Results.Ok(ev) : Results.NotFound();

        }).WithName(GetEventEndPointName); //so we can use after the create result CreatedAtRoute()

        // Create Event
        groupRoute.MapPost("/", (Event ev) =>
        {
            eventsRepository.CreateEvent(ev);
            // return the latest created using the Get by ID
            return Results.CreatedAtRoute(GetEventEndPointName, new { Id = ev.Id }, ev);
        });

        // Edit Event
        groupRoute.MapPut("/{id}", (int id, Event updatedEv) =>
        {
            Event? ev = eventsRepository.GetEvent(id);
            if (ev is null)
            {
                return Results.NotFound();
            }
            ev.Title = updatedEv.Title;
            ev.Description = updatedEv.Description;
            ev.EventDate = updatedEv.EventDate;
            ev.EventTime = updatedEv.EventTime;

            eventsRepository.UpdateEvent(ev);

            return Results.NoContent();
        });

        // DELETE event

        groupRoute.MapDelete("/{id}", (int id) =>
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