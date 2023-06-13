using System.ComponentModel.DataAnnotations;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.ViewModels;

public class EventViewModel
{
    public required Event Event { get; set; }
    public required List<EventSong> EventSongs { get; set; }
}

public class EventSongViewModel
{
    public int Id { get; set; }
    public Song? Song { get; set; }
}