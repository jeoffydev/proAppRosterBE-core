
using System.ComponentModel.DataAnnotations;

namespace RosterSoftwareApp.Api.AllDtos;


public record SongDto(
    int Id,
    [Required]
    [StringLength(100)]
    string Title,
    string? Artist,
    string? SongUrl,
    string? Description,
    bool ToLearn,
    string? YoutubeEmbed
);

public record CreateSongDto(
    [Required]
    [StringLength(100)]
    string Title,
    string? Artist,
    string? SongUrl,
    string? Description,
    bool ToLearn,
    string? YoutubeEmbed
);

public record UpdateSongDto(
    [Required]
    [StringLength(100)]
    string Title,
    string? Artist,
    string? SongUrl,
    string? Description,
    bool ToLearn,
    string? YoutubeEmbed
);