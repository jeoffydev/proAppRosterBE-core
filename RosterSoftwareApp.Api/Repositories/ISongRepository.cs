using RosterSoftwareApp.Api.Entities;


namespace RosterSoftwareApp.Api.Repositories;

public interface ISongRepository
{
    Task CreateSongAsync(Song s);
    Task DeleteSongAsync(int id);
    Task<IEnumerable<Song>> GetAllAsync();
    Task<Song?> GetSongAsync(int id);
    Task UpdateSongAsync(Song updatedSong);
}
