using System.ComponentModel.DataAnnotations;
using RosterSoftwareApp.Api.Entities;


namespace RosterSoftwareApp.Api.AllDtos;

public record MemberEventDtoV1(
    int Id,
    [Required]
    bool Confirm,
     [Required]
    int EventId,
     [Required]
    int MemberInstrumentId
);

public record MemberEventDtoV2(
    int Id,
    [Required]
    bool Confirm,
    [Required]
    bool Decline,
     [Required]
    int EventId,
     [Required]
    int MemberInstrumentId
);

public record GetMemberEventDto(
    int Id,
    [Required]
    bool Confirm,
     [Required]
    int EventId,
     [Required]
    int MemberInstrumentId,
    MemberInstrument MemberInstrument
);


public record CreateMemberEventDto(
    [Required]
    bool Confirm,
     [Required]
    int EventId,
    [Required]
    int MemberInstrumentId
);

public record UpdateConfirmMemberEventDto(
    [Required]
    bool Confirm
);

public record DeleteMemberEventDto(
    int Id,
    [Required]
    int EventId,
    [Required]
    int MemberInstrumentId
);