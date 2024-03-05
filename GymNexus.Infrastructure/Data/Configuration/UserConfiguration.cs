using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymNexus.Infrastructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .HasMany(u => u.Comments)
            .WithOne(c => c.Creator);

        builder.HasData(ConfigurationHelper.RootApplicationUser);
        builder.HasData(ConfigurationHelper.TestApplicationUser);
    }
}