using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymNexus.Infrastructure.Data.Configuration;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder
            .HasKey(od => new { od.OrderId, od.ProductId });

        builder
            .HasOne(od => od.Order)
            .WithMany(o => o.OrdersDetails)
            .OnDelete(DeleteBehavior.Restrict);
    }
}