using System.ComponentModel.DataAnnotations;

namespace RosterSoftwareApp.Api.Entities;

public class Event
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter a title for this event")]
    [StringLength(100)]
    public required string Title { get; set; }
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Please enter a date for this event")]
    public DateTime EventDate { get; set; }
    [Required(ErrorMessage = "Please enter a time for this event")]
    public required string EventTime { get; set; }
    [Required(ErrorMessage = "Please enter a description for this event")]
    public required string Description { get; set; }
}