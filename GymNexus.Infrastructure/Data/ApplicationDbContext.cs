using GymNexus.Infrastructure.Data.Configuration;
using GymNexus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymNexus.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public virtual DbSet<Marketplace> Marketplaces { get; set; } = null!;

    public virtual DbSet<Store> Stores { get; set; } = null!;

    public virtual DbSet<Category> Categories { get; set; } = null!;

    public virtual DbSet<Product> Products { get; set; } = null!;

    public virtual DbSet<Comment> Comments { get; set; } = null!;

    public virtual DbSet<Order> Orders { get; set; } = null!;

    public virtual DbSet<OrderDetail> OrdersDetails { get; set; } = null!;

    public virtual DbSet<Post> Posts { get; set; } = null!;

    public virtual DbSet<PostLike> PostsLikes { get; set; } = null!;

    public virtual DbSet<ProductLike> ProductsLikes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new IdentityRolesConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RolesConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new MarketplaceConfiguration());
        builder.ApplyConfiguration(new PostConfiguration());
        builder.ApplyConfiguration(new StoreConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new OrderDetailConfiguration());
        builder.ApplyConfiguration(new PostLikeConfiguration());
        builder.ApplyConfiguration(new ProductLikeConfiguration());

        base.OnModelCreating(builder);
    }
}