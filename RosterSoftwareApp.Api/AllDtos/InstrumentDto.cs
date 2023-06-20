using System.ComponentModel.DataAnnotations;

namespace RosterSoftwareApp.Api.AllDtos;

public record InstrumentDto(
    int Id,
    [Required]
    [StringLength(20)]
    string InstrumentName,
    string? Description
);

public record CreateInstrumentDto(
    [Required]
    [StringLength(20)]
    string InstrumentName,
    string? Description
);

public record UpdateInstrumentDto(
    [Required]
    [StringLength(20)]
    string InstrumentName,
    string? Description
);