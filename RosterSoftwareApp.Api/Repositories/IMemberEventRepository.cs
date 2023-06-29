using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public interface IMemberEventRepository
{
    Task CreateMemberEventAsync(MemberEvent mi);
    Task DeleteMemberEventAsync(int id);
    Task UpdateConfirmMemberEventAsync(MemberEvent mi);
    Task<IEnumerable<MemberEvent>> GetMemberEventByEventIdAsync(int id);
    Task<IEnumerable<MemberEvent>> GetAllMemberEventsAsync();
    Task<MemberEvent?> GetMemberEventAsync(MemberEvent me);
    Task<MemberEvent?> GetMemberEventByIdAsync(int id);
    Task<IEnumerable<MemberEvent>> GetMemberEventByMemberIdAsync(string id);
}
