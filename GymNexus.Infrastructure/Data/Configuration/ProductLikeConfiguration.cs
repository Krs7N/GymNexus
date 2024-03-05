using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymNexus.Infrastructure.Data.Configuration;

public class ProductLikeConfiguration : IEntityTypeConfiguration<ProductLike>
{
    public void Configure(EntityTypeBuilder<ProductLike> builder)
    {
        builder
            .HasKey(pl => new { pl.ProductId, pl.UserId });

        builder
            .HasOne(pl => pl.Product)
            .WithMany(p => p.ProductsLikes);
    }
}