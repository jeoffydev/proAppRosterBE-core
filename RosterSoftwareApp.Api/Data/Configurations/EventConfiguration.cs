using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Data.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        // Example Price with range value
        // builder.Property(e => e.Price).HasPrecision(5, 2);

        // throw new NotImplementedException();
    }
}
