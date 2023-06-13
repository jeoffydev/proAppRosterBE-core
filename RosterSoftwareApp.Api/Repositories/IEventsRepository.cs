using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public interface IEventsRepository
{
    Task CreateEventAsync(Event ev);
    Task DeleteEventAsync(int id);
    Task<IEnumerable<Event>> GetAllAsync();

    Task<Event?> GetEventAsync(int id);
    Task UpdateEventAsync(Event updatedEvent);
}
