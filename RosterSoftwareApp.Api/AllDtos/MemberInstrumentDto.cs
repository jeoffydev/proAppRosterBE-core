using System.ComponentModel.DataAnnotations;
using RosterSoftwareApp.Api.Entities;


namespace RosterSoftwareApp.Api.AllDtos;

public record MemberInstrumentDto(
    int Id,
    [Required]
    string MemberId,
    [Required]
    int InstrumentId,
    Instrument? Instrument
);

public record GetMemberInstrumentDto(
    int Id,
    [Required]
    string MemberId,
    [Required]
    int InstrumentId,
    Instrument? Instrument
);


public record CreateMemberInstrumentDto(
    int Id,
    [Required]
    string MemberId,
    [Required]
    int InstrumentId
);


public record DeleteMemberInstrumentDto(
    int Id,
    [Required]
    string MemberId,
    [Required]
    int InstrumentId
);