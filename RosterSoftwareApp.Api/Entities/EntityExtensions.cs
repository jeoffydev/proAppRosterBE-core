
using RosterSoftwareApp.Api.AllDtos;
using RosterSoftwareApp.Api.Dtos;
using RosterSoftwareApp.Api.ViewModels;

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

    public static NotificationDto AsNotificationDto(this Notification no)
    {
        return new NotificationDto(
            no.Id,
            no.Title,
            no.Description,
            no.Active
        );
    }


    public static MemberInstrumentDto AsMemberInstrumentDto(this MemberInstrument memberInstrument)
    {
        return new MemberInstrumentDto(
            memberInstrument.Id,
            memberInstrument.MemberId,
            memberInstrument.MemberName,
            memberInstrument.InstrumentId,
            memberInstrument.Instrument
        );
    }

    public static MemberEventDto AsMemberEventDto(this MemberEvent memberEvent)
    {
        return new MemberEventDto(
            memberEvent.Id,
            memberEvent.Confirm,
            memberEvent.EventId,
            memberEvent.MemberInstrumentId
        );
    }



}