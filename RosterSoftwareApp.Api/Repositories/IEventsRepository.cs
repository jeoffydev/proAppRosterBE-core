using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public interface IEventsRepository
{
    Task CreateEventAsync(Event ev);
    Task DeleteEventAsync(int id);
    Task<IEnumerable<Event>> GetAllAsync(int pageNumber, int pageSize);

    Task<Event?> GetEventAsync(int id);
    Task UpdateEventAsync(Event updatedEvent);

    Task<int> CountAsync();
}
