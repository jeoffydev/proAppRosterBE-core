
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RosterSoftwareApp.Api.Entities;

public class MemberInstrument
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string MemberId { get; set; }

    [Required]
    public required string MemberName { get; set; }

    [Required]
    public int InstrumentId { get; set; }

    [ForeignKey("InstrumentId")]

    public virtual Instrument? Instrument { get; set; }
}