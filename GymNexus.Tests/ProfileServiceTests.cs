using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Services;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;

namespace GymNexus.Tests;

[TestFixture]
public class ProfileServiceTests : TestBase
{
    private IProfileService _profileService;

    [SetUp]
    public void SetUp()
    {
        _profileService = new ProfileService(_userManagerMock.Object, _context);
    }

    [Test]
    public async Task UpdateProfileAsyncReturnsProfileUpdateResponseDto()
    {
        var profileUpdateDto = new ProfileUpdateDto()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = User.Email,
            ImageUrl = "https://www.example.com/image.jpg"
        };

        var result = await _profileService.UpdateProfileAsync(profileUpdateDto, User);

        Assert.NotNull(result);
        Assert.That(result.FirstName, Is.EqualTo(profileUpdateDto.FirstName));
        Assert.That(result.LastName, Is.EqualTo(profileUpdateDto.LastName));
        Assert.That(result.Email, Is.EqualTo(profileUpdateDto.Email));
        Assert.That(result.ImageUrl, Is.EqualTo(profileUpdateDto.ImageUrl));
    }

    [Test]
    public async Task GetUserStoresAsyncReturnsUserStores()
    {
        var stores = new List<Store>
        {
            new Store() { Id = 10, Name = "Store 1", OwnerId = User.Id, Description = "Test Description 112323", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now},
            new Store() { Id = 11, Name = "Store 2", OwnerId = User.Id, Description = "Test Longer Description", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now}
        };

        _context.Stores.AddRange(stores);
        await _context.SaveChangesAsync();

        var result = await _profileService.GetUserStoresAsync(User.Id);

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetUserStoresAsyncReturnsEmptyListIfNoStoresFound()
    {
        var result = await _profileService.GetUserStoresAsync(User.Id);

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task GetUserStoresAsyncReturnsEmptyListIfUserDoesNotExist()
    {
        var stores = new List<Store>
        {
            new Store() { Id = 10, Name = "Store 1", OwnerId = User.Id, Description = "Test Description 112323", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now},
            new Store() { Id = 11, Name = "Store 2", OwnerId = User.Id, Description = "Test Longer Description", RatingsCount = 2, AverageRating = 2.5m, CreatedOn = DateTime.Now}
        };

        _context.Stores.AddRange(stores);
        await _context.SaveChangesAsync();

        var result = await _profileService.GetUserStoresAsync("non-existing-user-id");

        Assert.NotNull(result);
        Assert.That(result.Count(), Is.EqualTo(0));
    }
}