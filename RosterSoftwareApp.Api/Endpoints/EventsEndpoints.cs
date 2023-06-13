using RosterSoftwareApp.Api.Dtos;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;
using RosterSoftwareApp.Api.ViewModels;

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
        (
            await eventsRepository.GetAllAsync()).Select(e => e.AsDto())
        );



        // Get Event by ID 
        groupRoute.MapGet("/{id}", async (IEventsRepository eventsRepository, IEventSongRepository eventSongRepository, int id) =>
        {
            Event? ev = await eventsRepository.GetEventAsync(id);
            var evs = (List<EventSong>)await eventSongRepository.GetEventSongByEventIdAsync(id);

            if (ev is not null)
            {
                var EventAndSongs = new EventViewModel
                {
                    Event = ev,
                    EventSongs = evs
                };
                return Results.Ok(EventAndSongs);
            }
            return Results.NotFound();

        }).WithName(GetEventEndPointName); //so we can use after the create result CreatedAtRoute()

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
                EventSongs = new() { }
            };

            await eventsRepository.CreateEventAsync(ev);

            // Save songs ID
            // if (evDto.SongIds is not null)
            // {
            //     int[] songIds = evDto.SongIds;
            //     if (songIds.Length > 0)
            //     {
            //         foreach (var so in songIds)
            //         {
            //             EventSong es = new()
            //             {
            //                 EventId = ev.Id,
            //                 SongId = so
            //             };

            //             await eventSongRepository.CreateMultipleEventSongAsync(es);

            //         }
            //         await eventSongRepository.CreateEventSongAsync();
            //     }
            // }

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