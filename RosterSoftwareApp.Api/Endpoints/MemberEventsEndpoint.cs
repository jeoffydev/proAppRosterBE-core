using RosterSoftwareApp.Api.AllDtos;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Endpoints;

public static class MemberEventsEndpoint
{
    const string GetMemberEventEndPointName = "GetMemberEventByID";
    public static RouteGroupBuilder MapMemberEventsEndpoint(this IEndpointRouteBuilder routes)
    {
        var groupRoute = routes.MapGroup("/memberevents")
                        .WithParameterValidation();

        // Get all  MemberEvents
        groupRoute.MapGet("/", async (IMemberEventRepository memberEventRepository) =>
        (await memberEventRepository.GetAllMemberEventsAsync()).Select(m => m.AsMemberEventDto()))
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        );

        // Get MI by ID 
        groupRoute.MapGet("/{id}", async (IMemberEventRepository memberEventRepository, int id) =>
        {
            MemberEvent? me = await memberEventRepository.GetMemberEventByIdAsync(id);
            return me is not null ? Results.Ok(me.AsMemberEventDto()) : Results.NotFound();

        })
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        ).WithName(GetMemberEventEndPointName);

        // Get ME by event ID 
        groupRoute.MapGet("/eventId/{id}", async (IMemberEventRepository memberEventRepository, int id) =>
        {
            return (await memberEventRepository.GetMemberEventByEventIdAsync(id)).Select(e => e.AsMemberEventDto());

        }).RequireAuthorization(
            PoliciesClaim.ReadAccess
        );

        //Create member and instrumentevent relation
        groupRoute.MapPost("/", async (IMemberEventRepository memberEventRepository, CreateMemberEventDto meDto) =>
       {
           //Map the DTOs type to MI type
           MemberEvent me = new()
           {
               Confirm = meDto.Confirm,
               MemberInstrumentId = meDto.MemberInstrumentId,
               EventId = meDto.EventId
           };
           await memberEventRepository.CreateMemberEventAsync(me);
           // return the latest created using the Get by ID
           return Results.CreatedAtRoute(GetMemberEventEndPointName, new { Id = me.Id }, me);
       }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        );

        return groupRoute;
    }
}