using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public interface IEventSongRepository
{
    Task<IEnumerable<EventSong>> GetAllAsync();
    Task CreateEventSongAsync(EventSong es);

    Task<EventSong?> GetEventSongAsync(EventSong es);

    Task<EventSong?> GetEventSongByIdAsync(int id);

    Task DeleteEventSongAsync(int id);
}
