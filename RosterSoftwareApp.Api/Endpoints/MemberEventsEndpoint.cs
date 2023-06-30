using RosterSoftwareApp.Api.AllDtos;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Repositories;

namespace RosterSoftwareApp.Api.Endpoints;

public static class MemberEventsEndpoint
{
    const string GetMemberEventEndPointNameV1 = "GetMemberEventByIDV1";
    const string GetMemberEventEndPointNameV2 = "GetMemberEventByIDV2";
    public static RouteGroupBuilder MapMemberEventsEndpoint(this IEndpointRouteBuilder routes)
    {
        // Note if remove the version in MapGroup then use Query string = /memberevents?api-version=1.0
        var groupRoute = routes
                        .NewVersionedApi()
                        .MapGroup("/memberevents")
                        .HasApiVersion(1.0)
                        .HasApiVersion(2.0)
                        .WithParameterValidation();

        // Get all  MemberEvents
        groupRoute.MapGet("/", async (IMemberEventRepository memberEventRepository) =>
        (await memberEventRepository.GetAllMemberEventsAsync()).Select(m => m.AsMemberEventDtoV1()))
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        ).MapToApiVersion(1.0);

        // Get MI by ID 
        groupRoute.MapGet("/{id}", async (IMemberEventRepository memberEventRepository, int id) =>
        {
            MemberEvent? me = await memberEventRepository.GetMemberEventByIdAsync(id);
            return me is not null ? Results.Ok(me.AsMemberEventDtoV1()) : Results.NotFound();

        })
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        ).WithName(GetMemberEventEndPointNameV1).MapToApiVersion(1.0);

        // Get ME by event ID 
        groupRoute.MapGet("/eventId/{id}", async (IMemberEventRepository memberEventRepository, int id) =>
        {
            return (await memberEventRepository.GetMemberEventByEventIdAsync(id)).Select(e => e.AsMemberEventDtoV1());

        }).RequireAuthorization(
            PoliciesClaim.ReadAccess
        ).MapToApiVersion(1.0);

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
           return Results.CreatedAtRoute(GetMemberEventEndPointNameV1, new { Id = me.Id }, me);
       }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        ).MapToApiVersion(1.0);

        //  DELETE MemberEvent
        groupRoute.MapDelete("/{id}", async (IMemberEventRepository memberEventRepository, int id) =>
        {
            MemberEvent? me = await memberEventRepository.GetMemberEventByIdAsync(id);
            if (me is not null)
            {
                await memberEventRepository.DeleteMemberEventAsync(id);
            }
            return Results.NoContent();
        }).RequireAuthorization(
            PoliciesClaim.WriteAccess
        ).MapToApiVersion(1.0);

        // Members area

        // Edit EventMember Confirm
        groupRoute.MapPatch("/{id}", async (IMemberEventRepository memberEventRepository, int id, UpdateConfirmMemberEventDto ucDto) =>
        {
            MemberEvent? ev = await memberEventRepository.GetMemberEventByIdAsync(id);
            if (ev is null)
            {
                return Results.NotFound();
            }
            ev.Confirm = ucDto.Confirm;

            await memberEventRepository.UpdateConfirmMemberEventAsync(ev);

            return Results.NoContent();
        }).RequireAuthorization(PoliciesClaim.ReadAccess).MapToApiVersion(1.0);


        // API ENDPOINTS Version 2 ******************************************** Change DTO first for versioning

        groupRoute.MapGet("/", async (IMemberEventRepository memberEventRepository) =>
        (await memberEventRepository.GetAllMemberEventsAsync()).Select(m => m.AsMemberEventDtoV2()))
        .RequireAuthorization(
            PoliciesClaim.ReadAccess
        ).MapToApiVersion(2.0);


        return groupRoute;
    }
}