using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public interface IEventsRepository
{
    void CreateEvent(Event ev);
    void DeleteEvent(int id);
    IEnumerable<Event> GetAll();
    Event? GetEvent(int id);
    void UpdateEvent(Event updatedEvent);
}
