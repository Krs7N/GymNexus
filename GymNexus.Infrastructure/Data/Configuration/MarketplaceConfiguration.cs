using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymNexus.Infrastructure.Data.Configuration;

public class MarketplaceConfiguration : IEntityTypeConfiguration<Marketplace>
{
    public void Configure(EntityTypeBuilder<Marketplace> builder)
    {
        builder
            .HasQueryFilter(mp => mp.IsActive)
            .HasData(ConfigurationHelper.GetSeedMarketplaces());
    }
}