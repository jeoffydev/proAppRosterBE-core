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

    // public int EventId { get; set; }
    //public required Event Event { get; set; }

    //public required MemberEvent MemberEvent { get; set; }

    // public int MemberInstrumentId { get; set; }

    // public required MemberInstrument MemberInstrument { get; set; }

    public required IEnumerable<EventSong> EventSong { get; set; }
}