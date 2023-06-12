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
            Active = true,
            EventSongs = new(){
            }
        },
        new Event() {
            Id = 2,
            Title = "Title 2",
            EventDate = new DateTime(2023, 7, 8),
            EventTime = "11:00AM",
            Description = "Test2 Description",
            Active = true,
            EventSongs = new(){
            }
        },
        new Event() {
            Id = 3,
            Title = "Title3",
            EventDate = new DateTime(2023, 8, 9),
            EventTime = "12:00PM",
            Description = "Test3 Description",
            Active = false,
            EventSongs = new(){
            }
        }
    };

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        return await Task.FromResult(events);
    }

    public async Task<Event?> GetEventAsync(int id)
    {
        return await Task.FromResult(events.Find(e => e.Id == id));
    }

    public async Task CreateEventAsync(Event ev)
    {
        ev.Id = events.Max(e => e.Id) + 1;
        events.Add(ev);

        await Task.CompletedTask;
    }

    public async Task UpdateEventAsync(Event updatedEvent)
    {
        var findIndex = events.FindIndex(e => e.Id == updatedEvent.Id);
        events[findIndex] = updatedEvent;

        await Task.CompletedTask;
    }

    public async Task DeleteEventAsync(int id)
    {
        var findIndex = events.FindIndex(e => e.Id == id);
        events.RemoveAt(findIndex);

        await Task.CompletedTask;
    }

}