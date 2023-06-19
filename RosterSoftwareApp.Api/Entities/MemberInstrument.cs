
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RosterSoftwareApp.Api.Entities;

public class MemberInstrument
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string MemberId { get; set; }


    public int InstrumentId { get; set; }

    [ForeignKey("InstrumentId")]

    [NotMapped]
    public required List<Instrument> Instruments { get; set; }
}