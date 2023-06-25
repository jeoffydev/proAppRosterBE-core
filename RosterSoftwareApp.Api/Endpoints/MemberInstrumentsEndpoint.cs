using RosterSoftwareApp.Api.AllDtos;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Endpoints;

public static class MemberInstrumentEndpoint
{
    const string GetMemberInstrumentEndPointName = "GetMemberInstrumentByID";
    public static RouteGroupBuilder MapMemberInstrumentsEndpoint(this IEndpointRouteBuilder routes)
    {
        var groupRoute = routes.MapGroup("/memberinstruments")
                        .WithParameterValidation();

        // Get all  MemberInstruments
        groupRoute.MapGet("/", async (IMemberInstrumentRepository memberInstrumentRepository) =>
        (await memberInstrumentRepository.GetAllMemberInstrumentsAsync()).Select(m => m.AsMemberInstrumentDto()))
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        );

        // Get Song by ID 
        groupRoute.MapGet("/{id}", async (IMemberInstrumentRepository memberInstrumentRepository, int id) =>
        {
            MemberInstrument? mi = await memberInstrumentRepository.GetMemberInstrumentByIdAsync(id);
            return mi is not null ? Results.Ok(mi.AsMemberInstrumentDto()) : Results.NotFound();

        })
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        ).WithName(GetMemberInstrumentEndPointName);

        //Create member and instrument relation
        groupRoute.MapPost("/", async (IMemberInstrumentRepository memberInstrumentRepository, CreateMemberInstrumentDto miDto) =>
       {
           //Map the DTOs type to Event Song type
           MemberInstrument mi = new()
           {
               MemberId = miDto.MemberId,
               InstrumentId = miDto.InstrumentId
           };
           await memberInstrumentRepository.CreateMemberInstrumentAsync(mi);
           // return the latest created using the Get by ID
           return Results.CreatedAtRoute(GetMemberInstrumentEndPointName, new { Id = mi.Id }, mi);
       }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        //  DELETE MemberInstrument

        groupRoute.MapDelete("/{id}", async (IMemberInstrumentRepository memberInstrumentRepository, int id) =>
        {
            MemberInstrument? mi = await memberInstrumentRepository.GetMemberInstrumentByIdAsync(id);
            if (mi is not null)
            {
                await memberInstrumentRepository.DeleteMemberInstrumentAsync(id);
            }
            return Results.NoContent();
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        return groupRoute;
    }



}