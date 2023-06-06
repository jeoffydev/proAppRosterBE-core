using RosterSoftwareApp.Api.Entities;

const string GetEventEndPointName = "GetEvent";

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

// Get all Events
app.MapGet("/events", () => events);

// Get Event by ID 
app.MapGet("/event/{id}", (int id) =>
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
app.MapPost("/event", (Event ev) =>
{
    ev.Id = events.Max(e => e.Id) + 1;
    events.Add(ev);

    // return the latest created
    return Results.CreatedAtRoute(GetEventEndPointName, new { Id = ev.Id }, ev);
});

// Edit Event
app.MapPut("/event/{id}", (int id, Event updatedEv) =>
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

app.Run();
