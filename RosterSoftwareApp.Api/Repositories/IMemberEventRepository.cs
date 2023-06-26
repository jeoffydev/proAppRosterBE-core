using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public interface IMemberEventRepository
{
    Task CreateMemberEventAsync(MemberEvent mi);
    Task DeleteMemberEventAsync(int id);
    Task<IEnumerable<MemberEvent>> GetMemberEventByEventIdAsync(int id);
    Task<IEnumerable<MemberEvent>> GetAllMemberEventsAsync();
    Task<MemberEvent?> GetMemberEventAsync(MemberEvent me);
    Task<MemberEvent?> GetMemberEventByIdAsync(int id);
}
