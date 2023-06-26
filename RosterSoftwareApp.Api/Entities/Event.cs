using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RosterSoftwareApp.Api.Entities;

public class Event
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Title { get; set; }
    [Required]
    public required DateTime? EventDate { get; set; }
    [Required]
    [StringLength(50)]
    public required string EventTime { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public required bool Active { get; set; } = false;

    [NotMapped]
    public required List<EventSong> EventSongs { get; set; }

    [NotMapped]
    public required List<MemberEvent> MemberEvents { get; set; }

}