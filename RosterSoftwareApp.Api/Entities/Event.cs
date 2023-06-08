using System.ComponentModel.DataAnnotations;

namespace RosterSoftwareApp.Api.Entities;

public class Event
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Title { get; set; }
    [Required]
    public DateTime? EventDate { get; set; }
    [Required]
    [StringLength(10)]
    public required string EventTime { get; set; }
    [Required]
    public required string Description { get; set; }
}