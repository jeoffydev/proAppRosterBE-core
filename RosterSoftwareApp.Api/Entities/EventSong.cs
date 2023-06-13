using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RosterSoftwareApp.Api.Entities;

public class EventSong
{
    [Key]
    public int Id { get; set; }
    public int EventId { get; set; }

    [ForeignKey("EventId")]
    public virtual Event? Event { get; set; }

    public int SongId { get; set; }

    [ForeignKey("SongId")]
    public virtual Song? Song { get; set; }
}