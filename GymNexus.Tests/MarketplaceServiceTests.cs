using GymNexus.Core.Contracts;
using GymNexus.Core.Services;
using GymNexus.Infrastructure.Data.Models;

namespace GymNexus.Tests;

[TestFixture]
public class MarketplaceServiceTests : TestBase
{
    private IMarketplaceService _marketplaceService;

    [SetUp]
    public void SetUp()
    {
        _marketplaceService = new MarketplaceService(_context);
    }

    [Test]
    public async Task GetAllMarketplacesAsyncReturnsAllMarketplaces()
    {
        var result = await _marketplaceService.GetAllAsync();

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(4));
    }

    [Test]
    public async Task GetAllWithStoresAsyncReturnsAllMarketplacesWithStores()
    {
        var result = await _marketplaceService.GetAllWithStoresAsync();

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetAllWithStoresAsyncReturnsEmptyListIfNoMarketplacesFound()
    {
        _context.Marketplaces.RemoveRange(_context.Marketplaces);
        await _context.SaveChangesAsync();

        var result = await _marketplaceService.GetAllWithStoresAsync();

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task GetAllWithStoresAsyncReturnsEmptyListIfNoActiveMarketplacesFound()
    {
        _context.Marketplaces.RemoveRange(_context.Marketplaces);
        _context.Marketplaces.AddRange(new[]
        {
            new Marketplace() { Id = 1, Name = "Marketplace 1", Address = "Plovdiv 4000", Description = "Test Description long", Longitude = 42.56789m, Latitude = 44.44433m ,IsActive = false },
            new Marketplace() { Id = 2, Name = "Marketplace 2", Address = "Burgas 8000", Description = "Test Description long", Longitude = 44.56789m, Latitude = 46.44433m ,IsActive = false }
        });
        await _context.SaveChangesAsync();

        var result = await _marketplaceService.GetAllWithStoresAsync();

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(0));
    }
}