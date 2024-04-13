using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Services;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymNexus.Tests;

[TestFixture]
public class AdminServiceTests : TestBase
{
    private IAdminService _adminService;

    [SetUp]
    public void SetUp()
    {
        _adminService = new AdminService(_context, _userManagerMock.Object);
    }

    [Test]
    public async Task GetAllOrdersCountAsyncReturnsAllOrdersCount()
    {
        var orders = new List<Order>
        {
            new Order { Id = 1, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Pending", TotalPrice = 12.25m},
            new Order { Id = 2, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Pending", TotalPrice = 12.25m },
            new Order {Id = 3, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Pending", TotalPrice = 12.25m}
        };

        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();

        var result = await _adminService.GetAllOrdersCountAsync();

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public async Task GetPendingOrdersCountAsyncReturnsPendingOrdersCount()
    {
        var orders = new List<Order>
        {
            new Order { Id = 1, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Pending", TotalPrice = 12.25m},
            new Order { Id = 2, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Pending", TotalPrice = 12.25m },
            new Order {Id = 3, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Completed", TotalPrice = 12.25m}
        };

        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();

        var result = await _adminService.GetPendingOrdersCountAsync();

        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public async Task GetAllOrdersAsyncReturnsAllOrders()
    {
        var orders = new List<Order>
        {
            new Order { Id = 1, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Pending", TotalPrice = 12.25m},
            new Order { Id = 2, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Pending", TotalPrice = 12.25m },
            new Order {Id = 3, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Completed", TotalPrice = 12.25m}
        };

        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();

        var result = await _adminService.GetAllOrdersAsync();

        Assert.That(result.Count(), Is.EqualTo(3));
        Assert.That(result.First().Id, Is.EqualTo(3));
        Assert.That(result.First().CreatedBy, Is.EqualTo(User.Email));
        Assert.That(result.First().Quantity, Is.EqualTo(5));
        Assert.That(result.First().Status, Is.EqualTo("Completed"));
        Assert.That(result.First().TotalPrice, Is.EqualTo(12.25m));
    }

    [Test]
    public async Task ChangeOrderStatusAsyncReturnsOrderStatus()
    {
        var order = new Order { Id = 1, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Pending", TotalPrice = 12.25m};

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var result = await _adminService.ChangeOrderStatusAsync(1, order.Status);

        Assert.That(result, Is.EqualTo("Confirmed"));
    }

    [Test]
    public async Task GetConfirmedOrdersCountAsyncReturnsConfirmedOrdersCount()
    {
        var orders = new List<Order>
        {
            new Order { Id = 1, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Confirmed", TotalPrice = 12.25m},
            new Order { Id = 2, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Confirmed", TotalPrice = 12.25m },
            new Order {Id = 3, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Completed", TotalPrice = 12.25m}
        };

        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();

        var result = await _adminService.GetConfirmedOrdersCountAsync();

        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public async Task GetCompletedOrdersCountAsyncReturnsCompletedOrdersCount()
    {
        var orders = new List<Order>
        {
            new Order { Id = 1, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Completed", TotalPrice = 12.25m, IsActive = false},
            new Order { Id = 2, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Completed", TotalPrice = 12.25m, IsActive = false},
            new Order {Id = 3, CreatedBy = User.Id, CreatedOn = DateTime.Now, PaymentMethod = "InCash", Quantity = 5, Status = "Completed", TotalPrice = 12.25m, IsActive = false}
        };

        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();

        var result = await _adminService.GetCompletedOrdersCountAsync();

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public async Task GetMostLikedPostAsyncReturnsMostLikedPost()
    {
        var postLike1 = new PostLike { PostId = 1, UserId = Guid.NewGuid().ToString() };
        var postLike2 = new PostLike { PostId = 3, UserId = Guid.NewGuid().ToString() };
        var postLike3 = new PostLike { PostId = 3, UserId = Guid.NewGuid().ToString() };

        var posts = new List<Post>
        {
            new Post
            {
                Id = 10, Title = "Post 1", Content = "Content 1", CreatedBy = User.Id, CreatedOn = DateTime.Now,
                PostsLikes = new List<PostLike>() { postLike1 }
            },
            new Post
            {
                Id = 11, Title = "Post 2", Content = "Content 2", CreatedBy = User.Id, CreatedOn = DateTime.Now,
                PostsLikes = new List<PostLike>()
            },
            new Post
            {
                Id = 12, Title = "Post 3", Content = "Content 3", CreatedBy = User.Id, CreatedOn = DateTime.Now,
                PostsLikes = new List<PostLike>() { postLike2, postLike3 }
            }
        };

        _context.Posts.AddRange(posts);
        await _context.SaveChangesAsync();

        var result = await _adminService.GetMostLikedPostAsync();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(12));
        Assert.That(result.Title, Is.EqualTo("Post 3"));
        Assert.That(result.Content, Is.EqualTo("Content 3"));
        Assert.That(result.Likes, Is.EqualTo(2));
    }

    [Test]
    public async Task GetMostCommentedPostAsyncReturnsMostCommentedPost()
    {
        var postComment1 = new Comment() { PostId = 1, Content = "Comment Content 1", CreatedBy = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now};
        var postComment2 = new Comment() { PostId = 3, Content = "Comment Content 2", CreatedBy = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now};
        var postComment3 = new Comment() { PostId = 3, Content = "Comment Content 3", CreatedBy = Guid.NewGuid().ToString(), CreatedOn = DateTime.Now };

        var posts = new List<Post>
        {
            new Post
            {
                Id = 10, Title = "Post 1", Content = "Content 1", CreatedBy = User.Id, CreatedOn = DateTime.Now,
                Comments = new List<Comment>() { postComment1 }
            },
            new Post
            {
                Id = 11, Title = "Post 2", Content = "Content 2", CreatedBy = User.Id, CreatedOn = DateTime.Now,
                Comments = new List<Comment>()
            },
            new Post
            {
                Id = 12, Title = "Post 3", Content = "Content 3", CreatedBy = User.Id, CreatedOn = DateTime.Now,
                Comments = new List < Comment >() { postComment2, postComment3 }
            }
        };

        _context.Posts.AddRange(posts);
        await _context.SaveChangesAsync();

        var result = await _adminService.GetMostCommentedPostAsync();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(12));
        Assert.That(result.Title, Is.EqualTo("Post 3"));
        Assert.That(result.Content, Is.EqualTo("Content 3"));
        Assert.That(result.Comments, Is.EqualTo(2));
    }

    [Test]
    public async Task AddMarketplaceAsyncAddsMarketplace()
    {
        var marketplaceFormDto = new MarketplaceFormDto
        {
            Name = "Marketplace 1",
            Description = "Marketplace 1 Description",
            Address = "Knyaz Boris I 65 Plovdiv 4000",
        };

        await _adminService.AddMarketplaceAsync(marketplaceFormDto);

        var marketplace = await _context.Marketplaces.OrderByDescending(m => m.Id).FirstOrDefaultAsync();

        Assert.That(marketplace, Is.Not.Null);
        Assert.That(marketplace.Name, Is.EqualTo("Marketplace 1"));
        Assert.That(marketplace.Description, Is.EqualTo("Marketplace 1 Description"));
        Assert.That(marketplace.Address, Is.EqualTo("Knyaz Boris I 65 Plovdiv 4000"));
    }

    [Test]
    public async Task AddMarketplaceAsyncThrowsExceptionWhenAddressIsInvalid()
    {
        var marketplaceFormDto = new MarketplaceFormDto
        {
            Name = "Marketplace 1",
            Description = "Marketplace 1 Description",
            Address = "Invalid Address",
        };

        Assert.ThrowsAsync<InvalidOperationException>(() => _adminService.AddMarketplaceAsync(marketplaceFormDto));
    }
}