using GymNexus.Core.Contracts;
using GymNexus.Core.Services;
using GymNexus.Infrastructure.Data.Models;

namespace GymNexus.Tests;

public class ProductServiceTests : TestBase
{
    private IProductService _productService = null!;

    [SetUp]
    public void SetUp()
    {
        _productService = new ProductService(_context, _userManager);
    }

    [Test]
    public async Task GetAllAsyncShouldReturnAllProducts()
    {
        var result = await _productService.GetAllAsync(User.Id);

        Console.WriteLine(Product);

        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Id, Is.EqualTo(Product.Id));
        Assert.That(result.First().Name, Is.EqualTo(Product.Name));
        Assert.That(result.First().Description, Is.EqualTo(Product.Description));
        Assert.That(result.First().ImageUrl, Is.EqualTo(Product.ImageUrl));
        Assert.That(result.First().Price, Is.EqualTo(Product.Price));
        Assert.That(result.First().CreatedOn, Is.EqualTo(Product.CreatedOn.ToString("dd/MM/yyyy")));
        Assert.That(result.First().Category.Id, Is.EqualTo(Product.CategoryId));
        Assert.That(result.First().Store.Id, Is.EqualTo(Product.StoreId));
    }

    [Test]
    public async Task ToggleProductLikeByIdAsyncShouldToggleProductLike()
    {
        var product = new Product
        {
            Id = 1,
            IsActive = true
        };

        var productLike = new ProductLike
        {
            ProductId = 1,
            UserId = User.Id
        };

        await _context.Products.AddAsync(product);
        await _context.ProductsLikes.AddAsync(productLike);
        await _context.SaveChangesAsync();

        await _productService.ToggleProductLikeByIdAsync(product.Id, User.Id);

        Assert.That(product.ProductsLikes.Count(), Is.EqualTo(0));
    }
}