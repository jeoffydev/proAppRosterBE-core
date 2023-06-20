using Microsoft.EntityFrameworkCore;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public class EntityFrameworkInstrumentRepository : IInstrumentRepository
{
    private readonly RosterStoreContext dbContext; //ctrl .
    public EntityFrameworkInstrumentRepository(RosterStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Instrument>> GetAllInstrumentsAsync()
    {
        return await dbContext.Instruments.AsNoTracking().ToListAsync();
    }

    public async Task<Instrument?> GetInstrumentAsync(int id)
    {
        return await dbContext.Instruments.FindAsync(id);
    }

    public async Task CreateInstrumentAsync(Instrument i)
    {
        dbContext.Instruments.Add(i);
        await dbContext.SaveChangesAsync();
    }
    // Update is a temporary and we'll use the delete instead
    public async Task UpdateInstrumentAsync(Instrument updatedInstrument)
    {
        dbContext.Instruments.Update(updatedInstrument);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteInstrumentAsync(int id)
    {
        await dbContext.Instruments.Where(e => e.Id == id).ExecuteDeleteAsync();
    }


}