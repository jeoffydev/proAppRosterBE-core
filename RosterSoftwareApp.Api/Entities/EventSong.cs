using System.ComponentModel.DataAnnotations;

namespace RosterSoftwareApp.Api.Entities;

public class EventSong
{
    [Key]
    public int Id { get; set; }
    public int EventId { get; set; }
    public Event? Event { get; set; }

    public int SongId { get; set; }
    public Song? Song { get; set; }
}