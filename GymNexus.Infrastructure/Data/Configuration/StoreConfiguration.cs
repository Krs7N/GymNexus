using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymNexus.Infrastructure.Data.Configuration;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder
            .HasOne(s => s.Marketplace)
            .WithMany(mp => mp.Stores);

        builder
            .HasQueryFilter(s => s.IsActive)
            .HasData(ConfigurationHelper.GetSeedStores());
    }
}