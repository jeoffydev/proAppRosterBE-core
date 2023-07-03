

using RosterSoftwareApp.Api.Entities;
using RosterSoftwareApp.Api.Data;
using Microsoft.EntityFrameworkCore;


namespace RosterSoftwareApp.Api.Repositories;

public class EntityFrameworkEventRepository : IEventsRepository
{

    private readonly RosterStoreContext dbContext; //ctrl .
    private readonly ILogger<EntityFrameworkEventRepository> logger;

    public EntityFrameworkEventRepository(RosterStoreContext dbContext, ILogger<EntityFrameworkEventRepository> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<int> CountAsync()
    {
        return await dbContext.Events.CountAsync();
    }

    private IQueryable<Event> FilterOrderByClause(bool? orderByAsc)
    {
        if (orderByAsc == true)
        {
            return dbContext.Events
                .OrderBy((e) => e.EventDate);
        }
        else
        {
            return dbContext.Events
                .OrderByDescending((e) => e.EventDate);
        }
    }

    public async Task<IEnumerable<Event>> GetAllAsync(int pageNumber, int pageSize, bool? orderByAsc)
    {
        var skipCount = (pageNumber - 1) * pageSize;

        // sample throw new InvalidOperationException("The database");
        return await FilterOrderByClause(orderByAsc)
        .Skip(skipCount)
        .Take(pageSize)
        .AsNoTracking().ToListAsync();

    }

    public async Task<Event?> GetEventAsync(int id)
    {
        return await dbContext.Events.FindAsync(id);
    }

    public async Task CreateEventAsync(Event ev)
    {
        dbContext.Events.Add(ev);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(" Event created with {Title} and {EventDate} ", ev.Title, ev.EventDate);
    }

    public async Task UpdateEventAsync(Event updatedEvent)
    {
        dbContext.Events.Update(updatedEvent);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteEventAsync(int id)
    {
        await dbContext.Events.Where(e => e.Id == id).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Event>> GetAllExpiredEventsAsync()
    {
        var NumberOfDaysToPurge = 30;
        var purgeDate = DateTime.UtcNow.AddDays(-NumberOfDaysToPurge);

        return await dbContext.Events
        .Where(e => e.EventDate <= purgeDate)
        .AsNoTracking().ToListAsync();
    }
}