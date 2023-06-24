using RosterSoftwareApp.Api.Data;
using Microsoft.EntityFrameworkCore;
using RosterSoftwareApp.Api.Entities;


namespace RosterSoftwareApp.Api.Repositories;

public class EntityFrameworkSongRepository : ISongRepository
{
    private readonly RosterStoreContext dbContext; //ctrl .

    public EntityFrameworkSongRepository(RosterStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Song>> GetAllAsync()
    {
        return await dbContext.Songs
        .OrderBy((s) => s.Title)
        .AsNoTracking().ToListAsync();
    }

    public async Task<Song?> GetSongAsync(int id)
    {
        return await dbContext.Songs.FindAsync(id);
    }

    public async Task CreateSongAsync(Song s)
    {
        dbContext.Songs.Add(s);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateSongAsync(Song updatedSong)
    {
        dbContext.Songs.Update(updatedSong);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteSongAsync(int id)
    {
        await dbContext.Songs.Where(e => e.Id == id).ExecuteDeleteAsync();
    }

    // Members Repository
    public async Task<IEnumerable<Song>> GetAllMemberSongsAsync()
    {
        return await dbContext.Songs
        .Where((s) => s.ToLearn == true)
        .OrderBy((s) => s.Title)
        .AsNoTracking().ToListAsync();
    }

}