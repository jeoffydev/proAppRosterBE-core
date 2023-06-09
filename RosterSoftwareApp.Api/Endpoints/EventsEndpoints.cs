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
        groupRoute.MapGet("/", async (
            IEventsRepository eventsRepository,
            ILoggerFactory loggerFactory,
            [AsParameters] GetEventPaginationDto request,
            HttpContext http
            ) =>
        {
            var totalCount = await eventsRepository.CountAsync();
            http.Response.AddPaginationHeader(totalCount, request.pageSize);

            return Results.Ok((await eventsRepository.GetAllAsync(
                request.pageNumber,
                request.pageSize,
                request.orderByAsc
                )).Select(e => e.AsDto()));

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


        // Get all expired Events
        groupRoute.MapGet("/deleteExpiredEvents", async (
            IEventsRepository eventsRepository
            ) =>
        {
            var getExpiredEvents = await eventsRepository.GetAllExpiredEventsAsync();
            if (getExpiredEvents is not null)
            {
                foreach (var item in getExpiredEvents)
                {
                    await eventsRepository.DeleteEventAsync(item.Id);
                }
            }
            return Results.NoContent();
        }).RequireAuthorization(PoliciesClaim.WriteAccess);


        // Members area
        // Get Events by memberID 
        groupRoute.MapGet("/memberId/{id}", async (
            IEventsRepository eventsRepository,
            IMemberEventRepository memberEventRepository,
            IEventSongRepository eventSongRepository,
            IInstrumentRepository instrumentRepository,
            string id) =>
        {
            // false for orderBy is temporary until new version changes
            IEnumerable<MemberEvent> memberEvents = await memberEventRepository.GetMemberEventByMemberIdAsync(id, false);
            var meListVMInit = new List<MemberEventListViewModel>();

            foreach (var me in memberEvents)
            {
                var es = await eventSongRepository.GetEventSongByEventIdAsync(me.EventId);
                var allMembers = await memberEventRepository.GetMemberEventByEventIdAsync(me.EventId);

                var meWithInsVMInit = new List<MemberEventsWithInstrumentDetailsViewModel>();
                foreach (var myIns in allMembers)
                {
                    if (myIns.MemberInstrument is not null)
                    {
                        var ins = await instrumentRepository.GetInstrumentAsync(myIns.MemberInstrument.InstrumentId);

                        if (ins is not null)
                        {
                            MemberEventsWithInstrumentDetailsViewModel meWithInsVM = new()
                            {
                                Id = myIns.Id,
                                Confirm = myIns.Confirm,
                                EventId = myIns.EventId,
                                Event = myIns.Event,
                                MemberInstrumentId = myIns.MemberInstrumentId,
                                MemberInstrument = myIns.MemberInstrument,
                                Instrument = ins
                            };
                            meWithInsVMInit.Add(meWithInsVM);
                        }
                    }
                }

                MemberEventListViewModel meListVM = new()
                {
                    Id = me.Id,
                    Confirm = me.Confirm,
                    EventSongs = es,
                    EventId = me.EventId,
                    Event = me.Event,
                    MemberInstrumentId = me.MemberInstrumentId,
                    MemberInstrument = me.MemberInstrument,
                    MemberEventInstruments = meWithInsVMInit
                };
                meListVMInit.Add(meListVM);
            }
            return Results.Ok(meListVMInit);
        }).RequireAuthorization(PoliciesClaim.ReadAccess);








        return groupRoute;
    }
}