using RosterSoftwareApp.Api.AllDtos;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Endpoints;

public static class InstrumentsEndpoint
{
    const string GetInstrumentEndPointName = "GetInstrumentByID";

    public static RouteGroupBuilder MapInstrumentsEndpoint(this IEndpointRouteBuilder routes)
    {
        var groupRoute = routes.MapGroup("/instruments")
                        .WithParameterValidation();

        /* this can be read access which admin also has */


        // Get all Instruments 
        groupRoute.MapGet("/", async (IInstrumentRepository instrumentRepository) =>
        (await instrumentRepository.GetAllInstrumentsAsync()).Select(e => e.AsInstrumentDto()))
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        );



        // Get Instrument by ID 
        groupRoute.MapGet("/{id}", async (IInstrumentRepository insRepository, int id) =>
        {
            Instrument? i = await insRepository.GetInstrumentAsync(id);
            return i is not null ? Results.Ok(i.AsInstrumentDto()) : Results.NotFound();

        }).WithName(GetInstrumentEndPointName)
        .RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        // Create Instrument and received the Dtos type
        groupRoute.MapPost("/", async (IInstrumentRepository insRepository, CreateInstrumentDto iDto) =>
        {
            //Map the DTOs type to Song type
            Instrument ins = new()
            {
                InstrumentName = iDto.InstrumentName,
                Description = iDto.Description
            };
            await insRepository.CreateInstrumentAsync(ins);
            // return the latest created using the Get by ID
            return Results.CreatedAtRoute(GetInstrumentEndPointName, new { Id = ins.Id }, ins);
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        // Edit Song
        groupRoute.MapPut("/{id}", async (IInstrumentRepository insRepository, int id, UpdateInstrumentDto updateInsDto) =>
        {
            Instrument? ins = await insRepository.GetInstrumentAsync(id);
            if (ins is null)
            {
                return Results.NotFound();
            }
            ins.InstrumentName = updateInsDto.InstrumentName;
            ins.Description = updateInsDto.Description;

            await insRepository.UpdateInstrumentAsync(ins);

            return Results.NoContent();
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        // DELETE Instrument

        groupRoute.MapDelete("/{id}", async (IInstrumentRepository insRepository, int id) =>
        {
            Instrument? ins = await insRepository.GetInstrumentAsync(id);
            if (ins is not null)
            {
                await insRepository.DeleteInstrumentAsync(id);
            }
            return Results.NoContent();
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );




        return groupRoute;
    }

}