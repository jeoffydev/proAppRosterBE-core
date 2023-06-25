
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Data;

public class RosterStoreContext : DbContext
{
    public RosterStoreContext(DbContextOptions<RosterStoreContext> options)
    : base(options)
    {

    }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<Song> Songs => Set<Song>();

    public DbSet<EventSong> EventSongs => Set<EventSong>();
    public DbSet<Instrument> Instruments => Set<Instrument>();

    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<MemberInstrument> MemberInstruments => Set<MemberInstrument>();

    // add this after adding the EventConfiguration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
