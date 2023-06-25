using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public class EntityFrameworkMemberInstrumentRepository : IMemberInstrumentRepository
{
    private readonly RosterStoreContext dbContext; //ctrl .

    public EntityFrameworkMemberInstrumentRepository(RosterStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<MemberInstrument>> GetAllMemberInstrumentsAsync()
    {
        return await dbContext.MemberInstruments.Include(i => i.Instrument).ToListAsync();
    }

    public async Task<MemberInstrument?> GetMemberInstrumentAsync(MemberInstrument mi)
    {

        return await dbContext.MemberInstruments
        .Include(i => i.Instrument)
        .AsNoTracking()
        .Where(i => i.InstrumentId == mi.InstrumentId)
        .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberInstrument>> GetMemberInstrumentByMemberIdAsync(string id)
    {
        return await dbContext.MemberInstruments
        .Include(i => i.Instrument)
        .AsNoTracking()
        .Where(i => i.MemberId == id)
        .ToListAsync();
    }


    public async Task<MemberInstrument?> GetMemberInstrumentByIdAsync(int id)
    {
        return await dbContext.MemberInstruments
        .Include(i => i.Instrument)
        .Where(mi => mi.Id == id)
        .FirstOrDefaultAsync();
    }

    public async Task CreateMemberInstrumentAsync(MemberInstrument mi)
    {
        dbContext.MemberInstruments.Add(mi);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteMemberInstrumentAsync(int id)
    {
        await dbContext.MemberInstruments.Where(e => e.Id == id).ExecuteDeleteAsync();
    }


}