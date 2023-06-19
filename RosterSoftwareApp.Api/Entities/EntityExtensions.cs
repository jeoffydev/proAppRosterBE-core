
using RosterSoftwareApp.Api.AllDtos;
using RosterSoftwareApp.Api.Dtos;

namespace RosterSoftwareApp.Api.Entities;

public static class EntityExtensions
{
    public static EventDto AsDto(this Event events)
    {
        return new EventDto(
            events.Id,
            events.Title,
            events.EventDate,
            events.EventTime,
            events.Description,
            events.Active,
            events.EventSongs
        );
    }

    public static SongDto AsSongDto(this Song songs)
    {
        return new SongDto(
            songs.Id,
            songs.Title,
            songs.Artist,
            songs.SongUrl,
            songs.Description,
            songs.ToLearn
        );
    }

    public static EventSongDto AsEventSongDto(this EventSong eventsong)
    {
        return new EventSongDto(
            eventsong.Id,
            eventsong.EventId,
            eventsong.Event,
            eventsong.SongId,
            eventsong.Song
        );
    }

    public static InstrumentDto AsInstrumentDto(this Instrument ins)
    {
        return new InstrumentDto(
            ins.Id,
            ins.InstrumentName,
            ins.Description
        );
    }



}