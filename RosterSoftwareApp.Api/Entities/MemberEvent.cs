using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RosterSoftwareApp.Api.Entities;

public class MemberEvent
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required bool Confirm { get; set; } = false;

    public int EventId { get; set; }

    [ForeignKey("EventId")]
    public virtual Event? Event { get; set; }

    public int MemberInstrumentId { get; set; }

    [ForeignKey("MemberInstrumentId")]
    public virtual MemberInstrument? MemberInstrument { get; set; }

}