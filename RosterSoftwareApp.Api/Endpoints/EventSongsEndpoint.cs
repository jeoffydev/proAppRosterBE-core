using RosterSoftwareApp.Api.AllDtos;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Endpoints;

public static class EventSongsEndpoint
{
    const string GetEventSongEndPointName = "GetEventSongByID";
    public static RouteGroupBuilder MapEventSongsEndpoint(this IEndpointRouteBuilder routes)
    {
        var groupRoute = routes.MapGroup("/eventsongs")
                        .WithParameterValidation();


        // Get all  Event Songs

        groupRoute.MapGet("/", async (IEventSongRepository eventSongRepository) =>
        (await eventSongRepository.GetAllAsync()).Select(e => e.AsEventSongDto()));

        // Get Song by ID 
        groupRoute.MapGet("/{id}", async (IEventSongRepository eventSongRepository, int id) =>
        {
            EventSong? s = await eventSongRepository.GetEventSongByIdAsync(id);
            return s is not null ? Results.Ok(s.AsEventSongDto()) : Results.NotFound();

        }).WithName(GetEventSongEndPointName);

        // Get Song by object
        groupRoute.MapPost("/Get", async (IEventSongRepository eventSongRepository, GetEventSongDto esDto) =>
        {
            EventSong so = new()
            {
                SongId = esDto.SongId,
                EventId = esDto.EventId
            };
            EventSong? s = await eventSongRepository.GetEventSongAsync(so);
            return s is not null ? Results.Ok(s.AsEventSongDto()) : Results.NotFound();

        });

        //     //Create event and songs relation
        groupRoute.MapPost("/New", async (IEventSongRepository eventSongRepository, CreateEventSongDto esDto) =>
       {
           //Map the DTOs type to Event Song type
           EventSong so = new()
           {
               SongId = esDto.SongId,
               EventId = esDto.EventId
           };
           await eventSongRepository.CreateEventSongAsync(so);
           // return the latest created using the Get by ID
           return Results.CreatedAtRoute(GetEventSongEndPointName, new { Id = so.Id }, so);
       });


        //  DELETE song

        groupRoute.MapDelete("/{id}", async (IEventSongRepository eventSongRepository, int id) =>
        {
            EventSong? so = await eventSongRepository.GetEventSongByIdAsync(id);
            if (so is not null)
            {
                await eventSongRepository.DeleteEventSongAsync(id);
            }
            return Results.NoContent();
        });

        return groupRoute;
    }

}