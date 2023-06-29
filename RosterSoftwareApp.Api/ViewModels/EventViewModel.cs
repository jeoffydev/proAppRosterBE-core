using System.ComponentModel.DataAnnotations;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.ViewModels;

public class EventViewModel
{
    public required Event Event { get; set; }
    public required List<EventSong> EventSongs { get; set; }

    public required List<MemberEvent> MemberEvents { get; set; }
}

public class EventSongViewModel
{
    public int Id { get; set; }
    public Song? Song { get; set; }
}


public class MemberEventListViewModel
{
    public int Id { get; set; }

    public required bool Confirm { get; set; } = false;

    public int EventId { get; set; }
    public required Event? Event { get; set; }


    public int MemberInstrumentId { get; set; }

    public required MemberInstrument? MemberInstrument { get; set; }

    public required IEnumerable<EventSong> EventSongs { get; set; }

    public required IEnumerable<MemberEventsWithInstrumentDetailsViewModel> MemberEventInstruments { get; set; }
}


public class MemberEventsWithInstrumentDetailsViewModel
{
    public int Id { get; set; }
    public required bool Confirm { get; set; } = false;

    public int EventId { get; set; }
    public virtual Event? Event { get; set; }

    public int MemberInstrumentId { get; set; }
    public virtual MemberInstrument? MemberInstrument { get; set; }

    public required Instrument Instrument { get; set; }
}