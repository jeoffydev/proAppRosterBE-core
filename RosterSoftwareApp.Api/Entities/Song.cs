using System.ComponentModel.DataAnnotations;

namespace RosterSoftwareApp.Api.Entities;

public class Song
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Title { get; set; }
    [StringLength(50)]
    public string? Artist { get; set; }
    public string? SongUrl { get; set; }
    public string? Description { get; set; }
    public bool ToLearn { get; set; } = false;

}