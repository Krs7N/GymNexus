using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymNexus.Infrastructure.Data.Configuration;

public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
{
    public void Configure(EntityTypeBuilder<PostLike> builder)
    {
        builder
            .HasKey(pl => new { pl.PostId, pl.UserId });

        builder
            .HasOne(pl => pl.Post)
            .WithMany(p => p.PostsLikes);
    }
}