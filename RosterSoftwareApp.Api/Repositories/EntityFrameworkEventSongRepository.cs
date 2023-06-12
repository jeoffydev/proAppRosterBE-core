using RosterSoftwareApp.Api.Data;
using Microsoft.EntityFrameworkCore;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public class EntityFrameworkEventSongRepository : IEventSongRepository
{
    private readonly RosterStoreContext dbContext; //ctrl .

    public EntityFrameworkEventSongRepository(RosterStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<EventSong>> GetAllAsync()
    {
        return await dbContext.EventSongs.Include(b => b.Song).ToListAsync();
    }

    public async Task<EventSong?> GetEventSongAsync(EventSong es)
    {

        return await dbContext.EventSongs
        .Include(s => s.Song)
        .AsNoTracking()
        .Where(s => s.SongId == es.SongId)
        .Where(s => s.EventId == es.EventId)
        .FirstOrDefaultAsync();
    }

    public async Task<EventSong?> GetEventSongByIdAsync(int id)
    {
        return await dbContext.EventSongs
        .Include(s => s.Song)
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
    }

    public async Task CreateEventSongAsync(EventSong es)
    {
        dbContext.EventSongs.Add(es);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteEventSongAsync(int id)
    {
        await dbContext.EventSongs.Where(e => e.Id == id).ExecuteDeleteAsync();
    }
}