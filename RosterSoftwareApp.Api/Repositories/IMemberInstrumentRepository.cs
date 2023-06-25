using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public interface IMemberInstrumentRepository
{
    Task CreateMemberInstrumentAsync(MemberInstrument mi);
    Task DeleteMemberInstrumentAsync(int id);
    Task<IEnumerable<MemberInstrument>> GetAllMemberInstrumentsAsync();
    Task<MemberInstrument?> GetMemberInstrumentAsync(MemberInstrument mi);
    Task<MemberInstrument?> GetMemberInstrumentByIdAsync(int id);
    Task<IEnumerable<MemberInstrument>> GetMemberInstrumentByMemberIdAsync(string id);
}
