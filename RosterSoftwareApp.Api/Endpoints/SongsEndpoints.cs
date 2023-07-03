using RosterSoftwareApp.Api.AllDtos;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Endpoints;

public static class SongsEndpoints
{
    const string GetSongEndPointName = "GetSongByID";
    public static RouteGroupBuilder MapSongsEndpoint(this IEndpointRouteBuilder routes)
    {
        var groupRoute = routes.MapGroup("/songs")
                        .WithParameterValidation();

        // Get all Songs
        groupRoute.MapGet("/", async (ISongRepository songsRepository) =>
        (await songsRepository.GetAllAsync()).Select(e => e.AsSongDto()))
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        );

        // Get Song by ID 
        groupRoute.MapGet("/{id}", async (ISongRepository songsRepository, int id) =>
        {
            Song? s = await songsRepository.GetSongAsync(id);
            return s is not null ? Results.Ok(s.AsSongDto()) : Results.NotFound();

        }).WithName(GetSongEndPointName)
         .RequireAuthorization(
            PoliciesClaim.ReadAccess
        );

        // Create Song and received the Dtos type
        groupRoute.MapPost("/", async (ISongRepository songRepository, CreateSongDto sDto) =>
        {
            //Map the DTOs type to Song type
            Song so = new()
            {
                Title = sDto.Title,
                Artist = sDto.Artist,
                SongUrl = sDto.SongUrl,
                Description = sDto.Description,
                ToLearn = sDto.ToLearn,
                YoutubeEmbed = sDto.YoutubeEmbed
            };
            await songRepository.CreateSongAsync(so);
            // return the latest created using the Get by ID
            return Results.CreatedAtRoute(GetSongEndPointName, new { Id = so.Id }, so);
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        // Edit Song
        groupRoute.MapPut("/{id}", async (ISongRepository songRepository, int id, UpdateSongDto updateSongDto) =>
        {
            Song? so = await songRepository.GetSongAsync(id);
            if (so is null)
            {
                return Results.NotFound();
            }
            so.Title = updateSongDto.Title;
            so.Artist = updateSongDto.Artist;
            so.SongUrl = updateSongDto.SongUrl;
            so.Description = updateSongDto.Description;
            so.ToLearn = updateSongDto.ToLearn;
            so.YoutubeEmbed = updateSongDto.YoutubeEmbed;

            await songRepository.UpdateSongAsync(so);

            return Results.NoContent();
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        // DELETE song

        groupRoute.MapDelete("/{id}", async (ISongRepository songRepository, int id) =>
        {
            Song? so = await songRepository.GetSongAsync(id);
            if (so is not null)
            {
                await songRepository.DeleteSongAsync(id);
            }
            return Results.NoContent();
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        // Members Endpoint

        // Get all To learn Songs for members
        groupRoute.MapGet("/to-learn", async (ISongRepository songsRepository) =>
        (await songsRepository.GetAllMemberSongsAsync()).Select(e => e.AsSongDto()))
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        );

        return groupRoute;
    }
}