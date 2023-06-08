
using RosterSoftwareApp.Api.Dtos;

namespace RosterSoftwareApp.Api.Entities;

public static class EntityExtensions
{
    public static EventDto AsDto(this Event events)
    {
        return new EventDto(
            events.Id,
            events.Title,
            events.EventDate,
            events.EventTime,
            events.Description,
            events.Active
        );
    }
}