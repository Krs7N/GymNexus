using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Services;
using GymNexus.Infrastructure.Data.Models;

namespace GymNexus.Tests;

[TestFixture]
public class ProductServiceTests : TestBase
{
    private IProductService _productService = null!;


    [SetUp]
    public void SetUp()
    {
        _productService = new ProductService(_context, _userManagerMock.Object);
    }

    [Test]
    public async Task GetAllAsyncShouldReturnAllProducts()
    {
        var product = new Product
        {
            Id = 1,
            Name = "Kevin Levrone's Whey Protein",
            Description =
                "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
            CategoryId = 1,
            CreatedOn = DateTime.Now.AddDays(-5),
            ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
            StoreId = 1,
            Price = 50.00m,
        };

        var result = await _productService.GetAllAsync(User.Id);

        Assert.That(result.Count(), Is.EqualTo(3));
        Assert.That(result.First().Id, Is.EqualTo(product.Id));
        Assert.That(result.First().Name, Is.EqualTo(product.Name));
        Assert.That(result.First().Description, Is.EqualTo(product.Description));
        Assert.That(result.First().ImageUrl, Is.EqualTo(product.ImageUrl));
        Assert.That(result.First().Price, Is.EqualTo(product.Price));
        Assert.That(result.First().CreatedOn, Is.EqualTo(product.CreatedOn.ToString("dd/MM/yyyy HH:mm")));
        Assert.That(result.First().Category.Id, Is.EqualTo(product.CategoryId));
        Assert.That(result.First().Store.Id, Is.EqualTo(product.StoreId));
    }

