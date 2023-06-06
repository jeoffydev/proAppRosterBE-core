using RosterSoftwareApp.Api.Entities;

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

app.MapGet("/events", () => events);
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
});

app.Run();
