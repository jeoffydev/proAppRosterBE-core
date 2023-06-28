

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

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        // sample throw new InvalidOperationException("The database");
        return await dbContext.Events
        .OrderByDescending((e) => e.EventDate)
        .AsNoTracking().ToListAsync();

        // return await dbContext.Events
        //         .Select(c => new
        //         {
        //             c.Id,
        //             c.Title,
        //             c.EventDate,
        //             c.Description,
        //             c.EventTime,
        //             c.Active,
        //             EventSong = c.EventSongs
        //                 .Select(e => new { e.Id, e.Song })
        //                 .ToList()
        //         }).ToListAsync();
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

}