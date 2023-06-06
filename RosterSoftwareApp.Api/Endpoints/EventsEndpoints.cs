using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Endpoints;

public static class EventsEndpoints
{
    const string GetEventEndPointName = "GetEventByID";

    static List<Event> events = new() {
        new Event() {
            Id = 1,
            Title = "Title1",
            EventDate = new DateTime(2023, 6, 7),
            EventTime = "10:00AM",
            Description = "Test1 Description"
        },
        new Event() {
            Id = 2,
            Title = "Title2",
            EventDate = new DateTime(2023, 7, 8),
            EventTime = "11:00AM",
            Description = "Test2 Description"
        },
        new Event() {
            Id = 3,
            Title = "Title3",
            EventDate = new DateTime(2023, 8, 9),
            EventTime = "12:00PM",
            Description = "Test3 Description"
        }
    };

    public static RouteGroupBuilder MapEventsEndpoint(this IEndpointRouteBuilder routes)
    {
        var groupRoute = routes.MapGroup("/events")
                        .WithParameterValidation();

        // Get all Events
        groupRoute.MapGet("/", () => events);

        // Get Event by ID 
        groupRoute.MapGet("/{id}", (int id) =>
        {
            Event? ev = events.Find(e => e.Id == id);
            if (ev is null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(ev);
            }
        }).WithName(GetEventEndPointName); //so we can use after the create result CreatedAtRoute()

        // Create Event
        groupRoute.MapPost("/", (Event ev) =>
        {
            ev.Id = events.Max(e => e.Id) + 1;
            events.Add(ev);

            // return the latest created using the Get by ID
            return Results.CreatedAtRoute(GetEventEndPointName, new { Id = ev.Id }, ev);
        });

        // Edit Event
        groupRoute.MapPut("/{id}", (int id, Event updatedEv) =>
        {
            Event? ev = events.Find(e => e.Id == id);
            if (ev is null)
            {
                return Results.NotFound();
            }
            ev.Title = updatedEv.Title;
            ev.Description = updatedEv.Description;
            ev.EventDate = updatedEv.EventDate;
            ev.EventTime = updatedEv.EventTime;
            return Results.NoContent();
        });

        // DELETE event

        groupRoute.MapDelete("/{id}", (int id) =>
        {
            Event? ev = events.Find(e => e.Id == id);
            if (ev is not null)
            {
                events.Remove(ev);
            }

            return Results.NoContent();
        });

        return groupRoute;
    }
}