using System.ComponentModel.DataAnnotations;

namespace RosterSoftwareApp.Api.Dtos;

public record EventDto(
    int Id,
    string Title,
    DateTime? EventDate,
    string EventTime,
    string Description,
    bool Active
);

public record CreateEventDto(
    [Required]
    [StringLength(100)]
    string Title,
    [Required]
    DateTime? EventDate,
    [StringLength(10)]
    [Required]
    string EventTime,
     [Required]
    string Description,
     [Required]
     bool Active
);

public record UpdateEventDto(
    [Required]
    [StringLength(100)]
    string Title,
    [Required]
    DateTime? EventDate,
    [Required]
    string EventTime,
     [Required]
    string Description,
     [Required]
     bool Active
);