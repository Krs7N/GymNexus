using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Services;
using GymNexus.Infrastructure.Data.Models;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Tests;

[TestFixture]
public class StoreServiceTests : TestBase
{
    private IStoreService _storeService;

    [SetUp]
    public void SetUp()
    {
        _storeService = new StoreService(_context);
    }

    [Test]
    public async Task GetAllStoresAsyncReturnsAllStores()
    {
        var stores = new List<Store>
        {
            new Store() { Id = 10, Name = "Store 1", OwnerId = User.Id, Description = "Test Description 112323", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now},
            new Store() { Id = 11,Name = "Store 2", OwnerId = User.Id, Description = "Test Longer Description", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now}
        };

        _context.Stores.AddRange(stores);
        await _context.SaveChangesAsync();

        var result = await _storeService.GetAllStoresAsync();

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task GetStoresByMarketplaceIdAsyncReturnsStoresByMarketplaceId()
    {
        var stores = new List<Store>
        {
            new Store() { Id = 10, Name = "Store 1", OwnerId = User.Id, Description = "Test Description 112323", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now, MarketplaceId = 4},
            new Store() { Id = 11,Name = "Store 2", OwnerId = User.Id, Description = "Test Longer Description", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now, MarketplaceId = 4}
        };

        _context.Stores.AddRange(stores);
        await _context.SaveChangesAsync();

        var result = await _storeService.GetStoresByMarketplaceIdAsync(4);

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetStoresByMarketplaceIdAsyncReturnsEmptyListIfNoStoresFound()
    {
        var result = await _storeService.GetStoresByMarketplaceIdAsync(4);

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task GetStoresByMarketplaceIdAsyncReturnsEmptyListIfMarketplaceIdDoesNotExist()
    {
        var stores = new List<Store>
        {
            new Store() { Id = 10, Name = "Store 1", OwnerId = User.Id, Description = "Test Description 112323", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now, MarketplaceId = 4},
            new Store() { Id = 11,Name = "Store 2", OwnerId = User.Id, Description = "Test Longer Description", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now, MarketplaceId = 4}
        };

        _context.Stores.AddRange(stores);
        await _context.SaveChangesAsync();

        var result = await _storeService.GetStoresByMarketplaceIdAsync(5);

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(0));
    }
}