    [Test]
    public async Task ToggleProductLikeByIdAsyncShouldToggleProductLike()
    {
        var productLike = new ProductLike
        {
            ProductId = 3,
            UserId = User.Id
        };

        await _context.ProductsLikes.AddAsync(productLike);
        await _context.SaveChangesAsync();

        await _productService.ToggleProductLikeByIdAsync(Product.Id, User.Id);

        Assert.That(Product.ProductsLikes.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task ToggleProductLikeByIdAsyncShouldAddProductLike()
    {
        await _context.SaveChangesAsync();

        await _productService.ToggleProductLikeByIdAsync(Product.Id, User.Id);

        Assert.That(Product.ProductsLikes.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task ToggleProductLikeByIdAsyncShouldThrowExceptionIfProductDoesNotExist()
    {
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _productService.ToggleProductLikeByIdAsync(10, User.Id));

        Assert.That(exception.Message, Is.EqualTo("Product does not exist."));
    }

    [Test]
    public async Task ToggleProductLikeByIdAsyncShouldThrowExceptionIfProductIsNotActive()
    {
        var product = new Product
        {
            Id = 4,
            Name = "Kevin Levrone's Whey Protein",
            Description =
                "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
            CategoryId = 1,
            CreatedOn = DateTime.Now.AddDays(-5),
            ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
            StoreId = 1,
            Price = 50.00m,
            IsActive = false
        };

        await _context.SaveChangesAsync();

        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                       await _productService.ToggleProductLikeByIdAsync(product.Id, User.Id));

        Assert.That(exception.Message, Is.EqualTo("Product does not exist."));
    }

    [Test]
    public async Task IsCurrentUserLikedProductAsyncShouldReturnTrueIfUserLikedProduct()
    {
        var productLike = new ProductLike
        {
            ProductId = 3,
            UserId = User.Id
        };

        await _context.ProductsLikes.AddAsync(productLike);
        await _context.SaveChangesAsync();

        var result = await _productService.IsCurrentUserLikedProductAsync(Product.Id, User.Id);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task IsCurrentUserLikedProductAsyncShouldReturnFalseIfUserDidNotLikeProduct()
    {
        var result = await _productService.IsCurrentUserLikedProductAsync(Product.Id, User.Id);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetProductByIdAsyncShouldReturnProduct()
    {
        var result = await _productService.GetProductByIdAsync(Product.Id, User.Id);

        Assert.That(result.Id, Is.EqualTo(Product.Id));
        Assert.That(result.Name, Is.EqualTo(Product.Name));
        Assert.That(result.Description, Is.EqualTo(Product.Description));
        Assert.That(result.ImageUrl, Is.EqualTo(Product.ImageUrl));
        Assert.That(result.Price, Is.EqualTo(Product.Price));
        Assert.That(result.CreatedOn, Is.EqualTo(Product.CreatedOn.ToString("dd/MM/yyyy HH:mm")));
        Assert.That(result.Category.Id, Is.EqualTo(Product.CategoryId));
        Assert.That(result.Store.Id, Is.EqualTo(Product.StoreId));
    }

    [Test]
    public async Task GetProductByIdAsyncShouldReturnNullIfProductDoesNotExist()
    {
        var result = await _productService.GetProductByIdAsync(10, User.Id);

        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public async Task UpdateProductByIdAsyncShouldUpdateProduct()
    {
        var productFormDto = new ProductFormDto
        {
            Name = "Kevin Levrone's Whey Protein",
            Description =
                "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
            CategoryId = 1,
            ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
            Price = 50.00m,
            StoreId = 1
        };

        var result = await _productService.UpdateProductByIdAsync(Product.Id, productFormDto, UserWithRoles);

        Assert.That(result.Name, Is.EqualTo(productFormDto.Name));
        Assert.That(result.Description, Is.EqualTo(productFormDto.Description));
        Assert.That(result.ImageUrl, Is.EqualTo(productFormDto.ImageUrl));
        Assert.That(result.Price, Is.EqualTo(productFormDto.Price));
        Assert.That(result.StoreId, Is.EqualTo(productFormDto.StoreId));
        Assert.That(result.CategoryId, Is.EqualTo(productFormDto.CategoryId));
    }

    [Test]
    public async Task UpdateProductByIdAsyncShouldThrowExceptionIfProductDoesNotExist()
    {
        var productFormDto = new ProductFormDto
        {
            Name = "Kevin Levrone's Whey Protein",
            Description =
                "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
            CategoryId = 1,
            ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
            Price = 50.00m,
            StoreId = 1
        };

        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                       await _productService.UpdateProductByIdAsync(10, productFormDto, UserWithRoles));

        Assert.That(exception.Message, Is.EqualTo("Product does not exist."));
    }

    [Test]
    public async Task UpdateProductByIdAsyncShouldThrowExceptionIfProductIsNotActive()
    {
        var product = new Product
        {
            Id = 4,
            Name = "Kevin Levrone's Whey Protein",
            Description =
                "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
            CategoryId = 1,
            CreatedOn = DateTime.Now.AddDays(-5),
            ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
            StoreId = 1,
            Price = 50.00m,
            IsActive = false
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        var productFormDto = new ProductFormDto
        {
            Name = "Kevin Levrone's Whey Protein",
            Description =
                "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
            CategoryId = 1,
            ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
            Price = 50.00m,
            StoreId = 1
        };

        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                                  await _productService.UpdateProductByIdAsync(product.Id, productFormDto, UserWithRoles));

        Assert.That(exception.Message, Is.EqualTo("Product does not exist."));
    }

    [Test]
    public async Task DeleteProductByIdAsyncShouldDeleteProduct()
    {
        var result = await _productService.DeleteProductByIdAsync(Product.Id);

        Assert.That(result.Name, Is.EqualTo(Product.Name));
        Assert.That(result.Description, Is.EqualTo(Product.Description));
        Assert.That(result.ImageUrl, Is.EqualTo(Product.ImageUrl));
        Assert.That(result.Price, Is.EqualTo(Product.Price));
        Assert.That(result.StoreId, Is.EqualTo(Product.StoreId));
        Assert.That(result.CategoryId, Is.EqualTo(Product.CategoryId));
    }

    [Test]
    public async Task DeleteProductByIdAsyncShouldThrowExceptionIfProductDoesNotExist()
    {
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                                  await _productService.DeleteProductByIdAsync(10));

        Assert.That(exception.Message, Is.EqualTo("Product does not exist."));
    }

    [Test]
    public async Task DeleteProductByIdAsyncShouldThrowExceptionIfProductIsNotActive()
    {
        var product = new Product
        {
            Id = 4,
            Name = "Kevin Levrone's Whey Protein",
            Description =
                "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
            CategoryId = 1,
            CreatedOn = DateTime.Now.AddDays(-5),
            ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
            StoreId = 1,
            Price = 50.00m,
            IsActive = false
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                                             await _productService.DeleteProductByIdAsync(product.Id));

        Assert.That(exception.Message, Is.EqualTo("Product does not exist."));
    }

    [Test]
    public async Task AddProductAsyncShouldAddProduct()
    {
        var productFormDto = new ProductFormDto
        {
            Name = "Kevin Levrone's Whey Protein",
            Description =
                "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g",
            CategoryId = 1,
            ImageUrl = "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg",
            Price = 50.00m,
            StoreId = 1
        };

        var result = await _productService.AddProductAsync(productFormDto, UserWithRoles);

        Assert.That(result.Name, Is.EqualTo(productFormDto.Name));
        Assert.That(result.Description, Is.EqualTo(productFormDto.Description));
        Assert.That(result.ImageUrl, Is.EqualTo(productFormDto.ImageUrl));
        Assert.That(result.Price, Is.EqualTo(productFormDto.Price));
        Assert.That(result.StoreId, Is.EqualTo(productFormDto.StoreId));
        Assert.That(result.CategoryId, Is.EqualTo(productFormDto.CategoryId));
    }
}