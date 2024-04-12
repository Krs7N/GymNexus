using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GymNexus.Tests;

public class TestBase
{
    protected ApplicationDbContext _context;
    protected Mock<UserManager<ApplicationUser>> _userManagerMock;
    protected Mock<RoleManager<IdentityRole>> _roleManagerMock;

    [SetUp]
    public async Task SetUpBase()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "testDb")
            .Options;

        _context = new ApplicationDbContext(options);

        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);

        var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                roleStoreMock.Object, null, null, null, null);

        var roles = new List<string>()
        {
            "Owner",
            "Seller"
        };

        _userManagerMock.Setup(x => x.FindByEmailAsync("test@example.com"))
            .ReturnsAsync(UserWithRoles);
        _userManagerMock.Setup(x => x.AddToRolesAsync(UserWithRoles, roles))
            .ReturnsAsync(IdentityResult.Success);

        _userManagerMock.Setup(x => x.IsInRoleAsync(It.Is<ApplicationUser>(u => u.Email == "test@example.com"), "Owner")).ReturnsAsync(true);
        _userManagerMock.Setup(x => x.IsInRoleAsync(It.Is<ApplicationUser>(u => u.Email == "test@example.com"), "Seller")).ReturnsAsync(true);

        foreach (var roleName in roles)
        {
            _roleManagerMock.Setup(x => x.RoleExistsAsync(roleName)).ReturnsAsync(true);
        }

        await _userManagerMock.Object.AddToRolesAsync(UserWithRoles, roles);

        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await SeedDatabase();
    }

    public ApplicationUser UserWithRoles { get; set; } = new ApplicationUser()
    {
        UserName = "test@example.com",
        Email = "test@example.com"
    };

    public ApplicationUser User { get; set; } = null!;

    public Product Product { get; set; } = null!;

    public Post Post { get; set; } = null!;

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

        Product = new Product
        {
            Id = 3,
            Name = "Kevin Levrone's Whey Protein",
            Description =
                "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
            CategoryId = 1,
            CreatedOn = DateTime.Now.AddDays(-5),
            ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
            StoreId = 1,
            Price = 50.00m,
        };

        await _context.Products.AddAsync(Product);

        Post = new Post()
        {
            Id = 4,
            Title = "Test Post",
            Content = "Test Content",
            CreatedBy = User.Id,
            CreatedOn = DateTime.Now,
            ImageUrl = "https://test.com/image.jpg"
        };

        await _context.Posts.AddAsync(Post);

        await _context.SaveChangesAsync();
    }

    [TearDown]
    public async Task TearDownBase()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.DisposeAsync();
    }
}