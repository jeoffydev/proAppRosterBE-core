using System.ComponentModel.DataAnnotations;

namespace RosterSoftwareApp.Api.Entities;

public class Notification
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Title { get; set; }
    [Required]
    public required string Description { get; set; }

    public int Active { get; set; } = 0;
}