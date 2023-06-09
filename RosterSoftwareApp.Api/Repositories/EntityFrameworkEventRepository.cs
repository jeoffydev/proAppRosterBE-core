

using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace RosterSoftwareApp.Api.Repositories;

public class EntityFrameworkEventRepository : IEventsRepository
{

    private readonly RosterStoreContext dbContext; //ctrl .

    public EntityFrameworkEventRepository(RosterStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<Event> GetAll()
    {
        return dbContext.Events.AsNoTracking().ToList();
    }

    public Event? GetEvent(int id)
    {
        return dbContext.Events.Find(id);
    }

    public void CreateEvent(Event ev)
    {
        dbContext.Events.Add(ev);
        dbContext.SaveChanges();
    }

    public void UpdateEvent(Event updatedEvent)
    {
        dbContext.Events.Update(updatedEvent);
        dbContext.SaveChanges();
    }

    public void DeleteEvent(int id)
    {
        dbContext.Events.Where(e => e.Id == id).ExecuteDelete();
    }
}