using GymNexus.Core.Contracts;
using GymNexus.Core.Models;
using GymNexus.Core.Utils;
using GymNexus.Infrastructure.Data;
using GymNexus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static GymNexus.Infrastructure.Constants.DataConstants;

namespace GymNexus.Core.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProductService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(string userId)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.IsActive)
            .Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                CreatedOn = p.CreatedOn.ToString(DateTimeFormat),
                Marketplace = new MarketplaceViewDto()
                {
                    Id = p.Store.MarketplaceId,
                    Name = p.Store.Marketplace.Name
                },
                Likes = p.ProductsLikes.Count(pl => pl.ProductId == p.Id),
                IsLikedByCurrentUser = p.ProductsLikes.Any(pl => pl.UserId == userId),
                Category = new CategoryDto()
                {
                    Id = p.CategoryId,
                    Name = p.Category.Name
                },
                Store = new StoreViewDto()
                {
                    Id = p.StoreId,
                    Name = p.Store.Name,
                }
            })
            .ToListAsync();
    }

    public async Task ToggleProductLikeByIdAsync(int id, string userId)
    {
        var product = await _context.Products
            .Where(p => p.IsActive)
            .Include(p => p.ProductsLikes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            throw new InvalidOperationException("Product does not exist.");
        }

        var like = product.ProductsLikes.FirstOrDefault(pl => pl.UserId == userId);

        if (like == null)
        {
            await _context.ProductsLikes.AddAsync(new ProductLike()
            {
                ProductId = product.Id,
                UserId = userId
            });
        }
        else
        {
            _context.ProductsLikes.Remove(like);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsCurrentUserLikedProductAsync(int id, string userId)
    {
        return await _context.ProductsLikes
            .AsNoTracking()
            .AnyAsync(pl => pl.ProductId == id && pl.UserId == userId);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id, string userId)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.IsActive && p.Id == id)
            .Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                CreatedOn = p.CreatedOn.ToString(DateTimeFormat),
                Marketplace = new MarketplaceViewDto()
                {
                    Id = p.Store.MarketplaceId,
                    Name = p.Store.Marketplace.Name
                },
                Likes = p.ProductsLikes.Count(pl => pl.ProductId == p.Id),
                IsLikedByCurrentUser = p.ProductsLikes.Any(pl => pl.UserId == userId),
                Category = new CategoryDto()
                {
                    Id = p.CategoryId,
                    Name = p.Category.Name
                },
                Store = new StoreViewDto()
                {
                    Id = p.StoreId,
                    Name = p.Store.Name
                }
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ProductFormDto> UpdateProductByIdAsync(int id, ProductFormDto productFormDto, ApplicationUser user)
    {
        var product = await _context.Products.Include(product => product.Store).FirstOrDefaultAsync(p => p.IsActive && p.Id == id);

        if (product == null)
        {
            throw new InvalidOperationException("Product does not exist.");
        }

        if (!(await _userManager.IsInRoleAsync(user, Roles.Owner) ||
              await _userManager.IsInRoleAsync(user, Roles.Seller)))
        {
            throw new InvalidOperationException();
        }

        product.Name = productFormDto.Name;
        product.Description = productFormDto.Description;
        product.Price = productFormDto.Price;
        product.ImageUrl = productFormDto.ImageUrl;
        product.CategoryId = productFormDto.CategoryId;
        product.StoreId = productFormDto.StoreId;

        await _context.SaveChangesAsync();

        return new ProductFormDto()
        {
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            StoreId = product.StoreId,
            CategoryId = product.CategoryId,
            MarketplaceId = product.Store.MarketplaceId
        };
    }

    public async Task<ProductFormDto> DeleteProductByIdAsync(int id)
    {
        var product = await _context.Products.Include(product => product.Store)
            .FirstOrDefaultAsync(p => p.IsActive && p.Id == id);

        if (product == null)
        {
            throw new InvalidOperationException("Product does not exist.");
        }

        product.IsActive = false;

        await _context.SaveChangesAsync();

        return new ProductFormDto()
        {
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            StoreId = product.StoreId,
            CategoryId = product.CategoryId,
            MarketplaceId = product.Store.MarketplaceId
        };
    }

    public async Task<ProductFormDto> AddProductAsync(ProductFormDto productModel, ApplicationUser user)
    {
        var product = new Product()
        {
            Name = productModel.Name,
            Description = productModel.Description,
            Price = productModel.Price,
            ImageUrl = productModel.ImageUrl,
            CategoryId = productModel.CategoryId,
            StoreId = productModel.StoreId,
            CreatedOn = DateTime.Now
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return new ProductFormDto()
        {
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            StoreId = product.StoreId,
            CategoryId = product.CategoryId,
            MarketplaceId = productModel.MarketplaceId
        };
    }
}