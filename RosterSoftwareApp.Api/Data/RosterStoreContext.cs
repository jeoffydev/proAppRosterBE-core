
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

    // add this after adding the EventConfiguration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
