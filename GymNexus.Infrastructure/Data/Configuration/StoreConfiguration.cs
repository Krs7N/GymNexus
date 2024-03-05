using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymNexus.Infrastructure.Data.Configuration;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder
            .HasOne(s => s.Owner)
            .WithMany(o => o.Stores)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(s => s.Marketplace)
            .WithMany(mp => mp.Stores);

        builder
            .HasMany(s => s.Orders)
            .WithOne(o => o.Store)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasData(ConfigurationHelper.GetSeedStores());
    }
}