using Microsoft.EntityFrameworkCore;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public class EntityFrameworkMemberEventRepository : IMemberEventRepository
{
    private readonly RosterStoreContext dbContext; //ctrl .

    public EntityFrameworkMemberEventRepository(RosterStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<IEnumerable<MemberEvent>> GetAllMemberEventsAsync()
    {
        return await dbContext.MemberEvents.Include(m => m.MemberInstrument).ToListAsync();
    }
    public async Task<MemberEvent?> GetMemberEventAsync(MemberEvent me)
    {

        return await dbContext.MemberEvents
        .Include(i => i.MemberInstrument)
        .AsNoTracking()
        .Where(i => i.MemberInstrumentId == me.MemberInstrumentId)
        .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberEvent>> GetMemberEventByEventIdAsync(int id)
    {
        return await dbContext.MemberEvents
        .Include(i => i.MemberInstrument)
        .AsNoTracking()
        .Where(i => i.EventId == id)
        .ToListAsync();
    }


    public async Task<MemberEvent?> GetMemberEventByIdAsync(int id)
    {
        return await dbContext.MemberEvents
        .Include(i => i.MemberInstrument)
        .Where(mi => mi.Id == id)
        .FirstOrDefaultAsync();
    }

    public async Task CreateMemberEventAsync(MemberEvent mi)
    {
        dbContext.MemberEvents.Add(mi);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteMemberEventAsync(int id)
    {
        await dbContext.MemberEvents.Where(e => e.Id == id).ExecuteDeleteAsync();
    }
}