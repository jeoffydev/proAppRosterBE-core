
using System.ComponentModel.DataAnnotations;


namespace RosterSoftwareApp.Api.Entities;

public class Instrument
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(20)]
    public required string InstrumentName { get; set; }

    public string? Description { get; set; }

}