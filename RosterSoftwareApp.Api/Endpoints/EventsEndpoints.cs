using RosterSoftwareApp.Api.Dtos;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;
using RosterSoftwareApp.Api.ViewModels;
using RosterSoftwareApp.Api.Data;
using System.Diagnostics;

namespace RosterSoftwareApp.Api.Endpoints;

public static class EventsEndpoints
{
    const string GetEventEndPointName = "GetEventByID";

    public static RouteGroupBuilder MapEventsEndpoint(this IEndpointRouteBuilder routes)
    {

        var groupRoute = routes.MapGroup("/events")
                        .WithParameterValidation();

        // Get all Events
        groupRoute.MapGet("/", async (IEventsRepository eventsRepository, ILoggerFactory loggerFactory) =>
        {
            return Results.Ok((await eventsRepository.GetAllAsync()).Select(e => e.AsDto()));

        }).RequireAuthorization(PoliciesClaim.WriteAccess);




        // Get Event by ID 
        groupRoute.MapGet("/{id}", async (IEventsRepository eventsRepository, IEventSongRepository eventSongRepository, IMemberEventRepository memberEventRepository, int id) =>
        {
            Event? ev = await eventsRepository.GetEventAsync(id);
            var evs = (List<EventSong>)await eventSongRepository.GetEventSongByEventIdAsync(id);
            var mes = (List<MemberEvent>)await memberEventRepository.GetMemberEventByEventIdAsync(id);

            if (ev is not null)
            {
                var EventAndSongs = new EventViewModel
                {
                    Event = ev,
                    EventSongs = evs,
                    MemberEvents = mes
                };
                return Results.Ok(EventAndSongs);
            }
            return Results.NotFound();

        }).WithName(GetEventEndPointName)
        .RequireAuthorization(PoliciesClaim.ReadAccess); //so we can use after the create result CreatedAtRoute()

        // Create Event and received the Dtos type
        groupRoute.MapPost("/", async (
            IEventsRepository eventsRepository,
            CreateEventDto evDto) =>
        {
            //Map the DTOs type to Event type
            Event ev = new()
            {
                Title = evDto.Title,
                EventDate = evDto.EventDate,
                EventTime = evDto.EventTime,
                Description = evDto.Description,
                Active = evDto.Active,
                EventSongs = new() { },
                MemberEvents = new() { }
            };

            await eventsRepository.CreateEventAsync(ev);

            // return the latest created using the Get by ID
            return Results.CreatedAtRoute(GetEventEndPointName, new { Id = ev.Id }, ev);
        }).RequireAuthorization(PoliciesClaim.WriteAccess);

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
        }).RequireAuthorization(PoliciesClaim.WriteAccess);

        // DELETE event

        groupRoute.MapDelete("/{id}", async (IEventsRepository eventsRepository, int id) =>
        {
            Event? ev = await eventsRepository.GetEventAsync(id);
            if (ev is not null)
            {
                await eventsRepository.DeleteEventAsync(id);
            }
            return Results.NoContent();
        }).RequireAuthorization(PoliciesClaim.WriteAccess);




        return groupRoute;
    }
}