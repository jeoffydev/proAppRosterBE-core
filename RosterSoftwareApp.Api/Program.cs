using RosterSoftwareApp.Api.Entities;

const string GetEventEndPointName = "GetEventByID";

List<Event> events = new() {
    new Event() {
        Id = 1,
        Title = "Title1",
        EventDate = new DateTime(2023, 6, 7),
        Description = "Test1 Description"
    },
    new Event() {
        Id = 2,
        Title = "Title2",
        EventDate = new DateTime(2023, 7, 8),
        Description = "Test2 Description"
    },
    new Event() {
        Id = 3,
        Title = "Title3",
        EventDate = new DateTime(2023, 8, 9),
        Description = "Test3 Description"
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var groupRoute = app.MapGroup("/events");

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

app.Run();
