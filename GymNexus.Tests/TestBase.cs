using GymNexus.Core.Utils;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymNexus.Tests;

public class TestBase
{
    protected ApplicationDbContext _context;
    protected UserManager<ApplicationUser> _userManager;

    [SetUp]
    public async Task SetUpBase()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "testDb")
            .Options;

        _context = new ApplicationDbContext(options);

        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await SeedDatabase();
    }

    public ApplicationUser User { get; set; } = null!;

    public Category Category { get; set; } = null!;

    public Marketplace Marketplace { get; set; } = null!;

    public Product Product { get; set; } = null!;

    public Order Order { get; set; } = null!;

    public Post Post { get; set; } = null!;

    public Store Store { get; set; } = null!;

    public Comment Comment { get; set; } = null!;

    private async Task SeedDatabase()
    {
        User = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = "test@gmail.com",
            FirstName = "Test",
            LastName = "Testov"
        };

        await _context.Users.AddAsync(User);

        Category = new Category
        {
            Id = 35,
            Name = "Test Category",
            Description = "Test Category description which is very long"
        };

        await _context.Categories.AddAsync(Category);

        Marketplace = new Marketplace
        {
            Id = 35,
            Name = "Test Marketplace",
            Description = "Test Marketplace description which is very long and the users will not have any information about it",
            Address = "Test Address 65 8000",
            Latitude = 42.698334m,
            Longitude = 23.319941m
        };

        await _context.Marketplaces.AddAsync(Marketplace);

        Product = new Product
        {
            Id = 35,
            Name = "Test Product",
            Description = "Test Product description which is very long and the users will not have any information about it",
            Price = 10.00m,
            CategoryId = Category.Id,
            CreatedOn = DateTime.Now,
            ImageUrl = "https://test.com/test.jpg"
        };

        await _context.Products.AddAsync(Product);

        Order = new Order
        {
            Id = 35,
            CreatedBy = User.Id,
            CreatedOn = DateTime.Now,
            Quantity = 1,
            TotalPrice = Product.Price,
            PaymentMethod = "InCash",
            Status = OrderStatus.Pending.ToString()
        };

        await _context.Orders.AddAsync(Order);

        Post = new Post
        {
            Id = 35,
            Title = "Test Post",
            Content = "Test Post content which is very long and the users will not have any information about it",
            CreatedBy = User.Id,
            CreatedOn = DateTime.Now
        };

        await _context.Posts.AddAsync(Post);

        Store = new Store
        {
            Id = 35,
            Name = "Test Store",
            Description = "Test Store description which is very long and the users will not have any information about it",
            MarketplaceId = Marketplace.Id,
            OwnerId = User.Id,
            AverageRating = 5.00m
        };

        await _context.Stores.AddAsync(Store);

        Comment = new Comment
        {
            Id = 35,
            Content = "Test Comment",
            CreatedBy = User.Id,
            CreatedOn = DateTime.Now,
            PostId = Post.Id
        };

        await _context.Comments.AddAsync(Comment);

        await _context.SaveChangesAsync();
    }

    [TearDown]
    public async Task TearDownBase()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.DisposeAsync();
    }
}