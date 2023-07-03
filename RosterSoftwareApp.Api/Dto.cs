using System.ComponentModel.DataAnnotations;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Dtos;

public record GetEventPaginationDto(
    int pageNumber = 1,
    int pageSize = 5,
    bool? orderByAsc = false
);

public record EventDto(
    int Id,
    string Title,
    DateTime? EventDate,
    string EventTime,
    string Description,
    bool Active,
    List<EventSong>? EventSongs
);

public record CreateEventDto(
    [Required]
    [StringLength(100)]
    string Title,
    [Required]
    DateTime? EventDate,
    [StringLength(50)]
    [Required]
    string EventTime,
     [Required]
    string Description,
     [Required]
     bool Active,
     int[]? SongIds
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