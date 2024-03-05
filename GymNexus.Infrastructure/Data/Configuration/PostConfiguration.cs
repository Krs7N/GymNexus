using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymNexus.Infrastructure.Data.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasOne(p => p.Creator)
            .WithMany(u => u.Posts);

        builder
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post);

        builder
            .HasQueryFilter(p => p.IsActive)
            .HasData(ConfigurationHelper.GetSeedPosts());
    }
}