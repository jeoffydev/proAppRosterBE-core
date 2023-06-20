using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public interface IInstrumentRepository
{
    Task CreateInstrumentAsync(Instrument i);
    Task DeleteInstrumentAsync(int id);
    Task<IEnumerable<Instrument>> GetAllInstrumentsAsync();
    Task<Instrument?> GetInstrumentAsync(int id);
    Task UpdateInstrumentAsync(Instrument updatedInstrument);
}
