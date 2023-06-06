namespace RosterSoftwareApp.Api.Entities;

public class Event
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public DateTime EventDate { get; set; }
    public required string Description { get; set; }
}