using System.ComponentModel.DataAnnotations;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.AllDtos;


public record EventSongDto(
    int Id,
    int EventId,
    Event? Event,
    int SongId,
    Song? Song
);

public record GetEventSongDto(
    [Required]
     int EventId,
     [Required]
     int SongId
);


public record CreateEventSongDto(
    [Required]
     int EventId,
     [Required]
     int SongId
);


public record DeleteEventSongDto(
    [Required]
     int EventId,
     [Required]
     int SongId
);