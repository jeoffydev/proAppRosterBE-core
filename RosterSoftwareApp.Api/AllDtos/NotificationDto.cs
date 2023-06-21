
using System.ComponentModel.DataAnnotations;


namespace RosterSoftwareApp.Api.AllDtos;

public record NotificationDto(
    int Id,
     [Required]
    [StringLength(50)]
    string Title,
     [Required]
    string Description,
    int Active
);


public record CreateNotificationDto(
     [Required]
    [StringLength(50)]
    string Title,
     [Required]
    string Description,
    int Active
);

public record UpdateNotificationDto(
     [Required]
    [StringLength(50)]
    string Title,
     [Required]
    string Description,
    int Active
);