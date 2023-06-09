using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public class InMemEventsRepository : IEventsRepository
{
    private List<Event> events = new() {
        new Event() {
            Id = 1,
            Title = "Title1",
            EventDate = new DateTime(2023, 6, 7),
            EventTime = "10:00AM",
            Description = "Test1 Description",
            Active = true
        },
        new Event() {
            Id = 2,
            Title = "Title2",
            EventDate = new DateTime(2023, 7, 8),
            EventTime = "11:00AM",
            Description = "Test2 Description",
            Active = true
        },
        new Event() {
            Id = 3,
            Title = "Title3",
            EventDate = new DateTime(2023, 8, 9),
            EventTime = "12:00PM",
            Description = "Test3 Description",
            Active = false
        }
    };

    public IEnumerable<Event> GetAll()
    {
        return events;
    }

    public Event? GetEvent(int id)
    {
        return events.Find(e => e.Id == id);
    }

    public void CreateEvent(Event ev)
    {
        ev.Id = events.Max(e => e.Id) + 1;
        events.Add(ev);
    }

    public void UpdateEvent(Event updatedEvent)
    {
        var findIndex = events.FindIndex(e => e.Id == updatedEvent.Id);
        events[findIndex] = updatedEvent;
    }

    public void DeleteEvent(int id)
    {
        var findIndex = events.FindIndex(e => e.Id == id);
        events.RemoveAt(findIndex);
    }

}